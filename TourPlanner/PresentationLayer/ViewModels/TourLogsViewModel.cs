using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.BusinessLayer.Services;
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
        
        private readonly TourLogService _service = new();

        public List<Difficulty> Difficulties { get; } = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().ToList();
        public List<int> Ratings { get; } = [1, 2, 3, 4, 5];

        protected override void AddItem()
        {
            if (TourViewModel?.SelectedTour == null)
                return;

            NewTourLog = new TourLog
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.UtcNow,
                TotalTime = "",
                TotalDistance = "",
                TourId = TourViewModel.SelectedTour.Id,
                Difficulty = Difficulty.Medium
            };

            NewWindow = new NewTourLogWindow
            {
                DataContext = this
            };
            NewWindow.ShowDialog();
        }

        protected override async void Save()
        {
            if (NewTourLog == null || TourViewModel?.SelectedTour == null)
                return;

            await _service.AddTourLogAsync(NewTourLog, TourViewModel.SelectedTour.Id);
            TourViewModel.AddTourLogToSelected(NewTourLog);
            TourLogs.Add(NewTourLog);
            SelectedTourLog = NewTourLog;
            CloseWindow();
        }

        public override bool CanSave()
        {
            return NewTourLog != null &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(NewTourLog.TotalDistance) &&
                   NewTourLog.Rating != default;
        }

        public override bool CanDelete()
        {
            return SelectedTourLog != null;
        }

        public override bool CanEdit()
        {
            return SelectedTourLog != null;
        }

        protected override void EditItem()
        {
            if (SelectedTourLog == null) return;

            NewTourLog = new TourLog
            {
                Id = SelectedTourLog.Id,
                DateTime = SelectedTourLog.DateTime,
                Comment = SelectedTourLog.Comment,
                Difficulty = SelectedTourLog.Difficulty,
                TotalDistance = SelectedTourLog.TotalDistance,
                TotalTime = SelectedTourLog.TotalTime,
                Rating = SelectedTourLog.Rating,
                TourId = SelectedTourLog.TourId
            };

            NewWindow = new EditTourLogWindow
            {
                DataContext = this
            };
            NewWindow.ShowDialog();
        }


        protected override async void UpdateItem()
        {
            if (SelectedTourLog != null && NewTourLog != null)
            {
                await _service.UpdateTourLogAsync(NewTourLog);

                int index = TourLogs.IndexOf(SelectedTourLog);
                if (index >= 0)
                {
                    TourLogs[index] = NewTourLog;
                    SelectedTourLog = NewTourLog;
                }

                CloseWindow();
            }
        }

        public override bool CanUpdate()
        {
            return SelectedTourLog != null &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalTime) &&
                   !string.IsNullOrWhiteSpace(SelectedTourLog.TotalDistance);
        }

        protected override async void DeleteItem()
        {
            if (SelectedTourLog != null)
            {
                await _service.DeleteTourLogAsync(SelectedTourLog.Id);
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
