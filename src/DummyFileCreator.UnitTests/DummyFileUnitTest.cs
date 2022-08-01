using System.Diagnostics;
using Xunit.Abstractions;

namespace DummyFileCreator.UnitTests;

public class DummyFileUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DummyFileUnitTest(ITestOutputHelper testOutputHelper)
    {
        this._testOutputHelper = testOutputHelper;
    }

    public static IEnumerable<object?[]> DummyFile_CreateAsync_TestData()
    {
        yield return new object?[] { "100MB", "1KB", false };
        yield return new object?[] { "100MB", "10MB", false };
        yield return new object?[] { "100MB", "1KB", true };
        yield return new object?[] { "100MB", "10MB", true };

        // yield return new object?[] { "1GB", "10MB", false };
        // yield return new object?[] { "1GB", "10MB", true };
        // yield return new object?[] { "1GB", "1KB", false };
        // yield return new object?[] { "1GB", "1KB", true };
    }

    [Theory]
    [MemberData(nameof(DummyFileUnitTest.DummyFile_CreateAsync_TestData))]
    public async Task DummyFile_CreateAsync_Test(string sizeText, string bufferSizeText, bool fillWithZeros)
    {
        var path = @$"dummy{sizeText}_{bufferSizeText}_{(fillWithZeros ? "Zero" : "Random")}.txt";

        var parsed1 = sizeText.TryParseToByteSize(out var targetBytes);
        Assert.True(parsed1);

        var sw = Stopwatch.StartNew();
        await DummyFile.CreateAsync(path, sizeText, bufferSizeText, fillWithZeros).ConfigureAwait(false);
        sw.Stop();

        var result = new FileInfo(path);
        Assert.True(result.Length >= targetBytes);

        this._testOutputHelper.WriteLine($"要求サイズ={sizeText} 生成サイズ={result.Length:n0} バッファ={bufferSizeText} IsZero={fillWithZeros} 所要時間={sw.Elapsed}");
    }

    [Fact]
    public async Task DummyFile_CreateAsync_And_DisposeAsync_Test()
    {
        var path = "dummy.txt";

        // First creation.
        await DummyFile.CreateAsync(path, "100MB", "5MB", fillWithZeros: true).ConfigureAwait(false);
        // Second creation of the same file path.
        await DummyFile.CreateAsync(path, "100MB", "5MB", fillWithZeros: false).ConfigureAwait(false);

        // Check if it can open.
        var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        await using var streamAsyncDisposable = stream.ConfigureAwait(false);
        Assert.True(stream.CanRead);
    }
}
