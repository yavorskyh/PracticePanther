using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP.Library.Models;
using PP.Library.Services;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace PP.MAUI.ViewModels
{
    class ProjectViewModel : INotifyPropertyChanged
    {   

        public Client Client { get; set; }
        public Project Model { get; set; }
        public ObservableCollection<Project> Projects
        {
            get
            {
                return new ObservableCollection<Project>
                (ProjectService.Current.Projects
                .Where(p => p.ClientID == Client.Id));
            }
        }
        public Project SelectedProject { get; set; }
        public void AddProject()
        {
            Model.ClientID = Client.Id;
            ProjectService.Current.Add(Model);
        }

        public void DeleteProject()
        {
            if (SelectedProject != null)
            {
                ProjectService.Current.DeleteProject(SelectedProject.Id);
                SelectedProject = null;
                NotifyPropertyChanged(nameof(SelectedProject));
                NotifyPropertyChanged(nameof(Projects));
            }
        }

        public void UpdateProject()
        {
            ProjectService.Current.UpdateProject(Model);
        }

        public void RefreshProjects()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public ProjectViewModel(int clientId, int projectId)
        {

            Model = ProjectService.Current.GetProject(projectId);
        }

        public ProjectViewModel(int clientId)
        {
            if (clientId > 0)
            {
                Client = ClientService.Current.GetClient(clientId);
            }
            else
            {
                Client = new Client();
            }

            Model = new Project();
            Model.Id = 0;
            Model.OpenDate = DateTime.Now;
            Model.CloseDate = DateTime.Now;
            Model.isActive = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

