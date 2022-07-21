// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.CommandLine;
using DummyFileCreator;
using ShellProgressBar;

var outputFilePathOption = new Option<FileInfo>(
    aliases: new[] { "--file", "-f" },
    description: "Specify the full path of the file to be generated.")
{
    IsRequired = true,
};

var outputSizeOption = new Option<string>(
    aliases: new[] { "--size", "-s" },
    description: "Specifies the size of the file to be generated.The default is \"10MB\".",
    getDefaultValue: () => "10MB")
{
    IsRequired = true,
};

var bufferSizeOption = new Option<string>(
    aliases: new[] { "--buffer", "-b" },
    description: "Specifies the buffer size when generating files. The default is \"10MB\".",
    getDefaultValue: () => "10MB");

var fillWithZerosOption = new Option<bool>(
    aliases: new[] { "--fillWithZeros", "-z" },
    description: "Specify if you want to fill with zeros.If not specified, it will be filled with a random string.",
    getDefaultValue: () => false);

var command = new RootCommand(description: "Dummy File Generation Tool.")
{
    outputFilePathOption,
    outputSizeOption,
    bufferSizeOption,
    fillWithZerosOption,
};

command.SetHandler(
    async (outputFilePath, outputSize, bufferSize, fillWithZeros) =>
        await ExecuteCommand(outputFilePath, outputSize, bufferSize, fillWithZeros).ConfigureAwait(false),
    outputFilePathOption,
    outputSizeOption,
    bufferSizeOption,
    fillWithZerosOption);

await command.InvokeAsync(args).ConfigureAwait(false);

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
