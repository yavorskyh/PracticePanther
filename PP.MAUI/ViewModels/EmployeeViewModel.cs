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
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public Employee Model { get; set; }
        public int EmployeeID { get; set; }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<Employee>(EmployeeService.Current.Employees);
                }
                return new ObservableCollection<Employee>(EmployeeService.Current.Search(Query));
            }
        }

        public Employee SelectedEmployee { get; set; }

        public void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                EmployeeService.Current.DeleteEmployee(SelectedEmployee.Id);
                SelectedEmployee = null;
                NotifyPropertyChanged(nameof(SelectedEmployee));
                NotifyPropertyChanged(nameof(Employees));
            }
        }

        public string Query { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Employees");
        }

        public void AddOrUpdateEmployee()
        {
            EmployeeService.Current.AddorUpdateEmployee(Model);
        }

        public void RefreshEmployees()
        {
            NotifyPropertyChanged(nameof(Employees));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EmployeeViewModel()
        {
            Model = new Employee();
        }

        public EmployeeViewModel(int EmployeeId)
        {
            Model = EmployeeService.Current.GetEmployee(EmployeeId);
            EmployeeID = EmployeeId;
        }
       
    }
}
