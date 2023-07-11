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
    public class BillViewModel : INotifyPropertyChanged
    {
        public Bill Model { get; set; }
        public int ProjectId { get; set; }
        public bool AddView { get; set; }
        public bool EditView { get; set; }

        public ObservableCollection<Bill> Bills
        {
            get
            {
                return new ObservableCollection<Bill>
                (BillService.Current.Bills
                .Where(b => b.ProjectId == ProjectId));
            }
        }

        public void AddOrUpdateBill()
        {
            // Set Project ID if adding
            if (AddView)
                Model.ProjectId = ProjectId;

            BillService.Current.AddOrUpdateBill(Model);
        }

        public void DeleteBill()
        {
            BillService.Current.DeleteBill(Model.Id);
        }

        public bool CheckTimes()
        {
            // Check if time entries exist
            var times = TimeService.Current.Times.Where(t => t.ProjectId == ProjectId);
            if (times.Count() == 0)
                return false;
            return true;
        }

        public bool CheckProjects()
        {
            // Check if new project exists
            if (EditView)
            {
                var project = ProjectService.Current.GetProject(Model.ProjectId);
                if (project == null)
                    return false;
            }
            
            return true;
        }

        public bool CheckBill()
        {
            // Check if bill already exists for new project
            if (EditView)
            {
                if (Model.ProjectId != ProjectId)
                {
                    var project = ProjectService.Current.GetProject(Model.ProjectId);
                    var bill = Bills.Where(b => b.ProjectId == project.Id);
                    if (bill != null)
                        return false;
                }
                
            }

            return true;
        }

        public void RefreshBills()
        {
            NotifyPropertyChanged(nameof(Bills));
        }

        // Add/Edit Constructor
        public BillViewModel(int projectId)
        {
            Model = BillService.Current.GetBillByProjectID(projectId);
            if (Model == null)
            {
                Model = new Bill();
                AddView = true;
                EditView = false;
                Model.DueDate = DateTime.Now;
            }
            else
            {
                AddView = false;
                EditView = true;
            }
                
                
                

            ProjectId = projectId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
