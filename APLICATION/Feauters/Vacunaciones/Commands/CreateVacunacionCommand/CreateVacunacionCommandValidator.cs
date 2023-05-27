using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunaciones.Commands.CreateVacunacionCommand
{
    public class CreateVacunacionCommandValidator : AbstractValidator<CreateVacunacionCommand>
    {
        public CreateVacunacionCommandValidator()
        {
           
            RuleFor(p => p.FechaProxima)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");
            RuleFor(p => p.CentroId)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");
            RuleFor(p => p.CaninoId)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");
            RuleFor(p => p.VacunadorId)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");
            RuleFor(p => p.VacunaId)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");
            RuleFor(p => p.Dosis)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracter");


        }
    }
}
