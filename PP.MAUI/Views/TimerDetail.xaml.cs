using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ProjectID), "ProjectID")]
public partial class TimerDetail : ContentPage
{
    public int ProjectID { get; set; }
    public TimerDetail()
    {
        InitializeComponent();
    }
    private void SubmitClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewModel).AddOrUpdateTime();
        Shell.Current.GoToAsync("//Time");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Time");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        if (ProjectID == 0)
        {
            BindingContext = new TimeViewModel();
        }
        else
        {
            BindingContext = new TimeViewModel(ProjectID);
            (BindingContext as TimeViewModel).RefreshTimes();
        }

    }
}