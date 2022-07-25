using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Canaliza las validaciones de los modelos en los request antes de que llegue a la persistencia
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validtionResults = await Task.WhenAll(_validators.Select(s => s.ValidateAsync(context, cancellationToken)));
                var failures = validtionResults.SelectMany(r => r.Errors).Where(w => w != null).ToList();

                if (failures.Count != 0)
                    throw new Exceptions.ValidationException(failures);

            }
            return await next();            
        }
    }
}
