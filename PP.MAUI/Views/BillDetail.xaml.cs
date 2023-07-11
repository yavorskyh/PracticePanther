using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ClientID), "ClientID")]
[QueryProperty(nameof(ProjectID), "ProjectID")]
public partial class BillDetail : ContentPage
{
    public int ClientID { get; set; }
    public int ProjectID { get; set; }
    public BillDetail()
	{
		InitializeComponent();
	}

    private void SubmitClicked(object sender, EventArgs e)
    {
        if (!(BindingContext as BillViewModel).CheckTimes())
            DisplayAlert("Error", "Please add at least 1 time entry to project", "OK");

        else if (!(BindingContext as BillViewModel).CheckProjects())
            DisplayAlert("Error", "Please input a valid project ID", "OK");

        else if (!(BindingContext as BillViewModel).CheckBill())
            DisplayAlert("Error", "Bill already exists for current project ID", "OK");

        else
        {
            (BindingContext as BillViewModel).AddOrUpdateBill();
            Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
        }
        
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as BillViewModel).DeleteBill();
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new BillViewModel(ProjectID);
    }
}