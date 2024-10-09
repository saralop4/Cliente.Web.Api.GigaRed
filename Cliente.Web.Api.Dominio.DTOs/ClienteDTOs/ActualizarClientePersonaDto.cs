namespace Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;

public class ActualizarClientePersonaDto
{
    // Propiedades del Cliente
    public long IdCliente { get; set; }
    public string? UsuarioQueActualizaCliente { get; set; }
    public DateTime FechaDeActualizadoCliente { get; set; }
    public TimeSpan HoraDeActualizadoCliente { get; set; }
    public string? IpDeActualizadoCliente { get; set; }

    // Propiedades de la Persona
    public long IdPersona { get; set; }
    public long IdIndicativo { get; set; }
    public long IdCiudad { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; } 
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; } 
    public string? Telefono { get; set; }
    public string? UsuarioQueActualizaPersona { get; set; }
    public DateTime FechaDeActualizadoPersona { get; set; }
    public TimeSpan HoraDeActualizadoPersona { get; set; }
    public string? IpDeActualizadoPersona { get; set; }
}
