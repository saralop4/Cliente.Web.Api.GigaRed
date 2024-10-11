using Cliente.Web.Api.Dominio.DTOs;
using Cliente.Web.Api.Transversal.Modelos;

namespace Cliente.Web.Api.Aplicacion.Interfaces;

public interface IIndicativoServicio
{
    Task<Response<IEnumerable<IndicativoDto>>> ObtenerTodos();

}
