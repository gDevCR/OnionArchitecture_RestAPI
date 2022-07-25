using FluentValidation;

namespace Application.Features.ToDoTasks.Commands.CreateToDoTaskCmd
{
    public class CreateToDoTaskValidator : AbstractValidator<CreateToDoTaskCmd>
    {
        public CreateToDoTaskValidator()
        {
            RuleFor(p => p.GroupId)
                .NotEmpty().WithMessage("La tarea debe tener un grupo asignado.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Nombre de la tarea es requerido.")
                .MaximumLength(200).WithMessage("Nombre no debe sobrepasar los {MaxLength} caracter(es).");
        }
    }
}
