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

    public async Task<Response<bool>> ActualizarCliente(long idCliente, ActualizarClientePersonaDto clienteDto)
    {
        var response = new Response<bool>();
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

        var ClientePersonaExistente = ObtenerCliente(idCliente);

        if (ClientePersonaExistente == null) 
        {
            response.IsSuccess = false;
            response.Message = "El Cliente a actualizar no existe";
            return response;

        }

        var Cliente = await _ClienteRepositorio.ActualizarClientePersona(clienteDto);

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

    public async Task<Response<bool>> EliminarCliente(long idCliente)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = await _ClienteRepositorio.Eliminar(idCliente);
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

    public async Task<Response<ClienteDto>> ObtenerCliente(long idCliente)
    {
        var response = new Response<ClienteDto>();

        try
        {
            var cliente = await _ClienteRepositorio.ObtenerClientePersona(idCliente);
           
            if (cliente != null)
            {
                response.Data = cliente;    
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
