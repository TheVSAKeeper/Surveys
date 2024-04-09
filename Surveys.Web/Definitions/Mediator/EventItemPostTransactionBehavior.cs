using Surveys.Web.Application.Messaging.EventItemMessages.ViewModels;
using Surveys.Web.Definitions.Mediator.Base;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;

namespace Surveys.Web.Definitions.Mediator;

public class EventItemPostTransactionBehavior(IUnitOfWork unitOfWork)
    : TransactionBehavior<IRequest<Operation<EventItemViewModel>>, Operation<EventItemViewModel>>(unitOfWork);