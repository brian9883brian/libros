using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Uttt.Micro.Libro.Aplicacion;
using Uttt.Micro.Libro.Persistencia;

namespace Uttt.Micro.Libro.Extenciones
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextoLibreria>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                ));

            // Registra controladores y FluentValidation
            services.AddControllers().AddFluentValidation(cfg =>
                cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            // Registra MediatR, asegúrate que el namespace y clase sean correctos
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

            // AutoMapper (si usas)
            services.AddAutoMapper(typeof(Consulta.Manejador));

            return services;
        }
    }
}
