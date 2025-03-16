using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Enums;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> Tours { get; }
        
        private Tour? _selectedTour;

        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                SetField(ref _selectedTour, value);
                ImageUri = $"/Images/{_selectedTour.Id.ToString()}.png";
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

        public TourViewModel()
        {
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
        }
    }
}