using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using Cliente.Web.Api.Dominio.Interfaces;
using Cliente.Web.Api.Dominio.Persistencia;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Cliente.Web.Api.Infraestructura.Repositorios;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly DapperContext _context;

    public ClienteRepositorio(IConfiguration configuration)
    {
        _context = new DapperContext(configuration);

    }

    public async Task<bool> ActualizarClientePersona(ActualizarClientePersonaDto modelo)
    {
        using (var conexion = _context.CreateConnection())
        {
            var query = "ActualizarClienteYPersona"; 
            var parameters = new DynamicParameters();

            // Parámetros de Cliente
            parameters.Add("IdCliente", modelo.IdCliente);
            parameters.Add("UsuarioQueActualizaCliente", modelo.UsuarioQueActualizaCliente);
            parameters.Add("FechaDeActualizadoCliente", modelo.FechaDeActualizadoCliente);
            parameters.Add("HoraDeActualizadoCliente", modelo.HoraDeActualizadoCliente);
            parameters.Add("IpDeActualizadoCliente", modelo.IpDeActualizadoCliente);

            // Parámetros de Persona
            parameters.Add("IdPersona", modelo.IdPersona);
            parameters.Add("IdIndicativo", modelo.IdIndicativo);
            parameters.Add("IdCiudad", modelo.IdCiudad);
            parameters.Add("PrimerNombre", modelo.PrimerNombre);
            parameters.Add("SegundoNombre", modelo.SegundoNombre);
            parameters.Add("PrimerApellido", modelo.PrimerApellido);
            parameters.Add("SegundoApellido", modelo.SegundoApellido);
            parameters.Add("Telefono", modelo.Telefono);
            parameters.Add("UsuarioQueActualizaPersona", modelo.UsuarioQueActualizaPersona);
            parameters.Add("FechaDeActualizadoPersona", modelo.FechaDeActualizadoPersona);
            parameters.Add("HoraDeActualizadoPersona", modelo.HoraDeActualizadoPersona);
            parameters.Add("IpDeActualizadoPersona", modelo.IpDeActualizadoPersona);

            // Ejecutar el procedimiento almacenado
            var result = await conexion.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public async Task<bool> Eliminar(long Id)
    {
        using (var conexion = _context.CreateConnection())
        {

            var query = "EliminarClienteYPersona";
            var parameters = new DynamicParameters();
            parameters.Add("IdCliente", Id);
            parameters.Add("EstadoEliminado",true);

            var result = await conexion.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
    }

    public async Task<ClienteDto> ObtenerClientePersona(long Id)
    {
        using (var Conexion = _context.CreateConnection())
        {
            var Query = "ObtenerClientePorId";
            var Parameters = new DynamicParameters();
            Parameters.Add("IdCliente", Id);

            var ClientePersona = await Conexion.QuerySingleOrDefaultAsync<ClienteDto>(Query, param: Parameters, commandType: CommandType.StoredProcedure);

            return ClientePersona;
        }
    }


    public async Task<IEnumerable<ClienteDto>> ObtenerTodoClientePersona()
    {
        using (var conexion = _context.CreateConnection())
        {
            var query = "ObtenerTodosLosClientes";

            var result = await conexion.QueryAsync<ClienteDto>(query, commandType: CommandType.StoredProcedure);

            return result;
        }
    }


    public async Task<IEnumerable<ClienteDto>> ObtenerTodoConPaginacionClientePersona(int NumeroDePagina, int TamañoPagina)
    {
        using (var conexion = _context.CreateConnection())
        {
            var query = "ClientePersonaListaConPaginacion";
            var parameters = new DynamicParameters();

            parameters.Add("NumeroDePagina", NumeroDePagina);//el numero de la pagina a mostrar
            parameters.Add("TamañoPagina", TamañoPagina); //la cantidad de registros que se van a mostrar

            var result = await conexion.QueryAsync<ClienteDto>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }

    public async Task<bool> RegistrarClienteYPersona(ClientePersonaDto modelo)
    {
        using (var conexion = _context.CreateConnection())
        {
            var query = "RegistrarClienteYPersona"; 
            var parameters = new DynamicParameters();

            // Parámetros para la Persona
            parameters.Add("IdIndicativo", modelo.IdIndicativo);
            parameters.Add("IdCiudad", modelo.IdCiudad);
            parameters.Add("PrimerNombre", modelo.PrimerNombre);
            parameters.Add("SegundoNombre", modelo.SegundoNombre);
            parameters.Add("PrimerApellido", modelo.PrimerApellido);
            parameters.Add("SegundoApellido", modelo.SegundoApellido);
            parameters.Add("Telefono", modelo.Telefono);
            parameters.Add("Foto", modelo.Foto);
            parameters.Add("NombreFoto", modelo.NombreFoto);
            parameters.Add("UsuarioQueRegistraPersona", modelo.UsuarioQueRegistraPersona);
            parameters.Add("IpDeRegistroPersona", modelo.IpDeRegistroPersona);

            // Parámetros para el Cliente
            parameters.Add("UsuarioQueRegistraCliente", modelo.UsuarioQueRegistraCliente);
            parameters.Add("IpDeRegistroCliente", modelo.IpDeRegistroCliente);

            // Ejecutar el procedimiento almacenado
            var result = await conexion.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

}
