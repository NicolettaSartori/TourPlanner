using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                new TourLog { Date = "2024-03-03", Duration = "2h 15m", Distance = "10 km" },
                new TourLog { Date = "2024-03-04", Duration = "3h 00m", Distance = "15 km" },
                new TourLog { Date = "2024-03-05", Duration = "1h 45m", Distance = "8 km" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TourLog
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }
    }
}