using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Enums;
using TourPlanner.Models;
using TourPlanner.MVVM;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : NewWindowViewModelBase
    {
        public ObservableCollection<Tour> Tours { get; }
        
        private Tour? _selectedTour;

        private const string ImagePath = "/Images/";

        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                SetField(ref _selectedTour, value);
                ImageUri = $"{ImagePath}{_selectedTour?.Id.ToString()}.png";
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

        public TourViewModel()
        {
            TransportTypes = new ObservableCollection<TransportType>((TransportType[])Enum.GetValues(typeof(TransportType))); // Enum-Werte für die Auswahl

            Tours = [
                new Tour
                {
                    Id = Guid.Parse("d53b5c0e-9240-43b3-9b3d-8cefac645f38"),
                    Name = "Ruhrtal Radweg",
                    From = "Winterberg",
                    To = "Meschede",
                    TransportType = TransportType.Bike,
                    Distance = "40,5 km",
                    EstimatedTime = "2h 27min",
                    Description = "Mittelschwere Fahrradtour. Gute Grundkondition erforderlich. Überwiegend befestigte Wege. Kein besonderes Können erforderlich."
                },
                new Tour
                {
                    Id = Guid.Parse("9a9ecb9b-00db-4f5f-8385-0da3df583f44"),
                    Name = "Elberadweg",
                    From = "Elbquelle",
                    To = "Vrchlabí",
                    TransportType = TransportType.Bike,
                    Distance = "17,5 km",
                    EstimatedTime = "1h 19min",
                    Description = "Mittelschwere Fahrradtour. Gute Grundkondition erforderlich. Überwiegend befestigte Wege. Kein besonderes Können erforderlich."
                },
                new Tour
                {
                    Id = Guid.Parse("8f7cfed4-ae14-4eab-a2bc-881104685598"),
                    Name = "Etschtalradweg",
                    From = "Reschen",
                    To = "Schlanders",
                    TransportType = TransportType.Bike,
                    Distance = "45,4 km",
                    EstimatedTime = "2h 28min",
                    Description = "Von Reschen fährst du dann erst einmal an der Westseite des Reschensees entlang, wobei du einen fantastischen Blick auf das blaue Wasser und die dahinter emporragenden Berge hast. Am Ende des Stausees kannst du dich auf eine fast zehn Kilometer lange Abfahrt freuen, auf der du es gemütlich rollen lassen kannst."
                },
                new Tour
                {
                    Id = Guid.Parse("c52e645e-09a2-48ce-b7f7-34b418da3145"),
                    Name = "Jakobsweg",
                    From = "Sarria",
                    To = "Portomarin",
                    TransportType = TransportType.Hike,
                    Distance = "22,3 km",
                    EstimatedTime = "6h 9min",
                    Description = "Schwere Wanderung. Sehr gute Kondition erforderlich. Leicht begehbare Wege. Kein besonderes Können erforderlich."
                },
            ];
            
            SelectedTour = Tours.FirstOrDefault();
            AddCommand = new RelayCommand(_ => AddItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => SelectedTour != null);
            SaveCommand = new RelayCommand(_ => SaveNewTour(), _ => NewTour != null);
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
            if (SelectedTour != null)
            {
                int index = Tours.IndexOf(SelectedTour);
                Tours.Remove(SelectedTour);
                // select the tour before 
                SelectedTour = (index <= 0) ? Tours.FirstOrDefault() : Tours[index - 1];
            }
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
            return !string.IsNullOrWhiteSpace(NewTour?.Name) &&
                   !string.IsNullOrWhiteSpace(NewTour?.From) &&
                   !string.IsNullOrWhiteSpace(NewTour?.To) &&
                   !string.IsNullOrWhiteSpace(NewTour?.Distance) &&
                   !string.IsNullOrWhiteSpace(NewTour?.EstimatedTime);
        }

        protected override void Save()
        { 
            Console.WriteLine("in Save");
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