using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(ClientID), "ClientID")]
public partial class ProjectView : ContentPage
{
    public int ClientID { get; set; }
    public ProjectView()
    {
        InitializeComponent();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        var projectId = (BindingContext as ProjectViewModel).SelectedProject?.Id ?? 0;
        if (projectId == 0)
            DisplayAlert("Error", "Please select a project", "OK");
        else if (!(BindingContext as ProjectViewModel).CheckBill())
            DisplayAlert("Error", "Please close all bills under current project", "OK");
        else
            (BindingContext as ProjectViewModel).DeleteProject();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//AddProject?ClientID={ClientID}");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var projectId = (BindingContext as ProjectViewModel).SelectedProject?.Id ?? 0;
        if (projectId == 0) 
            DisplayAlert("Error", "Please select a project", "OK");
        else
            Shell.Current.GoToAsync($"//EditProject?ProjectID={projectId}&ClientID={ClientID}");
    }

    private void TimeClicked(object sender, EventArgs e)
    {
        var projectId = (BindingContext as ProjectViewModel).SelectedProject?.Id ?? 0;
        if (projectId == 0)
            DisplayAlert("Error", "Please select a project", "OK");
        else
            Shell.Current.GoToAsync($"//Time?ProjectID={projectId}&ClientID={ClientID}");
    }

    private void BillClicked(object sender, EventArgs e)
    {
        var projectId = (BindingContext as ProjectViewModel).SelectedProject?.Id ?? 0;
        if (projectId == 0)
            DisplayAlert("Error", "Please select a project", "OK");
        else
            Shell.Current.GoToAsync($"//BillDetail?ProjectID={projectId}&ClientID={ClientID}");
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Clients");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ClientID);
        (BindingContext as ProjectViewModel).RefreshProjects();
    }
}