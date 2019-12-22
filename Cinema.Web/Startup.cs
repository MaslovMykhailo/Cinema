using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.BusinessLogic.Services;
using Cinema.Persisted.Context;
using Cinema.Persisted.Interfaces;
using Cinema.Persisted.Repositories;
using Cinema.Web.Clients;
using Cinema.Web.Mapping;
using Cinema.Web.Models;
using Cinema.Web.Providers.Authentication;
using Cinema.Web.Providers.FilmProviders;
using Cinema.Web.Providers.Interfaces;
using Cinema.Web.Providers.Reservation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ITicketService = Cinema.BusinessLogic.Interfaces.ITicketService;
using TicketService = Cinema.BusinessLogic.Services.TicketService;

namespace Cinema.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            // The policy is configured to try five times with an exponential retry, starting at two seconds
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            HttpResponseMessage fallback = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            fallback.Content = new StringContent("{}");

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .FallbackAsync(fallback);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFilmProvider, FilmProvider>();
            services.AddScoped<IFilmExplorerProvider, FilmExplorerProvider>();
            services.AddScoped<IFilmSearcherProvider, FilmSearcherProvider>();
            services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
            services.AddScoped<IReservationProvider, ReservationProvider>();

            services.AddHttpClient("search", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44377/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<ICinemaSearcherClient>(c))
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetFallbackPolicy());

            services.AddHttpClient("explorer", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44318/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<ICinemaExplorerClient>(c))
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetFallbackPolicy());

            services.AddHttpClient("authentication", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44328/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<IAuthenticationClient>(c));

            services.AddHttpClient("reservation", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44329/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<IReservationClient>(c));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(MapperProfile.getInstance());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            var connection = Configuration.GetConnectionString("LocalConnection");
            services.AddDbContext<CinemaContext>(options =>
                options.UseSqlServer(connection));

            services.AddMemoryCache();
            //Jwt token settings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var key = appSettingsSection.GetSection("Secret").Value;
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            var secret = Convert.ToBase64String(bytes);

            var now = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = appSettingsSection.GetSection("Issuer").Value,
                            ValidAudience = appSettingsSection.GetSection("Audience").Value,
                            IssuerSigningKey = signingCredentials.Key,

                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cinema API", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();    

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/[controller]",
                    defaults: new { controller = "film" });
            });
        }

    }
}
