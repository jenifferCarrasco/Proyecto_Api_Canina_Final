using FluentValidation;

namespace APLICATION.Feauters.Authenticate.Command.RegisterAdminCommand
{
	public class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
    {
        public RegisterAdminCommandValidator() {

            RuleFor(p => p.Nombre)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                   .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Apellido)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

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
