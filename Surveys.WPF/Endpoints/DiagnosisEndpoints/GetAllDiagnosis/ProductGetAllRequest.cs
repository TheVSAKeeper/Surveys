using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Surveys.Domain;

namespace Surveys.WPF.Endpoints.DiagnosisEndpoints.GetAllDiagnosis;

public record DiagnosisGetAllRequest : IRequest<OperationResult<List<DiagnosisViewModel>>>;

public class DiagnosisGetAllRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<DiagnosisGetAllRequest, OperationResult<List<DiagnosisViewModel>>>
{
    public async Task<OperationResult<List<DiagnosisViewModel>>> Handle(DiagnosisGetAllRequest request, CancellationToken cancellationToken)
    {
        IList<DiagnosisViewModel> items = await unitOfWork
                                                .GetRepository<Diagnosis>()
                                                .GetAllAsync(diagnosis => new DiagnosisViewModel
                                                             {
                                                                 Id = diagnosis.Id,
                                                                 Name = diagnosis.Name,
                                                                 Description = diagnosis.Description ?? "Описание отсутствует"
                                                             },
                                                             true);

        return OperationResult.CreateResult(items.ToList());
    }
}

public class DiagnosisViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}