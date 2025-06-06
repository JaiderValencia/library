﻿using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            // Repositorios
            services.AddScoped<IConexion, Conexion>();

            // Aplicaciones
            services.AddScoped<IAuditoriasAplicacion, AuditoriasAplicacion>();
            services.AddScoped<IAutoresAplicacion, AutoresAplicacion>();
            services.AddScoped<ICategoriasAplicacion, CategoriasAplicacion>();
            services.AddScoped<IClientesAplicaciones, ClientesAplicacion>();
            services.AddScoped<IEstanteriasAplicacion, EstanteriasAplicacion>();
            services.AddScoped<ILibrosAplicacion, LibrosAplicacion>();
            services.AddScoped<INiveles_tiene_LibrosAplicacion, Niveles_tiene_LibrosAplicacion>();
            services.AddScoped<INivelesAplicacion, NivelesAplicacion>();
            services.AddScoped<INumerosDeSerieAplicacion, NumerosDeSerieAplicacion>();
            services.AddScoped<IPrestamosAplicacion, PrestamosAplicacion>();
            services.AddScoped<ITiposDocumentosAplicacion, TiposDocumentosAplicacion>();
            services.AddScoped<IAccesosAplicacion, AccesosAplicacion>();
            services.AddScoped<IAdministradoresAplicacion, AdministradoresAplicacion>();
            services.AddScoped<IRolesAplicacion, RolesAplicacion>();
            services.AddScoped<IRoles_tiene_AccesosAplicacion, Roles_tiene_AccesosAplicacion>();


            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors();
        }
    }
}