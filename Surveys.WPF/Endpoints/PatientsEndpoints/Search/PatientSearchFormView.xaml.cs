using System.Windows.Controls;
using System.Windows.Data;
using Surveys.Domain;

namespace Surveys.WPF.Endpoints.PatientsEndpoints.Search;

/// <summary>
///     Interaction logic for NavigationBarView.xaml
/// </summary>
public partial class PatientSearchFormView : UserControl
{
    public PatientSearchFormView()
    {
        InitializeComponent();
    }

    private void OnPatientsCollectionOnFilter(object sender, FilterEventArgs e)
    {
        if (e.Item is not Patient patient)
        {
            e.Accepted = false;
            return;
        }

        string filterText = PatientsFilterText.Text;

        if (string.IsNullOrWhiteSpace(filterText))
            return;

        if (patient.FirstName.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            return;

        if (patient.LastName.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            return;

        if (patient.Patronymic != null && patient.Patronymic.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            return;

        e.Accepted = false;
    }

    private void OnPatientsFilterTextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        CollectionViewSource? collection = (CollectionViewSource)textBox.FindResource("PatientsCollection");
        collection.View?.Refresh();
    }
}