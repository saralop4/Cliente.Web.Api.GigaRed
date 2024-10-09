using Cliente.Web.Api.Transversal.Modelos;
using Cliente.Web.Api.Transversal.Interfaces;
using Cliente.Web.Api.Dominio.Persistencia;
using Cliente.Web.Api.Infraestructura.Repositorios;
using Cliente.Web.Api.Dominio.Interfaces;
using Cliente.Web.Api.Aplicacion.Interfaces;
using Cliente.Web.Api.Aplicacion.Servicios;


namespace Cliente.Web.Api.Modules.Injection;

public static class InjectionExtensions
{

    public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<DapperContext>();
        services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
        services.AddScoped<IClienteServicio, ClienteServicio>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }


}
