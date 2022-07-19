using Xunit.Abstractions;

namespace DummyFileCreator.UnitTests;

public class ByteSizeExtensionsUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ByteSizeExtensionsUnitTest(ITestOutputHelper testOutputHelper)
    {
        this._testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ByteSizeExtensions_ToPrettySize_Test()
    {
        var actual = 102400L.ToPrettySize();
        Assert.Equal("100.0 KB", actual);
    }

    public static IEnumerable<object?[]> ByteSizeExtensions_TryParseToByteSize_TestData()
    {
        yield return new object?[] { "1.00B", true, 1 };
        yield return new object?[] { "1.00KB", true, 1_024 };
        yield return new object?[] { "1.00MB", true, 1_048_576 };
        yield return new object?[] { "10.00MB", true, 10_485_760 };
        yield return new object?[] { "1.00GB", true, 1_073_741_824 };
        yield return new object?[] { "1.00TB", true, 1_099_511_627_776 };
        yield return new object?[] { "1.00PB", true, 1_125_899_906_842_624 };
    }

    [Theory]
    [MemberData(nameof(ByteSizeExtensionsUnitTest.ByteSizeExtensions_TryParseToByteSize_TestData))]
    public void ByteSizeExtensions_TryParseToByteSize_Test(string text, bool isParsed, long exceptedValue)
    {
        var actualParsed = text.TryParseToByteSize(out var actualValue);
        this._testOutputHelper.WriteLine($"{text} is {actualValue:n0}.");

        Assert.Equal(isParsed, actualParsed);
        Assert.Equal(exceptedValue, actualValue);
    }
}
