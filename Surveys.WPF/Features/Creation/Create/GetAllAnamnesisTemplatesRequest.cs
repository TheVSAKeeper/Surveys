using System.Collections.ObjectModel;
using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Surveys.Domain;

namespace Surveys.WPF.Features.Creation.Create;

public record GetAllAnamnesisTemplatesRequest : IRequest<OperationResult<List<AnamnesisTemplateDto>>>;

public class GetAllAnamnesisTemplatesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllAnamnesisTemplatesRequest, OperationResult<List<AnamnesisTemplateDto>>>
{
    public async Task<OperationResult<List<AnamnesisTemplateDto>>> Handle(GetAllAnamnesisTemplatesRequest request, CancellationToken cancellationToken)
    {
        IList<AnamnesisTemplateDto> templates = await unitOfWork.GetRepository<AnamnesisTemplate>()
            .GetAllAsync(disableTracking: true,
                selector: s => new AnamnesisTemplateDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Questions = new ObservableCollection<Question>(s.Questions),
                    IsSelected = false
                },
                include: i => i.Include(template => template.Questions));

        return OperationResult.CreateResult(templates.ToList());
    }
}