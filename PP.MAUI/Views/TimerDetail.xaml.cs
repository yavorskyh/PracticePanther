using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ProjectID), "ProjectID")]
[QueryProperty(nameof(TimeID), "TimeID")]
[QueryProperty(nameof(ClientID), "ClientID")]
public partial class TimerDetail : ContentPage
{
    public int ProjectID { get; set; }
    public int TimeID { get; set; }
    public int ClientID { get; set; }
    public TimerDetail()
    {
        InitializeComponent();
    }
    private void SubmitClicked(object sender, EventArgs e)
    {
        if (!(BindingContext as TimeViewModel).AddOrUpdateTime())
            DisplayAlert("Error", "Employee ID must exist", "OK");
        else
            Shell.Current.GoToAsync($"//Time?ProjectID={ProjectID}&ClientID={ClientID}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Time?ProjectID={ProjectID}&ClientID={ClientID}");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        if (TimeID == 0)
        {
            BindingContext = new TimeViewModel(ProjectID);
        }
        else
        {
            BindingContext = new TimeViewModel(ProjectID, TimeID);
            (BindingContext as TimeViewModel).RefreshTimes();
        }

    }
}