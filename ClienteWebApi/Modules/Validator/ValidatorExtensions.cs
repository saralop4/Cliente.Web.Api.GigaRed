using Cliente.Web.Api.Aplicacion.Validadores;

namespace Cliente.Web.Api.Modules.Validator;


public static class ValidatorExtensions
{

    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
      //  services.AddTransient<ClienteDtoValidador>(); //crea una instancia por cada peticion
        services.AddTransient<ActualizarClientePersonaDtoValidador>();
        services.AddTransient<ClientePersonaDtoValidador>();


        return services;
    }
}
