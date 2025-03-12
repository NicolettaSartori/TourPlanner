namespace TourPlanner.ViewModels
{
    public class MapViewModel : ViewModelBase
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
    }
}