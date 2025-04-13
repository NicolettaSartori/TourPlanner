using System.Collections.ObjectModel;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.PresentationLayer.ViewModels;

namespace Tests.TourPlanner.ViewModels;

public class TourViewModelTests
{
    private TourViewModel _tourViewModel;
    
    [SetUp]
    public void Setup()
    {
        var tours = new ObservableCollection<Tour>
        {
            new Tour
            {
                Id = Guid.Parse("d53b5c0e-9240-43b3-9b3d-8cefac645f38"),
                Name = "Ruhrtal Radweg",
                From = "Winterberg",
                To = "Meschede",
                TransportType = TransportType.Bike,
                Distance = "40,5 km",
                EstimatedTime = "2h 27min",
                Description = "",
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
                Description = "",
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
                Description = "",
            },
        };
        _tourViewModel = new TourViewModel(new TourLogsViewModel());
        _tourViewModel.Tours = tours;
        _tourViewModel.SelectedTour = tours.FirstOrDefault();
        
    }

    [Test]
    public void DeleteCommand_ShouldDeleteTour()
    {
        int initialCount = _tourViewModel.Tours.Count;
        Tour delTour = _tourViewModel.Tours[0];
        
        _tourViewModel.DeleteCommand.Execute(delTour);
        
        Assert.That(initialCount - 1, Is.EqualTo(_tourViewModel.Tours.Count));
        Assert.That(_tourViewModel.Tours.Contains(delTour), Is.False);
    }
    
    // If a tour is deleted it should automatically mark the tour before as selected
    [Test]
    public void DeleteCommand_ShouldSelectTourThatsBefore()
    {
        Tour delTour = _tourViewModel.Tours[2];
        _tourViewModel.SelectedTour = delTour;
        
        _tourViewModel.DeleteCommand.Execute(delTour);
        
        Assert.That(_tourViewModel.SelectedTour.Id, Is.EqualTo(_tourViewModel.Tours[1].Id));
    }
    
    // If a tour is deleted it should automatically mark the tour before as selected
    // If the first tour is selected, it should mark the new first tour as selected
    [Test]
    public void DeleteCommand_ShouldSelectFirstTour()
    {
        Tour delTour = _tourViewModel.Tours[0];
        _tourViewModel.SelectedTour = delTour;
        
        _tourViewModel.DeleteCommand.Execute(delTour);
        
        Assert.That(_tourViewModel.SelectedTour.Id, Is.EqualTo(_tourViewModel.Tours[0].Id));
    }
    
    [Test]
    public void DeleteTourLogFromSelected_ShouldDeleteTourLog()
    {
        _tourViewModel.Tours[0].Logs =
        [
            new TourLog
            {
                DateTime = DateTime.Parse("2021-10-25"),
                Difficulty = Difficulty.Medium,
                TotalDistance = "45,4 km",
                TotalTime = "2h 30min",
                Rating = 5,
            },
        ];
            
        _tourViewModel.DeleteTourLogFromSelected(_tourViewModel.Tours[0].Logs[0]);
        
        Assert.That(_tourViewModel.SelectedTour.Logs.Count, Is.EqualTo(0));
    }
}