using Cliente.Web.Api.Dominio.DTOs;

namespace Cliente.Web.Api.Dominio.Interfaces;
public interface ICiudadRepositorio
{
    Task<IEnumerable<CiudadDto>> ObtenerTodo();    
}
