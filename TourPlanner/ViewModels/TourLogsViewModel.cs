using System.Collections.ObjectModel;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class TourLogsViewModel : ViewModelBase
    {
        public ObservableCollection<TourLog> TableData { get; }
       

        public TourLogsViewModel()
        {
            //Beispielsdaten
            TableData = [
                new TourLog { DateTime = DateTime.Parse("2024-03-03"), TotalTime = "2h 15m", TotalDistance = "10 km" },
                new TourLog { DateTime = DateTime.Parse("2024-03-04"), TotalTime = "3h 00m", TotalDistance = "15 km" },
                new TourLog { DateTime = DateTime.Parse("2024-03-05"), TotalTime = "1h 45m", TotalDistance = "8 km" }
            ];
        }
    }
}