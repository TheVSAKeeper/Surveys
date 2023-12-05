namespace Surveys.WPF.ViewModels.Base;

public class TitledViewModel(string title) : BaseViewModel
{
    public string Title
    {
        get => title;
        set => Set(field: ref title,
                   value: value);
    }
}