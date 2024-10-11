using Cliente.Web.Api.Dominio.DTOs;

namespace Cliente.Web.Api.Dominio.Interfaces;
public interface IIndicativoRepositorio
{
    Task<IEnumerable<IndicativoDto>> ObtenerTodo();


}
