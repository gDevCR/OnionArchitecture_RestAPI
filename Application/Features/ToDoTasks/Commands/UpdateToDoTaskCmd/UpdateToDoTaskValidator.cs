using FluentValidation;

namespace Application.Features.ToDoTasks.Commands.UpdateToDoTaskCmd
{
    public class UpdateToDoTaskValidator : AbstractValidator<UpdateToDoTaskCmd>
    {
        public UpdateToDoTaskValidator()
        {
            RuleFor(p => p.GroupId)               
                .NotEmpty().WithMessage("The task must have a group assigned.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Task name in required.")
                .MaximumLength(200).WithMessage("Name must not exceed {MaxLength} character(s).");

            RuleFor(p => p.Description)                
                .MaximumLength(500).WithMessage("Task decription must not exceed {MaxLength} character(s).");
        }
    }
}
