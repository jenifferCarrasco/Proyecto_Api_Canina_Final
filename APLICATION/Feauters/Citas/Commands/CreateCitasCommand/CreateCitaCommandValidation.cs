using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Feauters.Citas.Commands.CreateCitasCommand
{
    public class CreateCitaCommandValidation : AbstractValidator<CreateCitaCommand>
    {
        public CreateCitaCommandValidation()
        {
    
            RuleFor(p => p.FechaCita)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(p => p.CentroId)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(p => p.VacunadorId)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(p => p.CaninoId)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
            RuleFor(p => p.Estatus)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

        }
    }
}
