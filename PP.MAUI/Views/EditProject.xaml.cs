using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ProjectID), "ProjectID")]
[QueryProperty(nameof(ClientID), "ClientID")]
public partial class EditProject : ContentPage
{
    public int ProjectID { get; set; }
    public int ClientID { get; set; }
    public EditProject()
    {
        InitializeComponent();
    }

    private void SubmitClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).UpdateProject();
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Projects?ClientID={ClientID}");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ClientID, ProjectID);
        (BindingContext as ProjectViewModel).RefreshProjects();
    }
}