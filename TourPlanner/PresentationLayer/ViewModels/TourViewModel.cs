using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.PresentationLayer.Windows;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.PresentationLayer.MVVM;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class TourViewModel : NewWindowViewModelBase
    {
        private ObservableCollection<Tour>? _tours;
        public ObservableCollection<Tour>? Tours
        {
            get => _tours;
            set
            {
                SetField(ref _tours, value);
                OnPropertyChanged();
            }
        }

        private Tour? _selectedTour;

        private const string ImagePath = "/PresentationLayer/Images/";
        private readonly TourLogsViewModel _tourLogsViewModel;

        public Tour? NewTour { get; private set; }
        public List<TransportType> TransportTypes { get; } = Enum.GetValues(typeof(TransportType)).Cast<TransportType>().ToList();
        
        private readonly TourService _service = new();
        
        public ICommand ExportCommand { get; }
        public ICommand RandomCommand { get; }

        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (SetField(ref _selectedTour, value))
                {
                    ImageUri = _selectedTour != null
                        ? $"{ImagePath}{_selectedTour.Id}.png"
                        : string.Empty;

                    LoadTourLogsForSelectedTour();
                    OnPropertyChanged();
                }
            }
        }

        private string _imageUri;
        public string ImageUri
        {
            get => _imageUri;
            private set
            {
                _imageUri = value;
                OnPropertyChanged();
            }
        }
        
        public TourViewModel(TourLogsViewModel tourLogsViewModel)
        {
            _tourLogsViewModel = tourLogsViewModel;
            _tourLogsViewModel.TourViewModel = this;
            
            ExportCommand = new RelayCommand(_ => ExportItem());
            RandomCommand = new RelayCommand(_ => GetRandomItem());

            Tours = new ObservableCollection<Tour>(_service.GetTours());

            SelectedTour = Tours.FirstOrDefault();

            if (SelectedTour != null)
            {
                LoadTourLogsForSelectedTour();
            }
        }
        
        private async void LoadTourLogsForSelectedTour()
        {
            if (_selectedTour == null)
                return;

            await _service
                .GetLogsAsync(_selectedTour.Id)
                .ContinueWith(task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _tourLogsViewModel.TourLogs = new ObservableCollection<TourLog>(task.Result);
                            _tourLogsViewModel.TourViewModel = this;
                        });
                    }
                });
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

        protected override async void DeleteItem()
        {
            if (SelectedTour != null && Tours != null)
            {
                var tourToRemove = SelectedTour;

                await _service.DeleteTourAsync(tourToRemove.Id);

                int index = Tours.IndexOf(tourToRemove);
                Tours.Remove(tourToRemove);

                SelectedTour = Tours.Count == 0
                    ? null
                    : (index <= 0 ? Tours.FirstOrDefault() : Tours[index - 1]);

                OnPropertyChanged(nameof(Tours));
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public void DeleteTourLogFromSelected(TourLog log)
        {
            _selectedTour?.Logs.Remove(log);
            OnPropertyChanged(nameof(SelectedTour));
        }

        protected override async void Save()
        {
            if (NewTour != null)
            {
                NewTour.Id = Guid.NewGuid();
                await _service.AddTourAsync(NewTour);
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

        protected override async void UpdateItem()
        {
            if (NewTour != null && SelectedTour != null)
            {
                await _service.UpdateTourAsync(NewTour);
                Tours[Tours.IndexOf(SelectedTour)] = NewTour;
                SelectedTour = NewTour;
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

        private void ExportItem()
        {
            if (SelectedTour == null)
                return;

            _service.ExportTour(SelectedTour);
        }

        private void GetRandomItem()
        {
            Tour? randomTour = Tours?[new Random().Next(0, Tours.Count)];
            if (randomTour != null)
                SelectedTour = randomTour;
        }

        protected override void CloseWindow()
        {
            NewTour = null;
            NewWindow?.Close();
        }
    }
}
