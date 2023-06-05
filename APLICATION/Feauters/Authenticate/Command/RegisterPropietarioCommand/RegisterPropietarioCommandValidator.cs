using FluentValidation;

namespace APLICATION.Feauters.Authenticate.Command.RegisterPropietarioCommand
{
	public class RegisterPropietarioCommandValidator : AbstractValidator<RegisterPropietarioCommand>
	{
		public RegisterPropietarioCommandValidator()
		{

			RuleFor(p => p.Nombre)
				   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
				   .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

			RuleFor(p => p.Apellido)
					.NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
					.MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

			RuleFor(p => p.Cedula)
					.NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
					.MaximumLength(11).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
					.MinimumLength(11).WithMessage("{PropertyName} requiere un minimo de {MinLenght} caracteres");

			RuleFor(p => p.Sexo)
				   .NotEmpty().WithMessage("{PropertyName} is required")
				   .IsInEnum();

			RuleFor(p => p.Telefono)
					.NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
					.MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres")
					.MinimumLength(10).WithMessage("{PropertyName} requiere un minimo de {MinLenght} caracteres");

			RuleFor(p => p.Email)
					 .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
					 .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de correo valida")
					 .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

			RuleFor(p => p.UserName)
					.NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
					.MaximumLength(20).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

			RuleFor(p => p.Password)
					.NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
					.MaximumLength(30).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
		}
	}
}
