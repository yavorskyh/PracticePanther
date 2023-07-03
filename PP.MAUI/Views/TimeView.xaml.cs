using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ProjectID), "ProjectID")]
[QueryProperty(nameof(ClientID), "ClientID")]
public partial class TimeView : ContentPage
{
    public int ProjectID { get; set; }
    public int ClientID { get; set; }
    public TimeView()
	{
		InitializeComponent();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        var timeId = (BindingContext as TimeViewModel).SelectedTime?.ProjectId ?? 0;

        if (timeId == 0)
            DisplayAlert("Error", "Please Select a Time", "OK");
        else
            (BindingContext as TimeViewModel).DeleteTime();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//TimerDetail?ProjectID={ProjectID}&TimeID={0}&ClientID={ClientID}");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var timeId = (BindingContext as TimeViewModel).SelectedTime?.ProjectId ?? 0;

        if (timeId == 0)
            DisplayAlert("Error", "Please Select a Time", "OK");
        else
            Shell.Current.GoToAsync($"//TimerDetail?ProjectID={ProjectID}&TimeID={timeId}&ClientID={ClientID}");
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new TimeViewModel(ProjectID);
        (BindingContext as TimeViewModel).RefreshTimes();
    }
}