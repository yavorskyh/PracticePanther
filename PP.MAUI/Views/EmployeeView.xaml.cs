using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

public partial class EmployeeView: ContentPage
{
	public EmployeeView()
	{
		InitializeComponent();
        BindingContext = new EmployeeViewModel();
	}

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).DeleteEmployee();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//EmployeeDetail?EmployeeID=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var employeeId = (BindingContext as EmployeeViewModel).SelectedEmployee?.Id ?? 0;

        if (employeeId == 0)
            DisplayAlert("Error", "Please Select an Employee", "OK");
        else
            Shell.Current.GoToAsync($"//EmployeeDetail?EmployeeID={employeeId}");
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as EmployeeViewModel).RefreshEmployees();
    }
}