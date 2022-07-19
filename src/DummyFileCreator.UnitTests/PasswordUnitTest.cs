using Xunit.Abstractions;

namespace DummyFileCreator.UnitTests;

public class PasswordUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public PasswordUnitTest(ITestOutputHelper testOutputHelper)
    {
        this._testOutputHelper = testOutputHelper;
    }

    public static IEnumerable<object?[]> Password_Generate_TestData()
    {
        yield return new object?[] { 32, 0 };
        yield return new object?[] { 32, 16 };
        yield return new object?[] { 32, 32 };
        yield return new object?[] { 128, 0 };
        yield return new object?[] { 128, 64 };
        yield return new object?[] { 128, 128 };
    }

    [Theory]
    [MemberData(nameof(PasswordUnitTest.Password_Generate_TestData))]
    public void Password_Generate_Test(int length, int numberOfNonAlphanumericCharacters)
    {
        var password = Password.Generate(length, numberOfNonAlphanumericCharacters);
        this._testOutputHelper.WriteLine(
            $"{nameof(Password.Generate)}({length},{numberOfNonAlphanumericCharacters})={password}");
        Assert.Equal(length, password.Length);
    }
}
