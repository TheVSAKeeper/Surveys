using Surveys.Web.Application.Messaging.ResponseMessages.ViewModels;

namespace Surveys.Web.Application.Messaging.AnamnesisMessages;

public class AnamnesisUpdateViewModel
{
    public Guid Id { get; set; }
    public bool IsComplete { get; set; }
    public int SortIndex { get; set; }

    public List<ResponseUpdateViewModel>? Responses { get; set; }
}
