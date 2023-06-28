using PP.Library.Models;
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views;

[QueryProperty(nameof(EmployeeID), "EmployeeID")]
public partial class EmployeeDetail : ContentPage
{
	public int EmployeeID { get; set; }
	public EmployeeDetail()
	{
		InitializeComponent();

	}
    private void SubmitClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).AddOrUpdateEmployee();
        Shell.Current.GoToAsync("//Employees");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Employees");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        if (EmployeeID == 0)
        {
            BindingContext = new EmployeeViewModel();
        }
        else
        {
            BindingContext = new EmployeeViewModel(EmployeeID);
            (BindingContext as EmployeeViewModel).RefreshEmployees();
        }

    }
}