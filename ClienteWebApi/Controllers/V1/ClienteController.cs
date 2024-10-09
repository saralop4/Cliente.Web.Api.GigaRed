using Cliente.Web.Api.Aplicacion.Interfaces;
using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Web.Api.Controllers.V1;

//[Authorize]
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

    [HttpPost("Guardar")]
    public async Task<IActionResult> Guardar([FromBody] ClientePersonaDto clienteDto)
    {
        if (clienteDto == null)
        {
            return BadRequest();
        }
        var Response = await _clienteServicio.RegistrarCliente(clienteDto);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response.Message);


    }

    [HttpPut("Actualizar/{idCliente}")]
    public async Task<IActionResult> Actualizar(long idCliente, [FromBody] ActualizarClientePersonaDto clienteDto)
    {
                
        if (clienteDto == null)
        {
            return BadRequest();
        }
        var Response = await _clienteServicio.ActualizarCliente(idCliente,clienteDto);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response.Message);


    }

    [HttpDelete("Eliminar")]
    public async Task<IActionResult> Eliminar(long idCliente)
    {
        if (idCliente == 0)
        {
            return BadRequest();
        }
        var Response = await _clienteServicio.EliminarCliente(idCliente);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response.Message);


    }

    [HttpGet("Obtener")]
    public async Task<IActionResult> Obtener(long idCliente)
    {
        if (idCliente == 0)
        {
            return BadRequest();
        }
        var Response = await _clienteServicio.ObtenerCliente(idCliente);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response.Message);


    }

    [HttpGet("ObtenerTodo")]
    public async Task<IActionResult> ObtenerTodo()
    {

        var Response = await _clienteServicio.ObtenerTodosLosClientes();

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response.Message);


    }

    [HttpGet("ObtenerTodoConPaginacion")]
    public async Task<IActionResult> ObtenerTodoConPaginacion([FromQuery] int numeroPagina, int tamañoPagina)
    {

        var Response = await _clienteServicio.ObtenerTodoConPaginación(numeroPagina, tamañoPagina);

        if (Response.IsSuccess)
        {
            return Ok(Response);
        }
        return BadRequest(Response.Message);


    }

    #endregion


}
