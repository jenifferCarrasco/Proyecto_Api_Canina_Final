using FluentValidation;

namespace APLICATION.Feauters.Canino.Commands.CreateCommand
{
	public class CreateCaninoCommandValidator : AbstractValidator<CreateCaninoCommand>
    {
        public CreateCaninoCommandValidator()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Raza)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.FechaNacimiento)
                    .NotEmpty().WithMessage("Fecha Nacimiento no puede ser vacio");

            RuleFor(p => p.Peso)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Color)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Sexo)
					.NotEmpty().WithMessage("{PropertyName} is required")
				    .IsInEnum();

			RuleFor(p => p.PropietarioId)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
