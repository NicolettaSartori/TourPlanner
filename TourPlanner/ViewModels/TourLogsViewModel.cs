using System.Collections.ObjectModel;
using TourPlanner.Models;
using System.Windows;
using System.Windows.Input;
using TourPlanner.MVVM;
using TourPlanner.Models;
using TourPlanner.MVVM;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels
{
    public class TourLogsViewModel : NewWindowViewModelBase
    {
        public ObservableCollection<TourLog> TableData { get; }
        private TourLog? _selectedTourLog;
        public TourLog? SelectedTourLog
        {
            get => _selectedTourLog;
            set
            {
                SetField(ref _selectedTourLog, value);
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public TourLog NewTourLog { get; private set; }
        
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand UpdateCommand { get; }


        public TourLogsViewModel()
        {
            //Beispielsdaten
            TableData = [
                new TourLog { DateTime = DateTime.Parse("2024-03-03"), TotalTime = "2h 15m", TotalDistance = "10 km" },
                new TourLog { DateTime = DateTime.Parse("2024-03-04"), TotalTime = "3h 00m", TotalDistance = "15 km" },
                new TourLog { DateTime = DateTime.Parse("2024-03-05"), TotalTime = "1h 45m", TotalDistance = "8 km" }
            ];
            
            AddCommand = new RelayCommand(_ => AddTourLog());
            DeleteCommand = new RelayCommand(_ => DeleteTourLog(), _ => SelectedTourLog != null);
            EditCommand = new RelayCommand(_ => EditTourLog(), _ => SelectedTourLog != null);
            SaveCommand = new RelayCommand(_ => SaveNewTourLog(), _ => CanSaveTourLog());
            UpdateCommand = new RelayCommand(_ => UpdateTourLog(), _ => SelectedTourLog != null);

        }
        
        private void AddTourLog()
        {
            NewTourLog = new TourLog
            {
                DateTime = DateTime.Now,
                TotalTime = "",
                TotalDistance = ""
            };

            NewWindow = new NewTourLogWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }

        private void SaveNewTourLog()
        {
            if (NewTourLog != null)
            {
                TableData.Add(NewTourLog);
                SelectedTourLog = NewTourLog;
                CloseWindow();
            }
        }

        private void EditTourLog()
        {
            if (SelectedTourLog == null) return;

            NewWindow = new EditTourLogWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }

        private void UpdateTourLog()
        {
            if (SelectedTourLog != null)
            {
                Console.WriteLine($"Tour Log {SelectedTourLog.DateTime} wurde aktualisiert.");
                CloseWindow();
            }
        }

        private bool CanUpdateTourLog()
        {
            return SelectedTourLog != null &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalDistance) &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.Comment);
        }
        
        private bool CanSaveTourLog()
        {
            return NewTourLog != null &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalDistance);
        }


        private void DeleteTourLog()
        {
            if (SelectedTourLog != null)
            {
                TableData.Remove(SelectedTourLog);
                SelectedTourLog = TableData.FirstOrDefault();
            }
        }
        
        protected override void Save()
        {
            Console.WriteLine("in Save");
            CloseWindow();
        }
    }
}