﻿namespace PP.MAUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void ClientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Clients");
        }

        private void ProjectsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Projects");
        }
    }
}