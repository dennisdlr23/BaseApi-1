using GestionDocumental.WebApi.Features.Auth;
using GestionDocumental.WebApi.Features.Common;
using GestionDocumental.WebApi.Features.Users;
using GestionDocumental.WebApi.Features.Users.Services;
using GestionDocumental.WebApi.Helpers;
using GestionDocumental.WebApi.Infraestructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using BaseApi.WebApi.Repositories;



namespace GestionDocumental.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuración de Swagger para documentación de la API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GestionDocumental.WebApi", Version = "v1" });
            });

            // Configuración del contexto de base de datos
            services.AddDbContext<GestionDocumentalDbContext>(
                dbContextOptions => dbContextOptions
                    .UseSqlServer(Configuration.GetConnectionString("dbGestionD"))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            // Registro de servicios mediante inyección de dependencias
 

            services.AddTransient<AuthService, AuthService>();
            services.AddTransient<UserService, UserService>();
            services.AddTransient<CommonService, CommonService>();
            services.AddTransient<RoleService, RoleService>();
            services.AddTransient<PermissionService, PermissionService>();
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();


            // Configuración de autenticación mediante token
            services.AddTokenAuthentication(Configuration);

            // Agrega controladores MVC a los servicios
            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionDocumentalApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Habilitamos los cors para usar en la web OJO Solo en pruebas en produccion hay que especificiar el origen
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionDocumental.WebApi v1"));
            app.UseAuthentication();
            app.UseAuthorization();
       

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
