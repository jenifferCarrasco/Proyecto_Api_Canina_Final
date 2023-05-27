using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Feauters.Centros.Commands.CreateCentroCommand
{

    public class CreateCentroCommandValidation : AbstractValidator<CreateCentroCommand>
    {
        public CreateCentroCommandValidation()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Direccion)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(120).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");
            RuleFor(p => p.Estatus)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
