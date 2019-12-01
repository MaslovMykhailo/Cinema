using AspNet.Security.OAuth.Validation;
using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.BusinessLogic.Services;
using Cinema.Persisted.Context;
using Cinema.Persisted.Interfaces;
using Cinema.Persisted.Repositories;
using Cinema.Web.Clients;
using Cinema.Web.Mapping;
using Cinema.Web.Providers.Authentication;
using Cinema.Web.Providers.FilmProviders;
using Cinema.Web.Providers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
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

            services.AddHttpClient("search", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44377/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<ICinemaSearcherClient>(c));

            services.AddHttpClient("explorer", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44318/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<ICinemaExplorerClient>(c));

            services.AddHttpClient("authentication", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44328/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTypedClient(c => Refit.RestService.For<IAuthenticationClient>(c));

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
            services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
                .AddOAuthValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cinema API", Version = "v1" });
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
