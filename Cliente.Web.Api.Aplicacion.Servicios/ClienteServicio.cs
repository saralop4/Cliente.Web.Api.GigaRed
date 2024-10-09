using Cliente.Web.Api.Aplicacion.Interfaces;
using Cliente.Web.Api.Aplicacion.Validadores;
using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using Cliente.Web.Api.Dominio.Interfaces;
using Cliente.Web.Api.Transversal.Modelos;
using FluentValidation;

namespace Cliente.Web.Api.Aplicacion.Servicios;
public class ClienteServicio : IClienteServicio
{
    private readonly IClienteRepositorio _ClienteRepositorio;
    private readonly ClientePersonaDtoValidador _ClientePersonaDtoValidador;
    private readonly ActualizarClientePersonaDtoValidador _ActualizarClientePersonaDtoValidador;
    public ClienteServicio(IClienteRepositorio ClienteRepositorio, ActualizarClientePersonaDtoValidador ActualizarClientePersonaDtoValidador, ClientePersonaDtoValidador ClientePersonaDtoValidador)
    {
        _ClienteRepositorio = ClienteRepositorio;
        _ClientePersonaDtoValidador = ClientePersonaDtoValidador;
        _ActualizarClientePersonaDtoValidador = ActualizarClientePersonaDtoValidador;
    }

    public async Task<Response<bool>> ActualizarCliente(ActualizarClientePersonaDto ClienteDto)
    {
        var response = new Response<bool>();
        var validation = _ActualizarClientePersonaDtoValidador.Validate(new ActualizarClientePersonaDto()
        {
            IdCliente = ClienteDto.IdCliente,
            UsuarioQueActualizaCliente = ClienteDto.UsuarioQueActualizaCliente,
            FechaDeActualizadoCliente = ClienteDto.FechaDeActualizadoCliente,
            HoraDeActualizadoCliente = ClienteDto.HoraDeActualizadoCliente,
            IpDeActualizadoCliente = ClienteDto.IpDeActualizadoCliente,
            IdPersona = ClienteDto.IdPersona,
            IdIndicativo = ClienteDto.IdIndicativo,
            IdCiudad = ClienteDto.IdCiudad,
            PrimerNombre = ClienteDto.PrimerNombre,
            SegundoNombre = ClienteDto.SegundoNombre,
            PrimerApellido = ClienteDto.PrimerApellido,
            SegundoApellido = ClienteDto.SegundoApellido,
            Telefono = ClienteDto.Telefono,
            UsuarioQueActualizaPersona = ClienteDto.UsuarioQueActualizaPersona,
            FechaDeActualizadoPersona = ClienteDto.FechaDeActualizadoPersona,
            HoraDeActualizadoPersona = ClienteDto.HoraDeActualizadoPersona,
            IpDeActualizadoPersona = ClienteDto.IpDeActualizadoPersona

        });

        if (!validation.IsValid)
        {
            response.IsSuccess = false;
            response.Message = "Errores de validación encontrados";
            response.Errors = validation.Errors;
            return response;

        }

        var Cliente = await _ClienteRepositorio.ActualizarClientePersona(ClienteDto);

        if (Cliente is {}) // no es nulo
        {
            response.Data = Cliente;
            response.IsSuccess = true;
            response.Message = "Actualizacion exitosa!!";
        }
        else
        {
            response.IsSuccess = false;
            response.Message = "Hubo error al actualizar el registro";
        }
        return response;
    }

    public async Task<Response<bool>> DeleteCliente(long IdCliente)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = await _ClienteRepositorio.Eliminar(IdCliente);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Elimiacion Exitosa!!";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al eliminar el registro";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
        }
        return response;
    }

    public async Task<Response<ClienteDto>> ObtenerCliente(long IdCliente)
    {
        var response = new Response<ClienteDto>();

        try
        {
            var Cliente = await _ClienteRepositorio.ObtenerClientePersona(IdCliente);
           
            if (Cliente != null)
            {
                response.Data = Cliente;    
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al consultar el registro";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
        }
        return response;
    }

    public async Task<ResponsePagination<IEnumerable<ClienteDto>>> ObtenerTodoConPaginación(int NumeroDePagina, int TamañoDePagina)
    {
        var response = new ResponsePagination<IEnumerable<ClienteDto>>();

        try
        {
          //  var Contar = await _ClienteRepositorio.Contar();
            var Clientes = await _ClienteRepositorio.ObtenerTodoConPaginacionClientePersona(NumeroDePagina, TamañoDePagina);

            if (Clientes != null)
            {
                response.NumeroDePagina = NumeroDePagina;
             //   response.TotalPaginas = (int)Math.Ceiling(Contar / (double)TamañoDePagina);
             //   response.CantidadTotal = Contar;
                response.IsSuccess = true;
                response.Message = "Consulta Paginada Exitosa!!!";

            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<IEnumerable<ClienteDto>>> ObtenerTodosLosClientes()
    {
        var Response = new Response<IEnumerable<ClienteDto>>();

        try
        {
            var Clientes = await _ClienteRepositorio.ObtenerTodoClientePersona();
           
            if (Clientes != null)
            {
                Response.Data= Clientes;
                Response.IsSuccess = true;
                Response.Message = "Consulta Exitosa!!";
            }
            else
            {
                Response.IsSuccess = false;
                Response.Message = "Hubo error al obtener los registros";
            }
        }  
        catch (Exception ex)
        {
            Response.IsSuccess = false;
            Response.Message = $"Ocurrió un error: {ex.Message}";
        }
        
        return Response;
    }

    public async Task<Response<bool>> RegistrarCliente(ClientePersonaDto ClienteDto)
    {
        var response = new Response<bool>();

        try
        {
            var validation = _ClientePersonaDtoValidador.Validate(new ClientePersonaDto()
            {
                IdIndicativo = ClienteDto.IdIndicativo,
                IdCiudad = ClienteDto.IdCiudad,
                PrimerNombre = ClienteDto.PrimerNombre,
                PrimerApellido = ClienteDto.PrimerApellido,
                Telefono = ClienteDto.Telefono,
                UsuarioQueRegistraPersona = ClienteDto.UsuarioQueRegistraPersona,
                IpDeRegistroPersona = ClienteDto.IpDeRegistroPersona,
                UsuarioQueRegistraCliente = ClienteDto.UsuarioQueRegistraCliente,
                IpDeRegistroCliente = ClienteDto.IpDeRegistroCliente

            });

            if (!validation.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación encontrados";
                response.Errors = validation.Errors;
                return response;

            }

            var Cliente = await _ClienteRepositorio.RegistrarClienteYPersona(ClienteDto);

            if (Cliente is {}) // no es nulo
            {
                response.Data = Cliente;
                response.IsSuccess = true;
                response.Message = "Registro exitoso!!";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Hubo error al crear el registro";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
        }
       
        return response;
    }
}
