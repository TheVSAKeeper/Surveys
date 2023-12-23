using System.Windows;
using Surveys.WPF.Features.Authentication;

namespace Surveys.WPF.Application.Initialization;

public class ApplicationInitializer
{
    private readonly AuthenticationStore _authenticationStore;

    public ApplicationInitializer(AuthenticationStore authenticationStore)
    {
        _authenticationStore = authenticationStore;
    }

    public async Task Initialize()
    {
        try
        {
            await _authenticationStore.Initialize();
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}