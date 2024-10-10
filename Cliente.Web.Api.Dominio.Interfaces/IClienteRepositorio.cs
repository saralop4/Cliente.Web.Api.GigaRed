using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;

namespace Cliente.Web.Api.Dominio.Interfaces;

public interface IClienteRepositorio 
{
    Task<bool> Eliminar(long Id);
    Task<bool> RegistrarClientePersona(ClientePersonaDto modelo);
    Task<bool> ActualizarClientePersona(ActualizarClientePersonaDto Modelo);
    Task<ClienteDto> ObtenerClientePersona(long Id);
    Task<IEnumerable<ClienteDto>> ObtenerTodoClientePersona();
    Task<IEnumerable<ClienteDto>> ObtenerTodoConPaginacionClientePersona(int NumeroDePagina, int TamañoPagina);

}
