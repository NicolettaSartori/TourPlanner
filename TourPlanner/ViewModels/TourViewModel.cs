using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Factories;
using TourPlanner.Models;
using TourPlanner.MVVM;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : NewWindowViewModelBase
    {
        public ObservableCollection<Tour>? Tours { get; set; }
        
        private Tour? _selectedTour;

        private const string ImagePath = "/Images/";
        private readonly TourLogsViewModel _tourLogsViewModel;

        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                SetField(ref _selectedTour, value);
                ImageUri = $"{ImagePath}{_selectedTour?.Id.ToString()}.png";
                _tourLogsViewModel.TourLogs = new ObservableCollection<TourLog>(_selectedTour.Logs);
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
        
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand UpdateCommand { get; }
        

        public Tour NewTour { get; private set; } 
        public ObservableCollection<TransportType> TransportTypes { get; }

        public TourViewModel(TourLogsViewModel tourLogsViewModel)
        {
            _tourLogsViewModel = tourLogsViewModel;
            _tourLogsViewModel.TourViewModel = this;
            Tours = TourFactory.GetTours();
            SelectedTour = Tours.FirstOrDefault();
            AddCommand = new RelayCommand(_ => AddItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => SelectedTour != null);
            SaveCommand = new RelayCommand(_ => SaveNewTour(), _ => CanSaveTour());
            EditCommand = new RelayCommand(_ => EditItem(), _ => SelectedTour != null);
            UpdateCommand = new RelayCommand(_ => UpdateTour(), _ => CanUpdateTour());
        }
        
        private void AddItem()
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
        
        private void DeleteItem()
        {
            if (SelectedTour != null && Tours != null)
            {
                int index = Tours.IndexOf(SelectedTour);
                Tours.Remove(SelectedTour);
                // select the tour before 
                SelectedTour = (index <= 0) ? Tours.FirstOrDefault() : Tours[index - 1];
            }
        }
        
        public void DeleteTourLogFromSelected(TourLog log)
        {
            _selectedTour?.Logs.Remove(log);
            OnPropertyChanged(nameof(SelectedTour));
        }
        
        private void SaveNewTour()
        {
            if (NewTour != null)
            {
                NewTour.Id = Guid.NewGuid();
                Tours.Add(NewTour); // Tour zur Liste hinzufügen
                SelectedTour = NewTour;
                CloseWindow();
            }
        }

        private bool CanSaveTour()
        {
            return NewTour != null &&
                   !string.IsNullOrWhiteSpace(NewTour.Name) &&
                   !string.IsNullOrWhiteSpace(NewTour.From) &&
                   !string.IsNullOrWhiteSpace(NewTour.To) &&
                   !string.IsNullOrWhiteSpace(NewTour.Distance) &&
                   !string.IsNullOrWhiteSpace(NewTour.EstimatedTime) &&
                   !string.IsNullOrWhiteSpace(NewTour.Description);
        }


        protected override void Save()
        { 
            NewWindow?.Close();
        }
        
        private void EditItem()
        {
            if (SelectedTour == null) return;

            NewWindow = new EditTourWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }
        
        private void UpdateTour()
        {
            if (SelectedTour != null)
            {
                Console.WriteLine($"Tour {SelectedTour.Name} wurde aktualisiert.");
                CloseWindow();
            }
        }

        private bool CanUpdateTour()
        {
            return SelectedTour != null &&
                   !string.IsNullOrWhiteSpace(SelectedTour.Name) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.From) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.To) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.Distance) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.EstimatedTime) &&
                   !string.IsNullOrWhiteSpace(SelectedTour.Description);
        }

    }
}