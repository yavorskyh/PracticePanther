using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PP.Library.Models;
using PP.Library.Services;

namespace PP.MAUI.ViewModels
{
    internal class ClientViewModel : INotifyPropertyChanged
    {
        public Client Model { get; set; }
        public ObservableCollection<Client> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query)) 
                { 
                    return new ObservableCollection<Client>(ClientService.Current.Clients); 
                }
                return new ObservableCollection<Client>(ClientService.Current.Search(Query));
            }
        }  

        public Client SelectedClient { get; set; }

        public void DeleteClient()
        {
            if (SelectedClient != null) 
            { 
                ClientService.Current.DeleteClient(SelectedClient.Id); 
                SelectedClient = null;
                NotifyPropertyChanged(nameof(SelectedClient));
                NotifyPropertyChanged(nameof(Clients));
            }
        }
        
        public string Query { get; set; }

        public void Search(){
            NotifyPropertyChanged("Clients");
        }

        public void AddClient()
        {
            ClientService.Current.AddClient(Model);
        }

        public void UpdateClient() 
        {
            ClientService.Current.UpdateClient(Model);
        }

        public void RefreshClients()
        {
            NotifyPropertyChanged(nameof(Clients));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ClientViewModel() 
        {
            Model = new Client();
            Model.OpenDate = DateTime.Now;
            Model.CloseDate = DateTime.Now;
        }

        public ClientViewModel(int clientId)
        {
            Model = ClientService.Current.GetClient(clientId);
        }
    }

}
