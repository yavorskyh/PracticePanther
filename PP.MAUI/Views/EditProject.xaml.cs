using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ProjectID), "ProjectID")]
public partial class EditProject : ContentPage
{
    public int ProjectID { get; set; }
    public EditProject()
    {
        InitializeComponent();
    }

    private void SubmitClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).UpdateProject();
        Shell.Current.GoToAsync("//Projects");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Projects");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ProjectID);
        (BindingContext as ProjectViewModel).RefreshProjects();
    }
}