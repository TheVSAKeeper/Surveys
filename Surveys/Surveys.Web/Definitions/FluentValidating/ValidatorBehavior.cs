using FluentValidation;
using FluentValidation.Results;

namespace Surveys.Web.Definitions.FluentValidating;

/// <summary>
///     Base validator for requests
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    ///     Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">
    ///     Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the
    ///     handler.
    /// </param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse" /></returns>
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<ValidationFailure> failures = validators
            .Select(validator => validator.Validate(new ValidationContext<TRequest>(request)))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        return failures.Count != 0
            ? throw new ValidationException(failures)
            : next();
    }
}
