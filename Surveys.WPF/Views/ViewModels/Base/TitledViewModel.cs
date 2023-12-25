namespace Surveys.WPF.Views.ViewModels.Base;

public class TitledViewModel(string title) : BaseViewModel
{
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }
}