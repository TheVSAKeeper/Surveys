using System.Windows.Markup;
using MediatR;
using Surveys.WPF.ViewModels.Base;

namespace Surveys.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel : TitledViewModel
{
    private readonly IMediator _mediator;

    public MainWindowViewModel(IMediator mediator) : base("MainWindow")
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public MainWindowViewModel() : base("MainWindow")
    {
        
    }
}