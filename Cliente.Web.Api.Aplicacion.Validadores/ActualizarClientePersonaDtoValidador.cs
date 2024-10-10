using Cliente.Web.Api.Dominio.DTOs.ClienteDTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cliente.Web.Api.Aplicacion.Validadores;

public class ActualizarClientePersonaDtoValidador : AbstractValidator<ActualizarClientePersonaDto>
{
    public ActualizarClientePersonaDtoValidador()
    {
        RuleFor(u => u.IdCliente)
            .NotEmpty().WithMessage("El idCliente no puede estar vacio.")
            .NotNull().WithMessage("El idCliente no puede ser nulo.")
            .Must(SoloNumerosLong).WithMessage("El idCliente solo puede contener números.")
            .GreaterThan(0).WithMessage("Debe proporcionar un idCliente válido y mayor que 0."); ;

        RuleFor(u => u.IdPersona)
            .NotEmpty().WithMessage("El idPersona no puede estar vacio.")
            .NotNull().WithMessage("El idPersona no puede ser nulo.")
            .Must(SoloNumerosLong).WithMessage("El idPersona solo puede contener números.")
            .GreaterThan(0).WithMessage("Debe proporcionar un idPersona válido y mayor que 0.");


        RuleFor(u => u.PrimerNombre)
           .NotEmpty().WithMessage("El primer nombre no puede estar vacio.")
           .NotNull().WithMessage("El primer nombre no puede ser nulo.")
           .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El primer nombre solo puede contener letras.");

        RuleFor(u => u.SegundoNombre)
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El segundo nombre solo puede contener letras.");

        RuleFor(u => u.PrimerApellido)
            .NotEmpty().WithMessage("El primer apellido no puede estar vacio.")
            .NotNull().WithMessage("El primer apellido no puede ser nulo.")
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El primer apellido solo puede contener letras.");

        RuleFor(u => u.SegundoApellido)
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$").WithMessage("El segundo apellido solo puede contener letras.");

        RuleFor(u => u.Telefono)
            .NotEmpty().WithMessage("El telefono no puede estar vacio.")
            .NotNull().WithMessage("El telefono no puede ser nulo.")
            .Must(SoloNumeros).WithMessage("El telefono solo puede contener números.")
            .Length(7, 15).WithMessage("El telefono debe tener entre 7 y 15 dígitos.");

        RuleFor(u => u.UsuarioQueActualizaPersona)
            .NotEmpty().WithMessage("El usuario que actualiza persona es obligatorio.")
            .NotNull().WithMessage("El usuario que actualiza persona no puede ser nulo.")
            .MaximumLength(80).WithMessage("El usuario que actualiza persona no puede tener más de 80 caracteres.")
            .Must(CorreoValido).WithMessage("El usuario que actualiza persona debe tener un formato válido. (ejemplo@dominio.com)");

        RuleFor(u => u.FechaDeActualizadoPersona)
            .NotEmpty().WithMessage("La fecha de actualizado persona es obligatorio.")
            .NotNull().WithMessage("La fecha de actualizado persona no puede ser nulo.");

        RuleFor(u => u.HoraDeActualizadoPersona)
            .NotEmpty().WithMessage("La hora de actualizado persona es obligatorio.")
            .NotNull().WithMessage("La hora de actualizado persona no puede ser nulo.");

        RuleFor(u => u.IpDeActualizadoPersona)
            .NotEmpty().WithMessage("La ip de actualizado persona es obligatorio.")
            .NotNull().WithMessage("La ip de actualizado persona no puede ser nulo.");

        RuleFor(u => u.UsuarioQueActualizaCliente)
           .NotEmpty().WithMessage("El usuario que actualiza cliente es obligatorio.")
           .NotNull().WithMessage("El usuario que actualiza cliente no puede ser nulo.")
           .MaximumLength(80).WithMessage("El usuario que actualiza cliente no puede tener más de 80 caracteres.")
           .Must(CorreoValido).WithMessage("El usuario que actualiza cliente debe tener un formato válido. (ejemplo@dominio.com)");

        RuleFor(u => u.FechaDeActualizadoCliente)
           .NotEmpty().WithMessage("La fecha de actualizado cliente es obligatorio.")
           .NotNull().WithMessage("La fecha de actualizado cliente no puede ser nulo.");

        RuleFor(u => u.HoraDeActualizadoCliente)
            .NotEmpty().WithMessage("La hora de actualizado cliente es obligatorio.")
            .NotNull().WithMessage("La hora de actualizado cliente no puede ser nulo.");

        RuleFor(u => u.IpDeActualizadoCliente)
            .NotEmpty().WithMessage("La ip de actualizado cliente es obligatorio.")
            .NotNull().WithMessage("La ip de actualizado cliente no puede ser nulo.");


    }

    private bool SoloNumeros(string telefono)
    {
        // Aseguramos que el teléfono contenga solo dígitos
        return telefono.All(char.IsDigit);
    }
    private bool SoloNumerosLong(long telefono)
    {
        return telefono.ToString().All(char.IsDigit);
    }

    private bool CorreoValido(string correo)
    {
        // Expresión regular para validar el formato del correo
        var correoValido = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(correo, correoValido);
    }
}
