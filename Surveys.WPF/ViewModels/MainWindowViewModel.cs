using System.Windows.Markup;
using Surveys.WPF.ViewModels.Base;

namespace Surveys.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel : ViewModel
{
    private string _title = "Test";

    
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }
}