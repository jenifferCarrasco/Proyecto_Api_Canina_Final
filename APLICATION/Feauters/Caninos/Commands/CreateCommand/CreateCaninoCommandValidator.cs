using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Canino.Commands.CreateCommand
{
    public class CreateCaninoCommandValidator : AbstractValidator<CreateCaninoCommand>
    {
        public CreateCaninoCommandValidator()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Raza)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.FechaNacimiento)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");
            RuleFor(p => p.Peso)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Color)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Sexo)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(p => p.PropietarioId)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(p => p.Estatus)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");



        }
    }
}
