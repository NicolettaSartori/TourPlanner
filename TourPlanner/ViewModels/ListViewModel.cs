using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set { _tours = value; OnPropertyChanged(); }
        }

        public ListViewModel()
        {
            Tours = [
                new Tour { Name = "Wienerwald" },
                new Tour { Name = "Dopplerhütte" },
                new Tour { Name = "Figlwarte" },
                new Tour { Name = "Dorfrunde" }
            ];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}