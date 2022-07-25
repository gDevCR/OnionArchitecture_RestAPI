using FluentValidation;

namespace Application.Features.ToDoTasks.Commands.DeleteToDoTaskCmd
{
    public class DeleteToDoTaskValidator : AbstractValidator<DeleteToDoTaskCmd>
    {
        public DeleteToDoTaskValidator()
        {

            RuleFor(p => p.DeletedBy)
                .NotEmpty().WithMessage("The task must have a user name for delete record.");                
        }
    }
}
