using AutoMapper;
using Cinema.BusinessLogic.Interfaces;
using Cinema.BusinessLogic.Services;
using Cinema.Persisted.Context;
using Cinema.Persisted.Interfaces;
using Cinema.Persisted.Repositories;
using Cinema.Web.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Cinema.Web
{
    public class Startup
    {
        readonly string _allowSpecificOrigins = "_allowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_allowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });

            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CinemaContext>(options =>
                options.UseSqlServer(connection));

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
            app.UseCors(_allowSpecificOrigins);

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
