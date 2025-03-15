using System.Collections.ObjectModel;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set { _tours = value; OnPropertyChanged(); }
        }

        public TourViewModel()
        {
            Tours = [
                new Tour { Name = "Wienerwald" },
                new Tour { Name = "Dopplerhütte" },
                new Tour { Name = "Figlwarte" },
                new Tour { Name = "Dorfrunde" }
            ];
        }
    }
}