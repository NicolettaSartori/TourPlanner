using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _tours;
        public ObservableCollection<string> Tours
        {
            get { return _tours; }
            set { _tours = value; OnPropertyChanged(); }
        }

        public ListViewModel()
        {
            Tours = new ObservableCollection<string>
            {
                "Wienerwald",
                "Dopplerhütte",
                "Figlwarte",
                "Dorfrunde"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}