using FluentValidation;

namespace APLICATION.Feauters.Propietarios.Commands.CreateCommand
{

    public class CreatePropietarioCommandValidator : AbstractValidator<CreatePropietarioCommand>
    {
        public CreatePropietarioCommandValidator()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Apellido)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Direccion)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Cedula)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(13).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
            RuleFor(p => p.Telefono)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                    .MaximumLength(12).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");



        }
    }
}
