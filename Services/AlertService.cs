using Gameball.Resources;

namespace Gameball.Services;

public interface IAlertService
{
    Task DisplayAllert(string title, string message);
    Task DisplayAllert(string title, string message, string okButton);
    Task<bool> DisplayAllert(string title, string message, string okButton, string cancelButton);
}

public class AlertService : IAlertService
{
    public Task DisplayAllert(string title, string message)
        => Application.Current.MainPage.DisplayAlert(title, message, AppResources.Ok_Button);

    public Task DisplayAllert(string title, string message, string okButton)
        => Application.Current.MainPage.DisplayAlert(title, message, okButton);

    public Task<bool> DisplayAllert(string title, string message, string okButton, string cancelButton)
        => Application.Current.MainPage.DisplayAlert(title, message, okButton, cancelButton);
}
