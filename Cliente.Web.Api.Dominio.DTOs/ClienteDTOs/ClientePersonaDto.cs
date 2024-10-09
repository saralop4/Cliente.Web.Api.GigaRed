namespace Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
public class ClientePersonaDto
{
    // Propiedades de Persona
    public long IdIndicativo { get; set; }
    public long IdCiudad { get; set; }
    public string PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; } 
    public string PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; } 
    public string Telefono { get; set; }
    public byte[]? Foto { get; set; } 
    public string? NombreFoto { get; set; }
    public string UsuarioQueRegistraPersona { get; set; }
    public string IpDeRegistroPersona { get; set; }

    // Propiedades de Cliente
    public string UsuarioQueRegistraCliente { get; set; }
    public string IpDeRegistroCliente { get; set; }
}
