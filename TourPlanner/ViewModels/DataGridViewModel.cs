using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class DataGridViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TourLog> _tableData;
        public ObservableCollection<TourLog> TableData
        {
            get { return _tableData; }
            set { _tableData = value; OnPropertyChanged(); }
        }

        public DataGridViewModel()
        {
            //Beispielsdaten
            TableData = new ObservableCollection<TourLog>
            {
                new TourLog { DateTime = DateTime.Parse("2024-03-03"), TotalTime = "2h 15m", TotalDistance = "10 km" },
                new TourLog { DateTime = DateTime.Parse("2024-03-04"), TotalTime = "3h 00m", TotalDistance = "15 km" },
                new TourLog { DateTime = DateTime.Parse("2024-03-05"), TotalTime = "1h 45m", TotalDistance = "8 km" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}