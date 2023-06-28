using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

public partial class TimeView : ContentPage
{
	public TimeView()
	{
		InitializeComponent();
        BindingContext = new TimeViewModel();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewModel).DeleteTime();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//TimerDetail?ProjectID=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var projectId = (BindingContext as TimeViewModel).SelectedTime?.ProjectId ?? 0;

        if (projectId == 0)
            DisplayAlert("Error", "Please Select a Time", "OK");
        else
            Shell.Current.GoToAsync($"//TimerDetail?ProjectID={projectId}");
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as TimeViewModel).RefreshTimes();
    }
}