using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.PresentationLayer.Windows;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class TourLogsViewModel : NewWindowViewModelBase
    {
        private ObservableCollection<TourLog> _tourLogs = [];
        public ObservableCollection<TourLog> TourLogs
        {
            get => _tourLogs;
            set
            {
                _tourLogs = value;
                OnPropertyChanged();
            }
        }

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
        public TourLog? NewTourLog { get; private set; }
        public TourViewModel? TourViewModel { get; set; }
        
        public List<Difficulty> Difficulties { get; } = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().ToList();
        public List<int> Ratings { get; } = [1, 2, 3, 4, 5];
        
        protected override void AddItem()
        {
            NewTourLog = new TourLog
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                TotalTime = "",
                TotalDistance = "",
            };

            NewWindow = new NewTourLogWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }

        protected override void Save()
        {
            if (NewTourLog == null)
                return;
            
            TourViewModel?.AddTourLogToSelected(NewTourLog);
            TourLogs.Add(NewTourLog);
            SelectedTourLog = NewTourLog;
            CloseWindow();
        }
        
        protected override bool CanSave()
        {
            return NewTourLog != null &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalDistance) &&
                   NewTourLog.Rating != default;
        }

        protected override bool CanDelete()
        {
            return SelectedTourLog != null;
        }

        protected override bool CanEdit()
        {
            return SelectedTourLog != null;
        }

        protected override void EditItem()
        {
            if (SelectedTourLog == null) return;

            NewTourLog = JsonSerializer.Deserialize<TourLog>(JsonSerializer.Serialize(SelectedTourLog));
            NewWindow = new EditTourLogWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }

        protected override void UpdateItem()
        {
            if (SelectedTourLog != null && NewTourLog != null)
            {
                TourLogs[TourLogs.IndexOf(SelectedTourLog)] = NewTourLog;
                SelectedTourLog = NewTourLog;
                Console.WriteLine($"Tour Log {SelectedTourLog.DateTime} wurde aktualisiert.");
                CloseWindow();
            }
        }

        protected override bool CanUpdate()
        {
            return SelectedTourLog != null &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalDistance);
        }

        protected override void DeleteItem()
        {
            if (SelectedTourLog != null)
            {
                TourViewModel?.DeleteTourLogFromSelected(SelectedTourLog);
                TourLogs.Remove(SelectedTourLog);
            }
        }

        protected override void CloseWindow()
        {
            NewTourLog = null;
            NewWindow?.Close();
        }
    }
}