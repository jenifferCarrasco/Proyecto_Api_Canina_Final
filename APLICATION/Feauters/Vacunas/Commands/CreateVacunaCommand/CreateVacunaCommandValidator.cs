using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APLICATION.Feauters.Vacunas.Commands.CreateVacunaCommand
{
    public class CreateVacunaCommandValidator : AbstractValidator<CreateVacunaCommand>
    {
        public CreateVacunaCommandValidator()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Laboratorio)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            //RuleFor(p => p.FechaCaducidad)
            //        .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");


            //RuleFor(p => p.Lote)
            //        .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
            //        .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Descripcion)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(350).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            //RuleFor(p => p.Estatus)
            //        .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");





        }
    }
}
