using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cliente.Web.Api.Aplicacion.Validadores;
public class ClientePersonaDtoValidador : AbstractValidator<ClientePersonaDto>
{

    public ClientePersonaDtoValidador()
    {

        RuleFor(u => u.IdIndicativo)
            .NotEmpty().WithMessage("Debe seleccionar el indicativo.")
            .NotNull().WithMessage("El indicativo no puede ser nulo.");

        RuleFor(u => u.IdCiudad)
            .NotEmpty().WithMessage("Debe seleccionar la ciudad.")
            .NotNull().WithMessage("El indicativo no puede ser nulo.");

        RuleFor(u => u.PrimerNombre)
            .NotEmpty().WithMessage("El primer nombre es obligatorio.")
            .NotNull().WithMessage("El primer nombre no puede ser nulo.")
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El primer nombre solo puede contener letras.");

        RuleFor(u => u.SegundoNombre)
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El segundo nombre solo puede contener letras.");

        RuleFor(u => u.PrimerApellido)
            .NotEmpty().WithMessage("El primer apellido es obligatorio.")
            .NotNull().WithMessage("El primer apellido no puede ser nulo.")
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El primer apellido solo puede contener letras.");

        RuleFor(u => u.SegundoApellido)
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El segundo apellido solo puede contener letras.");

        RuleFor(u => u.Telefono)
            .NotEmpty().WithMessage("El telefono es obligatorio.")
            .NotNull().WithMessage("El telefono no puede ser nulo.")
            .Must(SoloNumeros).WithMessage("El telefono solo puede contener números.");

        RuleFor(u => u.UsuarioQueRegistraPersona)
            .NotEmpty().WithMessage("El usuario que registra persona es obligatorio.")
            .NotNull().WithMessage("El usuario que registra persona no puede ser nulo.")
            .MaximumLength(80).WithMessage("El usuario que registra no puede tener más de 80 caracteres.")
            .Must(CorreoValido).WithMessage("El usuario que registra debe tener un formato válido. (ejemplo@dominio.com)");

         RuleFor(u => u.UsuarioQueRegistraCliente)
            .NotEmpty().WithMessage("El usuario que registra cliente es obligatorio.")
            .NotNull().WithMessage("El usuario que registra cliente no puede ser nulo.")
            .MaximumLength(80).WithMessage("El usuario que registra cliente no puede tener más de 80 caracteres.")
            .Must(CorreoValido).WithMessage("El usuario que registra cliente debe tener un formato válido. (ejemplo@dominio.com)");

    }

    private bool CorreoValido(string correo)
    {
        // Expresión regular para validar el formato del correo
        var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(correo, correoValido);
    }
    private bool SoloNumeros(string telefono)
    {
        // Aseguramos que el teléfono contenga solo dígitos
        return telefono.All(char.IsDigit);
    }

}
