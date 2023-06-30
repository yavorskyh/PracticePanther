using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

public partial class ClientView : ContentPage
{
    public ClientView()
    {
        InitializeComponent();
        BindingContext = new ClientViewModel();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        if (!(BindingContext as ClientViewModel).DeleteClient())
            DisplayAlert("Error", "Please delete all projects before deleting", "OK");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AddClient");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var clientId = (BindingContext as ClientViewModel).SelectedClient?.Id ?? 0;

        if(clientId == 0) 
            DisplayAlert("Error", "Please Select a Client", "OK"); 
        else
            Shell.Current.GoToAsync($"//EditClient?ClientID={clientId}");
    }

    private void ProjectsClicked(object sender, EventArgs e)
    {
        var clientId = (BindingContext as ClientViewModel).SelectedClient?.Id ?? 0;

        if (clientId == 0)
            DisplayAlert("Error", "Please Select a Client", "OK");
        else
            Shell.Current.GoToAsync($"//Projects?ClientID={clientId}");
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ClientViewModel).RefreshClients();
    }
}