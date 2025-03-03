using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        private string _mapImagePath;
        public string MapImagePath
        {
            get { return _mapImagePath; }
            set { _mapImagePath = value; OnPropertyChanged(); }
        }

        public MapViewModel()
        {
            MapImagePath = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}