using System.Collections.ObjectModel;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.DataAccessLayer.Models;

namespace TourPlanner.BusinessLayer.Factories;

public class TourFactory
{
    public static ObservableCollection<Tour> GetTours()
    {
        return
        [
            new Tour
            {
                Id = Guid.Parse("d53b5c0e-9240-43b3-9b3d-8cefac645f38"),
                Name = "Ruhrtal Radweg",
                From = "Winterberg",
                To = "Meschede",
                TransportType = TransportType.Bike,
                Distance = "40,5 km",
                EstimatedTime = "2h 27min",
                Description =
                    "Mittelschwere Fahrradtour. Gute Grundkondition erforderlich. Überwiegend befestigte Wege. Kein besonderes Können erforderlich.",
                Logs = [
                    new TourLog
                    {
                        Id = Guid.NewGuid(),
                        DateTime = DateTime.Parse("2019-11-1").ToUniversalTime(),
                        Comment = "Wunderschöne Etappe mit Abstechern zu den Bruchhauser Steinen und dem Hennestausee bei Meschede.",
                        Difficulty = Difficulty.Medium,
                        TotalDistance = "40,5 km",
                        TotalTime = "2h 27min",
                        Rating = 5,
                        TourId = Guid.Parse("d53b5c0e-9240-43b3-9b3d-8cefac645f38"),
                    },
                    new TourLog
                    {
                        Id = Guid.NewGuid(),
                        DateTime = DateTime.Parse("2017-12-19").ToUniversalTime(),
                        Comment = "Los geht es mit dem RuhrtalRadweg in dem schönen Winterberg. Zum Startpunkt kommst du ganz unkompliziert mit der Bahn. Von dort aus geht es dann in 41 Kilometern meistens bergab bis zum ersten Etappenziel in Meschede.",
                        Difficulty = Difficulty.Medium,
                        TotalDistance = "40,4 km",
                        TotalTime = "2h 20min",
                        Rating = 4,
                        TourId = Guid.Parse("d53b5c0e-9240-43b3-9b3d-8cefac645f38"),
                    },
                ],
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
                Description = "Mittelschwere Fahrradtour. Gute Grundkondition erforderlich. Überwiegend befestigte Wege. Kein besonderes Können erforderlich.",
                Logs = [
                    new TourLog
                    {
                        Id = Guid.NewGuid(),
                        DateTime = DateTime.Parse("2017-06-19").ToUniversalTime(),
                        Comment = "Von tschechischem Bier höchster Braukunst über opulent bayrischen Schmankerl bis zur Vielfalt aus Fischen und Meeresfrüchten aus der Norddeutschen Küche. Naturliebhaber, Feinschmecker und natürlich auch Sportler kommen voll auf ihre Kosten.",
                        Difficulty = Difficulty.Easy,
                        TotalDistance = "17,52 km",
                        TotalTime = "1h 25min",
                        Rating = 5,
                        TourId = Guid.Parse("9a9ecb9b-00db-4f5f-8385-0da3df583f44"),
                    },
                ],
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
                Description = "Von Reschen fährst du dann erst einmal an der Westseite des Reschensees entlang, wobei du einen fantastischen Blick auf das blaue Wasser und die dahinter emporragenden Berge hast. Am Ende des Stausees kannst du dich auf eine fast zehn Kilometer lange Abfahrt freuen, auf der du es gemütlich rollen lassen kannst.",
                Logs = [
                    new TourLog
                    {
                        Id = Guid.NewGuid(),
                        DateTime = DateTime.Parse("2021-10-25").ToUniversalTime(),
                        Comment = "In diesem charmanten von Bergen umgebenen Ort lohnt sich ein Spaziergang durch das schöne Zentrum. Übernachtungsmöglichkeiten stehen dir in dem Ferienort in allen Formen und Preisklassen zur Verfügung.",
                        Difficulty = Difficulty.Medium,
                        TotalDistance = "45,4 km",
                        TotalTime = "2h 30min",
                        Rating = 5,
                        TourId = Guid.Parse("8f7cfed4-ae14-4eab-a2bc-881104685598"),
                    },
                ],
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
                Description =
                    "Schwere Wanderung. Sehr gute Kondition erforderlich. Leicht begehbare Wege. Kein besonderes Können erforderlich.",
            },
        ];
    }
}