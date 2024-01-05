﻿using Calabonga.OperationResults;
using MediatR;
using Surveys.Domain;
using Surveys.WPF.Shared.Commands;

namespace Surveys.WPF.Features.Creation.AnamnesesCreate;

public class AnamnesesCreateCommand(AnamnesesCreateFormViewModel viewModel, IMediator mediator)
    : AsyncCommandBase
{
    protected override async Task ExecuteAsync(object? parameter)
    {
        if (viewModel is { AnamnesisTemplates: not null, Survey: not null })
        {
            OperationResult<List<Anamnesis>> result = await mediator.Send(new AnamnesesCreateRequest(viewModel.Survey,viewModel.AnamnesisTemplates.ToList()));
            viewModel.CreatedAnamneses = result.Result;
        }
    }

    public override bool CanExecute(object? parameter) => IsExecuting == false
                                                          && viewModel.AnamnesisTemplates != null
                                                          && viewModel.AnamnesisTemplates.Any(dto => dto.IsSelected);
}