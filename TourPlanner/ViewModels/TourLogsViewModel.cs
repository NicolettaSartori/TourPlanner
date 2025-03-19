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
        
        protected override void AddItem()
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

        protected override void Save()
        {
            if (NewTourLog == null)
                return;
            
            // todo
            // TableData.Add(NewTourLog);
            SelectedTourLog = NewTourLog;
            CloseWindow();
        }
        
        protected override bool CanSave()
        {
            return NewTourLog != null &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalDistance);
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

            NewWindow = new EditTourLogWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }

        protected override void UpdateItem()
        {
            if (SelectedTourLog != null)
            {
                Console.WriteLine($"Tour Log {SelectedTourLog.DateTime} wurde aktualisiert.");
                CloseWindow();
            }
        }

        protected override bool CanUpdate()
        {
            return SelectedTourLog != null &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalDistance) &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.Comment);
        }

        protected override void DeleteItem()
        {
            if (SelectedTourLog != null)
            {
                TourViewModel?.DeleteTourLogFromSelected(SelectedTourLog);
                TourLogs.Remove(SelectedTourLog);
            }
        }
    }
}