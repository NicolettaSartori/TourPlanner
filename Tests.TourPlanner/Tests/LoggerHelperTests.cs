using System.Reflection;
using log4net;
using TourPlanner.Infrastructure;

namespace Tests.TourPlanner.Tests;

[TestFixture]
public class LoggerHelperTests
{
    private const string ValidConfigContent = @"<log4net></log4net>";
    private string _tempConfigPath = null!;

    [SetUp]
    public void SetUp()
    {
        var field = typeof(LoggerHelper).GetField("_isInitialized", BindingFlags.NonPublic | BindingFlags.Static);
        field!.SetValue(null, false);
        _tempConfigPath = Path.GetTempFileName();
        File.WriteAllText(_tempConfigPath, ValidConfigContent);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_tempConfigPath))
            File.Delete(_tempConfigPath);
    }

    [Test]
    public void Initialize_WithInvalidPath_ShouldThrowFileNotFoundException()
    {
        string invalidPath = Path.Combine(Path.GetTempPath(), "nonexistent.config");
        var ex = Assert.Throws<FileNotFoundException>(() => LoggerHelper.Initialize(invalidPath));
        Assert.That(ex!.FileName, Is.EqualTo(invalidPath));
    }

    [Test]
    public void Initialize_Twice_ShouldNotThrowOrReconfigure()
    {
        LoggerHelper.Initialize(_tempConfigPath);
        File.WriteAllText(_tempConfigPath, "<log4net><extra/></log4net>");
        Assert.DoesNotThrow(() => LoggerHelper.Initialize(_tempConfigPath));
    }

    [Test]
    public void GetLogger_ShouldReturnILogInstance()
    {
        var logger = LoggerHelper.GetLogger(typeof(LoggerHelperTests));
        Assert.That(logger, Is.Not.Null);
        Assert.That(logger, Is.InstanceOf<ILog>());
    }
}