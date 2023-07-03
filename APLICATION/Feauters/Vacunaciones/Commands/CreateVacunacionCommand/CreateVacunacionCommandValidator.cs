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
           
            RuleFor(p => p.CentroId)
                    .NotEmpty().WithMessage("El centroId no puede ser vacio");
            RuleFor(p => p.CaninoId)
                    .NotEmpty().WithMessage("El caninoId no puede ser vacio");
            RuleFor(p => p.VacunadorId)
                    .NotEmpty().WithMessage("El vacunadorId no puede ser vacio");
            RuleFor(p => p.VacunaId)
                    .NotEmpty().WithMessage("La vacunaId no puede ser vacio");
            RuleFor(p => p.FechaProxima)
                    .NotEmpty().WithMessage("Fecha Proxima no puede ser vacio");
            RuleFor(p => p.Dosis)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");


        }
    }
}
