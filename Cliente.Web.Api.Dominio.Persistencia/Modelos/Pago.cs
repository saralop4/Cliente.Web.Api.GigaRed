using System;
using System.Collections.Generic;

namespace Cliente.Web.Api.Dominio.Persistencia.Modelos;

public partial class Pago
{
    public long IdPago { get; set; }

    public long IdCliente { get; set; }

    public DateOnly FechaDeCorte { get; set; }

    public DateOnly? FechaDePago { get; set; }

    public decimal Monto { get; set; }

    public bool? Estado { get; set; }

    public string UsuarioQueRegistra { get; set; } = null!;

    public string? UsuarioQueActualiza { get; set; }

    public DateOnly FechaDeRegistro { get; set; }

    public TimeOnly HoraDeRegistro { get; set; }

    public string IpDeRegistro { get; set; } = null!;

    public DateOnly? FechaDeActualizado { get; set; }

    public TimeOnly? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
