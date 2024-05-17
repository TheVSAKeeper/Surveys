using Calabonga.Results;
using MediatR;
using Surveys.Web.Application.Messaging.ProfileMessages.ViewModels;
using Surveys.Web.Application.Services;

namespace Surveys.Web.Application.Messaging.ProfileMessages.Queries;

public sealed class RegisterAccount
{
    public class Handler(IAccountService accountService)
        : IRequestHandler<Request, Operation<UserProfileViewModel, string>>
    {
        public Task<Operation<UserProfileViewModel, string>> Handle(Request request, CancellationToken cancellationToken) =>
            accountService.RegisterAsync(request.Model, cancellationToken);
    }

    public record Request(RegisterViewModel Model) : IRequest<Operation<UserProfileViewModel, string>>;
}