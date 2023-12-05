using System.Windows.Markup;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Surveys.WPF.ViewModels.Base;

namespace Surveys.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel : TitledViewModel
{
    private readonly IMediator? _mediator;

    public MainWindowViewModel() : base("MainWindow")
    {
        _mediator = App.Services.GetService<IMediator>();
    }
}