using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;
using System;

namespace SocialMedia.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialMediaApi", Version = "v1" });
                c.CustomSchemaIds(c => c.FullName);
            });
            //Dependencias
            services.AddScoped<IPostService, PostService>();
            //services.AddScoped<IPostRepository, PostRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //Usar una interfaz y una implementación generica
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Se agrega interfaz e implementación para generar la URL base a utilizar en la navegación (Paginación)
            services.AddSingleton<IUriService>(provider =>
            {
                //Acceder al contexto del Html según el servicio solicitado
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                //Acceder al request recibido
                var request = accesor.HttpContext.Request;
                //Construir URL base utilizando:
                //+ Scheme = Http o Https
                //+ Host
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                //Devolver nueva instancia de la clase UriService con la URL base
                return new UriService(absoluteUri);
            });
            //Setear valores de configuración appsettings en una clase
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            //Conexión a la base de datos
            services.AddDbContext<SocialMediaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            );
            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Validaciones Fluent
            services.AddMvc().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
            //Controladores
            services.AddControllers(options =>
            {
                //Agregar filtro para el manejo global de las excepciones de negocio
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                //Indicar que a la hora de serializar a JSON las propiedades que esten nulas no se van a mostrar
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMediaApi v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
