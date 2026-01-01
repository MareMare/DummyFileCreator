// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.CommandLine;
using DummyFileCreator;
using ShellProgressBar;

var outputFilePathOption = new Option<FileInfo>("--file", "-f")
{
    Description = "Specify the full path of the file to be generated.",
    Required = true,
};

var outputSizeOption = new Option<string>("--size", "-s")
{
    Description = "Specifies the size of the file to be generated.The default is \"10MB\".",
    DefaultValueFactory = _ => "10MB",
    Required = true,
};

var bufferSizeOption = new Option<string>("--buffer", "-b")
{
    Description = "Specifies the buffer size when generating files. The default is \"10MB\".",
    DefaultValueFactory = _ => "10MB",
};

var fillWithZerosOption = new Option<bool>("--fillWithZeros", "-z")
{
    Description = "Specify if you want to fill with zeros.If not specified, it will be filled with a random string.",
    DefaultValueFactory = _ => false,
};

var command = new RootCommand(description: "Dummy File Generation Tool.")
{
    outputFilePathOption,
    outputSizeOption,
    bufferSizeOption,
    fillWithZerosOption,
};

command.Options.Add(outputFilePathOption);
command.Options.Add(outputSizeOption);
command.Options.Add(bufferSizeOption);
command.Options.Add(fillWithZerosOption);
command.SetAction(async parseResult =>
{
    var outputFilePath = parseResult.GetRequiredValue(outputFilePathOption);
    var outputSize = parseResult.GetRequiredValue(outputSizeOption);
    var bufferSize = parseResult.GetRequiredValue(bufferSizeOption);
    var fillWithZeros = parseResult.GetRequiredValue(fillWithZerosOption);
    await ExecuteCommand(outputFilePath, outputSize, bufferSize, fillWithZeros).ConfigureAwait(false);
});
await command.Parse(args).InvokeAsync().ConfigureAwait(false);

static async ValueTask ExecuteCommand(
    FileSystemInfo outputFilePath,
    string outputSize,
    string bufferSize,
    bool fillWithZeros)
{
    // dotnet run -- --file=c:\temp\dummy.txt --size=10KB --buffer=5KB --fillWithZeros
    // --file=c:\temp\dummy.txt --size=10KB --buffer=5KB --fillWithZeros
    // --file c:\temp\dummy.txt --size 10KB --buffer 5KB --fillWithZeros
    var progressOptions = new ProgressBarOptions
    {
        ProgressCharacter = '-',
        ProgressBarOnBottom = true,
        CollapseWhenFinished = false,
    };
    using var progressBar = new ProgressBar(100, $"{Path.GetFileName(outputFilePath.Name)}({outputSize})...", progressOptions);
    var progress = progressBar.AsProgress<double>();

    await DummyFile.CreateAsync(
            outputFilePath.Name,
            outputSize,
            bufferSize,
            fillWithZeros,
            (current, total) =>
            {
                var nowPercent = (int)CalculatePercentage(current, total);
                progress.Report(nowPercent / 100d);
            })
        .ConfigureAwait(false);
}

static double CalculatePercentage(long current, long total)
{
    var value = 100d * current / total;
    var percent = value < 0d ? 0d : value > 100d ? 100d : value;
    return percent;
}
