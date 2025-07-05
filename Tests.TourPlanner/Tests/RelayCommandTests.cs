using TourPlanner.PresentationLayer.MVVM;

namespace Tests.TourPlanner.Tests;

[TestFixture]
public class RelayCommandTests
{
    [Test]
    public void Execute_ShouldInvokeAction()
    {
        bool wasCalled = false;
        var command = new RelayCommand(_ => wasCalled = true);

        command.Execute(null);

        Assert.IsTrue(wasCalled);
    }

    [Test]
    public void CanExecute_ShouldReturnTrue()
    {
        var command = new RelayCommand(_ => { });

        var result = command.CanExecute(null);

        Assert.IsTrue(result);
    }

    [Test]
    public void CanExecute_ShouldReturnFalse()
    {
        var command = new RelayCommand(_ => { }, _ => false);

        var result = command.CanExecute(null);

        Assert.IsFalse(result);
    }

    [Test]
    public void CanExecute_ShouldPassParameterToPredicate()
    {
        object? receivedParam = null;
        var expectedParam = new object();
        var command = new RelayCommand(_ => { }, param =>
        {
            receivedParam = param;
            return true;
        });

        command.CanExecute(expectedParam);

        Assert.That(receivedParam, Is.SameAs(expectedParam));
    }
}