using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

public partial class AddProject : ContentPage
{
    public AddProject()
    {
        InitializeComponent();
        BindingContext = new ProjectViewModel();
    }

    private void SubmitClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).AddProject();
        Shell.Current.GoToAsync("//Projects");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Projects");
    }

    private void OnArriving(object sender, EventArgs e)
    {
        BindingContext = new ProjectViewModel();
    }
}