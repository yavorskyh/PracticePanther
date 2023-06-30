
using PP.MAUI.ViewModels;

namespace PP.MAUI.Views
{
    public partial class AddClient : ContentPage
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void SubmitClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewModel).AddClient();
            Shell.Current.GoToAsync("//Clients");
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Clients");
        }

        private void OnArriving(object sender, EventArgs e) 
        { 
            BindingContext = new ClientViewModel(); 
        }
    }
}

