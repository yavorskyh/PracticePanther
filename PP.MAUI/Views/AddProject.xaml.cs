using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ClientID), "ClientID")]
public partial class AddProject : ContentPage
{
    public int ClientID { get; set; }
    public AddProject()
    {
        InitializeComponent();
    }

    private void SubmitClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).AddProject();
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void OnArriving(object sender, EventArgs e)
    {
        BindingContext = new ProjectViewModel(ClientID);
    }
}