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
        public Project Model { get; set; }
        public ObservableCollection<Project> Projects
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Project>(ProjectService.Current.Projects);
                }
                return new ObservableCollection<Project>(ProjectService.Current.Search(Query));
            }
        }
        public string Query { get; set; }
        public Project SelectedProject { get; set; }
        public void AddProject()
        {
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

        public void Search()
        {
            NotifyPropertyChanged("Projects");
        }

        public void UpdateProject()
        {
            ProjectService.Current.UpdateProject(Model);
        }

        public void RefreshProjects()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public ProjectViewModel()
        {
            Model = new Project();
            Model.Id = 0;
            Model.OpenDate = DateTime.Now;
            Model.CloseDate = DateTime.Now;
            Model.isActive = true;
        }

        public ProjectViewModel(int projectId)
        {

            Model = ProjectService.Current.GetProject(projectId);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

