using FluentValidation;

namespace APLICATION.Feauters.Centros.Commands.CreateCentroCommand
{

	public class CreateCentroCommandValidation : AbstractValidator<CreateCentroCommand>
    {
        public CreateCentroCommandValidation()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Direccion)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(120).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
        }
    }
}
