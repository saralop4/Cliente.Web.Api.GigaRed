using Cliente.Web.Api.Aplicacion.Interfaces;
using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Web.Api.Controllers.V1;

[Authorize]
[Route("Api/V{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ClienteController : ControllerBase
{
    private readonly IClienteServicio _clienteServicio;
    public ClienteController(IClienteServicio clienteServicio)
    {
        _clienteServicio = clienteServicio;

    }

    #region Metodos Asincronos*

    [HttpPost("GuardarCliente")]
    public async Task<IActionResult> GuardarCliente([FromBody] ClientePersonaDto ClienteDto)
    {
        var ipDeRegistro = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (ipDeRegistro != null)
        {
            ClienteDto.IpDeRegistroPersona = ipDeRegistro.ToString();
            ClienteDto.IpDeRegistroCliente = ipDeRegistro.ToString();
        }

        var Response = await _clienteServicio.RegistrarCliente(ClienteDto);

        if (Response.IsSuccess)
        {
            return Ok( Response);
        }
        return BadRequest(Response);


    }

    [HttpPut("ActualizarCliente/{IdCliente}")]
    public async Task<IActionResult> ActualizarCliente(long IdCliente, [FromBody] ActualizarClientePersonaDto ClienteDto)
    {
        var ipDeActualizado = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (ipDeActualizado != null)
        {
            ClienteDto.IpDeActualizadoPersona = ipDeActualizado.ToString();
            ClienteDto.IpDeActualizadoCliente = ipDeActualizado.ToString();
        }

        var Response = await _clienteServicio.ActualizarCliente(IdCliente,ClienteDto);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);
        
    }

    [HttpDelete("EliminarCliente/{IdCliente}")]
    public async Task<IActionResult> EliminarCliente(long IdCliente)
    {

        var Response = await _clienteServicio.EliminarCliente(IdCliente);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);


    }

    [HttpGet("ObtenerClientePorId/{IdCliente}")]
    public async Task<IActionResult> ObtenerClientePorId(long IdCliente)
    {
        var Response = await _clienteServicio.ObtenerCliente(IdCliente);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);


    }

    [HttpGet("ObtenerTodoLosClientes")]
    public async Task<IActionResult> ObtenerTodoLosClientes()
    {

        var Response = await _clienteServicio.ObtenerTodosLosClientes();

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);

    }

    [HttpGet("ObtenerTodoClienteConPaginacion/{NumeroPagina}/{amTañoPagina}")]
    public async Task<IActionResult> ObtenerTodoClienteConPaginacion(int NumeroPagina, int TamañoPagina)
    {

        var Response = await _clienteServicio.ObtenerTodoConPaginación(NumeroPagina, TamañoPagina);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response);

    }

    #endregion


}
