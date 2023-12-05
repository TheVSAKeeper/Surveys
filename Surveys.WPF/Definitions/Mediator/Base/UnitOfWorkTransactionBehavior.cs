using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Surveys.WPF.Definitions.Mediator.Base;

public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await using IDbContextTransaction transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            TResponse response = await next();
            await transaction.CommitAsync(cancellationToken);
            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}