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
        public Time Model { get; set; }
        public int TimeID { get; set; }

        public ObservableCollection<Time> Times
        {
            get
            {
                return new ObservableCollection<Time>(TimeService.Current.Times);
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

        public void AddOrUpdateTime()
        {
            TimeService.Current.AddOrUpdateTime(Model);
        }

        public void RefreshTimes()
        {
            NotifyPropertyChanged(nameof(Times));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimeViewModel()
        {
            Model = new Time();
            Model.Date = DateTime.Now;
        }

        public TimeViewModel(int TimeId)
        {
            Model = TimeService.Current.GetTime(TimeId);
            TimeID = TimeId;
        }
    }
}
