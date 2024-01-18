using System.Windows.Controls;
using System.Windows.Data;

namespace Surveys.WPF.Endpoints.SurveysEndpoints.ShowAll;

/// <summary>
///     Interaction logic for NavigationBarView.xaml
/// </summary>
public partial class SurveyShowAllFormView : UserControl
{
    public SurveyShowAllFormView()
    {
        InitializeComponent();
    }

    private void OnSurveysCollectionOnFilter(object sender, FilterEventArgs e)
    {
        if (e.Item is not SurveyShowDto survey)
        {
            e.Accepted = false;
            return;
        }

        string filterText = SurveysFilterText.Text;

        if (string.IsNullOrWhiteSpace(filterText))
            return;

        if (survey.Patient!.FirstName.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            return;

        if (survey.Patient.LastName.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            return;

        if (survey.Patient.Patronymic != null && survey.Patient.Patronymic.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            return;

        e.Accepted = false;
    }

    private void OnSurveysFilterTextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        CollectionViewSource? collection = (CollectionViewSource)textBox.FindResource("SurveysCollection");
        collection.View?.Refresh();
    }
}