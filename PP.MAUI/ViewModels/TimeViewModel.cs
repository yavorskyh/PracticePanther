using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PP.Library.Models;
using PP.Library.Services;

namespace PP.MAUI.ViewModels
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        public Project Project { get; set; }
        public Time Model { get; set; }
        public int TimeID { get; set; }

        public ObservableCollection<Time> Times
        {
            get
            {
                return new ObservableCollection<Time>
                 (TimeService.Current.Times
                 .Where(t => t.ProjectId == Project.Id));
            }
        }

        public Time SelectedTime { get; set; }

        public void DeleteTime()
        {
            if (SelectedTime != null)
            {
                TimeService.Current.DeleteTime(SelectedTime.ProjectId);
                SelectedTime = null;
                NotifyPropertyChanged(nameof(SelectedTime));
                NotifyPropertyChanged(nameof(Times));
            }
        }

        public bool AddOrUpdateTime()
        {
            // Check if Employee exists
            List<Employee> Employees = EmployeeService.Current.SearchByEmployeeID(Model.EmployeeId).ToList();
            if (Employees.Count == 0)
                return false;

            // Set Project ID if adding
            if (TimeID == 0)
                Model.ProjectId = Project.Id;
            TimeService.Current.AddOrUpdateTime(Model);
            return true;
        }

        public void RefreshTimes()
        {
            NotifyPropertyChanged(nameof(Times));
        }

        // Add Constructor
        public TimeViewModel(int projectId)
        {
            if (projectId > 0)
            {
                Project = ProjectService.Current.GetProject(projectId);
            }
            else
            {
                Project = new Project();
            }

            Model = new Time();
            Model.Date = DateTime.Now;
            Model.Hours = 0.0m;
            Model.ProjectId = 0;
            Model.EmployeeId = 0;
        }

        // Edit Constructor
        public TimeViewModel(int projectId,int timeId)
        {
            Model = TimeService.Current.GetTime(timeId);
            TimeID = timeId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
