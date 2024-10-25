using Cliente.Web.Api.Aplicacion.Interfaces;
using Cliente.Web.Api.Aplicacion.Validadores;
using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using Cliente.Web.Api.Dominio.Interfaces;
using Cliente.Web.Api.Transversal.Modelos;

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

    public async Task<Response<bool>> ActualizarCliente(long idCliente, ActualizarClientePersonaDto clienteDto)
    {
        var response = new Response<bool>();

        if (idCliente == 0)
        {
            response.IsSuccess = false;
            response.Message = "Debe proporcionar el id del cliente a actualizar.";
            return response;
        }

        var validation = _ActualizarClientePersonaDtoValidador.Validate(new ActualizarClientePersonaDto()
        {
            IdCliente = clienteDto.IdCliente,
            UsuarioQueActualizaCliente = clienteDto.UsuarioQueActualizaCliente,
            FechaDeActualizadoCliente = clienteDto.FechaDeActualizadoCliente,
            HoraDeActualizadoCliente = clienteDto.HoraDeActualizadoCliente,
            IpDeActualizadoCliente = clienteDto.IpDeActualizadoCliente,
            IdPersona = clienteDto.IdPersona,
            IdIndicativo = clienteDto.IdIndicativo,
            IdCiudad = clienteDto.IdCiudad,
            PrimerNombre = clienteDto.PrimerNombre,
            SegundoNombre = clienteDto.SegundoNombre,
            PrimerApellido = clienteDto.PrimerApellido,
            SegundoApellido = clienteDto.SegundoApellido,
            Telefono = clienteDto.Telefono,
            UsuarioQueActualizaPersona = clienteDto.UsuarioQueActualizaPersona,
            FechaDeActualizadoPersona = clienteDto.FechaDeActualizadoPersona,
            HoraDeActualizadoPersona = clienteDto.HoraDeActualizadoPersona,
            IpDeActualizadoPersona = clienteDto.IpDeActualizadoPersona

        });

        if (!validation.IsValid)
        {
            response.IsSuccess = false;
            response.Message = "Errores de validación encontrados";
            response.Errors = validation.Errors;
            return response;

        }

        try
        {
            var ClientePersonaExistente = ObtenerCliente(idCliente);

            if (ClientePersonaExistente == null)
            {
                response.IsSuccess = false;
                response.Message = "El cliente a actualizar no existe";
                return response;

            }

            var Cliente = await _ClienteRepositorio.ActualizarClientePersona(clienteDto);

            if (Cliente is { }) // no es nulo
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

        }
        catch (Exception ex) 
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrio un Error: {ex}";
        }
        return response;
    }

    public async Task<Response<bool>> EliminarCliente(long idCliente)
    {
        var response = new Response<bool>();

        if (idCliente == 0)
        {
            response.IsSuccess = false;
            response.Message = "Debe proporcionar el id del cliente a eliminar.";
            return response;
        }

        try
        {
            response.Data = await _ClienteRepositorio.Eliminar(idCliente);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Elimiacion exitosa!!";
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

    public async Task<Response<ClienteDto>> ObtenerCliente(long idCliente)
    {
        var response = new Response<ClienteDto>();

        if (idCliente == 0)
        {
            response.IsSuccess = false;
            response.Message = "Debe proporcionar el id del cliente a consultar.";
            return response;
        }

        try
        {
            var cliente = await _ClienteRepositorio.ObtenerClientePersona(idCliente);
           
            if (cliente != null)
            {
                response.Data = cliente;    
                response.IsSuccess = true;
                response.Message = "Consulta exitosa!!";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "El cliente no existe";
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
                response.Message = "Consulta paginada exitosa!!!";

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
                Response.Message = "Consulta exitosa!!";
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
                SegundoNombre = ClienteDto.SegundoNombre,
                PrimerApellido = ClienteDto.PrimerApellido,
                SegundoApellido = ClienteDto.SegundoApellido,
                Telefono = ClienteDto.Telefono,
                UsuarioQueRegistraPersona = ClienteDto.UsuarioQueRegistraPersona,
                UsuarioQueRegistraCliente = ClienteDto.UsuarioQueRegistraCliente

            });

            if (!validation.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación encontrados";
                response.Errors = validation.Errors;
                return response;

            }

            var Cliente = await _ClienteRepositorio.RegistrarClientePersona(ClienteDto);

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
