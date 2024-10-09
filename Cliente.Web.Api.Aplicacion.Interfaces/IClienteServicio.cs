using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using Cliente.Web.Api.Transversal.Modelos;

namespace Cliente.Web.Api.Aplicacion.Interfaces
{
    public interface IClienteServicio
    {
        #region Metodos Asincronos

        Task<Response<bool>> RegistrarCliente(ClientePersonaDto ClienteDto);
        Task<Response<bool>> ActualizarCliente(long idCliente,ActualizarClientePersonaDto ClienteDto);
        Task<Response<bool>> EliminarCliente(long IdCliente);
        Task<Response<ClienteDto>> ObtenerCliente(long IdCliente);
        Task<Response<IEnumerable<ClienteDto>>> ObtenerTodosLosClientes();
        Task<ResponsePagination<IEnumerable<ClienteDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina);

        #endregion

    }
}
