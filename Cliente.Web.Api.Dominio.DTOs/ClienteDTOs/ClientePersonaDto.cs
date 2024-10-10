using System.Text.Json.Serialization;

namespace Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
public class ClientePersonaDto
{
    // Propiedades de Persona

    [JsonPropertyName("IdIndicativo")]
    public long IdIndicativo { get; set; }

    [JsonPropertyName("IdCiudad")]
    public long IdCiudad { get; set; }

    [JsonPropertyName("PrimerNombre")]
    public string PrimerNombre { get; set; }

    [JsonPropertyName("SegundoNombre")]
    public string? SegundoNombre { get; set; } = null;

    [JsonPropertyName("PrimerApellido")]
    public string PrimerApellido { get; set; }

    [JsonPropertyName("SegundoApellido")]
    public string? SegundoApellido { get; set; } = null;

    [JsonPropertyName("Telefono")]
    public string Telefono { get; set; }

    [JsonPropertyName("NombreFoto")]
    public string? NombreFoto { get; set; } = null;

    [JsonPropertyName("UsuarioQueRegistraPersona")]
    public string UsuarioQueRegistraPersona { get; set; }

    [JsonPropertyName("IpDeRegistroPersona")]
    public string IpDeRegistroPersona { get; set; }

    // Propiedades de Cliente
    [JsonPropertyName("UsuarioQueRegistraCliente")]
    public string UsuarioQueRegistraCliente { get; set; }

    [JsonPropertyName("IpDeRegistroCliente")]
    public string IpDeRegistroCliente { get; set; }
}
