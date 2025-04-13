using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.BusinessLayer.Factories;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.PresentationLayer.Windows;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class TourViewModel : NewWindowViewModelBase
    {
        public ObservableCollection<Tour>? Tours { get; set; }
        
        private Tour? _selectedTour;

        private const string ImagePath = "/PresentationLayer/Images/";
        private readonly TourLogsViewModel _tourLogsViewModel;

        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                SetField(ref _selectedTour, value);
                if (_selectedTour != null)
                {
                    ImageUri = $"{ImagePath}{_selectedTour.Id.ToString()}.png";
                    _tourLogsViewModel.TourLogs = new ObservableCollection<TourLog>(_selectedTour.Logs); 
                }
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        
        private string _imageUri;
        public string ImageUri {
            get => _imageUri;
            private set
            {
                _imageUri = value;
                OnPropertyChanged();
            }
        }
        

        public Tour? NewTour { get; private set; } 
        public List<TransportType> TransportTypes { get; } = Enum.GetValues(typeof(TransportType)).Cast<TransportType>().ToList();

        public TourViewModel(TourLogsViewModel tourLogsViewModel)
        {
            _tourLogsViewModel = tourLogsViewModel;
            _tourLogsViewModel.TourViewModel = this;
            Tours = TourFactory.GetTours();
            SelectedTour = Tours.FirstOrDefault();
        }
        
        protected override void AddItem()
        {
            NewTour = new Tour
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                From = string.Empty,
                To = string.Empty,
                TransportType = TransportType.Hike,
                Distance = string.Empty,
                EstimatedTime = string.Empty
            };

            NewWindow = new NewTourWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }

        public void AddTourLogToSelected(TourLog tourLog)
        {
            if (SelectedTour != null && Tours != null)
                SelectedTour.Logs.Add(tourLog);
        }
        
        protected override void DeleteItem()
        {
            if (SelectedTour != null && Tours != null)
            {
                int index = Tours.IndexOf(SelectedTour);
                Tours.Remove(SelectedTour);
                // select the tour before
                if (Tours.Count == 0)
                    SelectedTour = null;
                else
                    SelectedTour = (index <= 0) ? Tours.FirstOrDefault() : Tours[index - 1];
            }
        }
        
        public void DeleteTourLogFromSelected(TourLog log)
        {
            _selectedTour?.Logs.Remove(log);
            OnPropertyChanged(nameof(SelectedTour));
        }
        
        protected override void Save()
        {
            if (NewTour != null)
            {
                NewTour.Id = Guid.NewGuid();
                Tours?.Add(NewTour);
                SelectedTour = NewTour;
                CloseWindow();
            }
        }

        protected override bool CanSave()
        {
            return NewTour != null &&
                   !string.IsNullOrWhiteSpace(NewTour.Name) &&
                   !string.IsNullOrWhiteSpace(NewTour.From) &&
                   !string.IsNullOrWhiteSpace(NewTour.To) &&
                   !string.IsNullOrWhiteSpace(NewTour.Distance) &&
                   !string.IsNullOrWhiteSpace(NewTour.EstimatedTime) &&
                   !string.IsNullOrWhiteSpace(NewTour.Description);
        }

        protected override bool CanDelete()
        {
            return SelectedTour != null;
        }

        protected override bool CanEdit()
        {
            return SelectedTour != null;
        }

        protected override void EditItem()
        {
            if (SelectedTour == null) return;

            NewTour = JsonSerializer.Deserialize<Tour>(JsonSerializer.Serialize(SelectedTour));
            NewWindow = new EditTourWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }
        
        protected override void UpdateItem()
        {
            if (NewTour != null && SelectedTour != null)
            {
                Tours[Tours.IndexOf(SelectedTour)] = NewTour;
                SelectedTour = NewTour;
                Console.WriteLine($"Tour {SelectedTour.Name} wurde aktualisiert.");
                CloseWindow();
            }
        }

        protected override bool CanUpdate()
        {
            return SelectedTour != null &&
                   !string.IsNullOrWhiteSpace(SelectedTour.Name) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.From) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.To) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.Distance) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.EstimatedTime) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.Description);
        }
        
        protected override void CloseWindow()
        {
            NewTour = null;
            NewWindow?.Close();
        }
    }
}