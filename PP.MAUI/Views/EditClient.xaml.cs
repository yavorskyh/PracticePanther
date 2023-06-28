using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ClientID), "ClientID")]
public partial class EditClient : ContentPage
{
    public int ClientID { get; set; }
	public EditClient()
	{
		InitializeComponent();
	}

    private void SubmitClicked(object sender, EventArgs e)
    {
        if (!(BindingContext as ClientViewModel).UpdateClient())
        {
            DisplayAlert("Error", "Cannot close client. All projects must be closed.", "OK");
        }
        else
        {
            Shell.Current.GoToAsync("//Clients");
        }
        
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Clients");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel(ClientID);
        (BindingContext as ClientViewModel).RefreshClients();
    }
}