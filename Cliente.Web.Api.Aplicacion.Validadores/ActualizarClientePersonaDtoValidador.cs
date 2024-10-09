using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cliente.Web.Api.Aplicacion.Validadores;

public class ActualizarClientePersonaDtoValidador : AbstractValidator<ActualizarClientePersonaDto>
{
    public ActualizarClientePersonaDtoValidador()
    {
        RuleFor(u => u.PrimerNombre)
           .NotEmpty().WithMessage("El primer nombre es obligatorio.")
           .NotNull().WithMessage("El primer nombre no puede ser nulo.");

        RuleFor(u => u.PrimerApellido)
            .NotEmpty().WithMessage("El primer apellido es obligatorio.")
            .NotNull().WithMessage("El primer apellido no puede ser nulo.");

        RuleFor(u => u.UsuarioQueActualizaPersona)
            .NotEmpty().WithMessage("El usuario que registra es obligatorio.")
            .NotNull().WithMessage("El usuario que registra no puede ser nulo.")
            .MaximumLength(80).WithMessage("El usuario que registra no puede tener más de 80 caracteres.")
            .Must(CorreoValido).WithMessage("El usuario que registra debe tener un formato válido. (ejemplo@dominio.com)");

    }

    private bool CorreoValido(string correo)
    {
        // Expresión regular para validar el formato del correo
        var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(correo, correoValido);
    }
}

