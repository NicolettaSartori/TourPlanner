using Moq;
using TourPlanner.BusinessLayer.Enums;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.PresentationLayer.ViewModels;

namespace Tests.TourPlanner.Tests;

[TestFixture]
public class TourViewModelTests
{
    private TourViewModel _viewModel = null!;

    [SetUp]
    public void SetUp()
    {
        var mockLogsVm = new Mock<TourLogsViewModel>();
        _viewModel = new TourViewModel(mockLogsVm.Object);
    }

    private Tour GetTestTour()
    {
        return new Tour{
            Id = Guid.NewGuid(),
            Name = "Alpenwanderung",
            From = "Innsbruck",
            To = "Zell am See",
            Distance = "100km",
            EstimatedTime = "5h",
            Description = "Schöne Bergtour",
            TransportType = TransportType.Bike
        };
    }

    [Test]
    public void CanUpdate_ReturnsTrue_WhenAllFieldsAreFilled()
    {
        _viewModel.SelectedTour = GetTestTour();

        var result = _viewModel.CanUpdate();

        Assert.That(result, Is.True);
    }

    [TestCase("")]
    [TestCase("   ")]
    public void CanUpdate_ReturnsFalse_WhenAnyFieldIsInvalid(string invalidValue)
    {
        string valid = "valid";
        var fields = new[]
        {
            ("Name", invalidValue),
            ("From", invalidValue),
            ("To", invalidValue),
            ("Distance", invalidValue),
            ("EstimatedTime", invalidValue),
            ("Description", invalidValue),
        };

        foreach (var (fieldName, value) in fields)
        {
            var tour = new Tour
            {
                Id = Guid.NewGuid(),
                Name = valid,
                From = valid,
                To = valid,
                Distance = valid,
                EstimatedTime = valid,
                Description = valid,
                TransportType = TransportType.Bike
            };

            typeof(Tour).GetProperty(fieldName)!.SetValue(tour, value);

            _viewModel.SelectedTour = tour;
            Assert.That(_viewModel.CanUpdate(), Is.False);
        }
    }

    [Test]
    public void CanUpdate_ReturnsFalse_WhenSelectedTourIsNull()
    {
        _viewModel.SelectedTour = null;
        var result = _viewModel.CanUpdate();
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void CanSave_ReturnsTrue_WhenAllFieldsAreValid()
    {
        _viewModel.NewTour = GetTestTour();
    
        var result = _viewModel.CanSave();
    
        Assert.IsTrue(result);
    }
    
    [Test]
    public void CanSave_ReturnsFalse_WhenNewTourIsNull()
    {
        _viewModel.NewTour = null;
    
        Assert.IsFalse(_viewModel.CanSave());
    }

    [Test]
    public void CanDelete_ReturnsTrue_WhenSelectedTourIsSet()
    {
        _viewModel.SelectedTour = GetTestTour();
        Assert.IsTrue(_viewModel.CanDelete());
    }

    [Test]
    public void CanDelete_ReturnsFalse_WhenSelectedTourIsNull()
    {
        _viewModel.SelectedTour = null;
        Assert.IsFalse(_viewModel.CanDelete());
    }

    [Test]
    public void CanEdit_ReturnsTrue_WhenSelectedTourIsSet()
    {
        _viewModel.SelectedTour = GetTestTour();
        Assert.IsTrue(_viewModel.CanEdit());
    }

    [Test]
    public void CanEdit_ReturnsFalse_WhenSelectedTourIsNull()
    {
        _viewModel.SelectedTour = null;
        Assert.IsFalse(_viewModel.CanEdit());
    }
}