using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

public partial class ProjectView : ContentPage
{
    public ProjectView()
    {
        InitializeComponent();
        BindingContext = new ProjectViewModel();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).DeleteProject();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AddProject");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var projectId = (BindingContext as ProjectViewModel).SelectedProject?.Id ?? 0;
        if (projectId == 0) 
            DisplayAlert("Error", "Please Select a Project", "OK");
        
        else
            Shell.Current.GoToAsync($"//EditProject?ProjectID={projectId}");
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ProjectViewModel).RefreshProjects();
    }
}