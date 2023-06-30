using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PP.Library.Models;
using PP.Library.Services;

namespace PP.MAUI.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public Client Model { get; set; }
        public int ClientID { get; set; } 
        public List<Project> Projects 
        {
            get
            {
                return new List<Project>(ProjectService.Current.SearchByClientID(ClientID));
            }
        }

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

        public bool DeleteClient()
        {
            if (SelectedClient != null) 
            {
                List<Project> Projects = ProjectService.Current.Projects.Where(p => p.ClientID == SelectedClient.Id).ToList();
                if (Projects.Count > 0)
                    return false;

                ClientService.Current.DeleteClient(SelectedClient.Id); 
                SelectedClient = null;
                NotifyPropertyChanged(nameof(SelectedClient));
                NotifyPropertyChanged(nameof(Clients));
            }
            return true;
        }
        
        public string Query { get; set; }

        public void Search(){
            NotifyPropertyChanged("Clients");
        }

        public void AddClient()
        {
            ClientService.Current.AddClient(Model);
        }

        public bool UpdateClient() 
        {
            if (!Model.isActive)
            {
                if (HasOpenProjects())
                    return false;
            }

            ClientService.Current.UpdateClient(Model);
            return true;
        }

        public void RefreshClients()
        {
            NotifyPropertyChanged(nameof(Clients));
        }
        
        public bool HasOpenProjects()
        {
            foreach (Project project in Projects)
            {
                if (project.isActive)
                    return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ClientViewModel() 
        {
            Model = new Client();
            Model.Id = 0;
            Model.OpenDate = DateTime.Now;
            Model.CloseDate = DateTime.Now;
            Model.isActive = true;
        }

        public ClientViewModel(int clientId)
        {
            Model = ClientService.Current.GetClient(clientId);
            ClientID = clientId;
        }
    }
}
