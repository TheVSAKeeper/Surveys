using Calabonga.OperationResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Surveys.WPF.Application;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<ValidationFailure> failures = _validators
            .Select(x => x.Validate(new ValidationContext<TRequest>(request)))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (failures.Count == 0)
            return next();

        Type type = typeof(TResponse);

        if (!type.IsSubclassOf(typeof(OperationResult)))
            throw new ValidationException(failures);

        object? result = Activator.CreateInstance(type);
        ((OperationResult)result!).AddError(new ValidationException(failures));
        return Task.FromResult((TResponse)result!);
    }
}