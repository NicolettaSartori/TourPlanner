using System.Globalization;
using TourPlanner.PresentationLayer.Converter;

namespace Tests.TourPlanner.Tests;

[TestFixture]
public class NumberToStarsConverterTests
{
    private NumberToStarsConverter _converter;

    [SetUp]
    public void SetUp()
    {
        _converter = new NumberToStarsConverter();
    }

    [TestCase(0, "☆☆☆☆☆")]
    [TestCase(1, "★☆☆☆☆")]
    [TestCase(3, "★★★☆☆")]
    [TestCase(5, "★★★★★")]
    public void Convert_ValidIntegers_ReturnsExpectedStars(int input, string expected)
    {
        var result = _converter.Convert(input, null, null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void Convert_InvalidType_ReturnsOriginalValue()
    {
        var input = "not a number";
        var result = _converter.Convert(input, null, null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void Convert_NullValue_ReturnsNull()
    {
        var result = _converter.Convert(null, null, null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ConvertBack_Always_ReturnsInputUnchanged()
    {
        var input = "★★★★★";
        var result = _converter.ConvertBack(input, null, null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo(input));
    }
}