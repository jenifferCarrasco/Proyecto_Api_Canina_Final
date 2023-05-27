using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace APLICATION.Feauters.Authenticate.Command.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() {

            RuleFor(p => p.Nombre)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Apellido)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            //RuleFor(p => p.Direccion)
            //       .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
            //       .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            //RuleFor(p => p.Cedula)
            //        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
            //        .MaximumLength(13).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Email)
                     .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                     .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de correo valida")
                     .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.UserName)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Password)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.ConfirmPassword)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter")
                    .Equal(p => p.Password).WithMessage("{PropertyName} debe ser igual al password");
        }
    }
}
