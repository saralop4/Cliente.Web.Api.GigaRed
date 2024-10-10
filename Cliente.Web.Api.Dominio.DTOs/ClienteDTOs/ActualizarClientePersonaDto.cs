using System.Text.Json.Serialization;

namespace Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;

public class ActualizarClientePersonaDto
{
    // Propiedades del Cliente

    [JsonPropertyName("IdCliente")]
    public long IdCliente { get; set; }

    [JsonPropertyName("UsuarioQueActualizaCliente")]
    public string? UsuarioQueActualizaCliente { get; set; }

    [JsonPropertyName("FechaDeActualizadoCliente")]
    public DateTime FechaDeActualizadoCliente { get; set; }

    [JsonPropertyName("HoraDeActualizadoCliente")]
    public TimeSpan HoraDeActualizadoCliente { get; set; }

    [JsonPropertyName("IpDeActualizadoCliente")]
    public string? IpDeActualizadoCliente { get; set; }

    // Propiedades de la Persona
    [JsonPropertyName("IdPersona")]
    public long IdPersona { get; set; }

    [JsonPropertyName("IdIndicativo")]
    public long IdIndicativo { get; set; }

    [JsonPropertyName("IdCiudad")]
    public long IdCiudad { get; set; }

    [JsonPropertyName("PrimerNombre")]
    public string PrimerNombre { get; set; }

    [JsonPropertyName("SegundoNombre")]
    public string? SegundoNombre { get; set; }

    [JsonPropertyName("PrimerApellido")]
    public string PrimerApellido { get; set; }

    [JsonPropertyName("SegundoApellido")]
    public string? SegundoApellido { get; set; }

    [JsonPropertyName("Telefono")]
    public string Telefono { get; set; }

    [JsonPropertyName("UsuarioQueActualizaPersona")]
    public string? UsuarioQueActualizaPersona { get; set; }

    [JsonPropertyName("FechaDeActualizadoPersona")]
    public DateTime FechaDeActualizadoPersona { get; set; }

    [JsonPropertyName("HoraDeActualizadoPersona")]
    public TimeSpan HoraDeActualizadoPersona { get; set; }

    [JsonPropertyName("IpDeActualizadoPersona")]
    public string? IpDeActualizadoPersona { get; set; }
}
