// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using DummyFileCreator.App.Progress;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DummyFileCreator.App;

/// <summary>
/// アプリケーションのエントリポイントを提供します。
/// </summary>
internal static class Program
{
    /// <summary>
    /// アプリケーションのメインエントリポイントです。
    /// </summary>
    /// <param name="args">コマンドライン引数。</param>
    [STAThread]
    private static void Main(string[] args)
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        WinFormsApplication.Run<MainForm>(Program.CreateHostBuilder(args));
    }

    /// <summary>
    /// <see cref="IHostBuilder" /> を生成します。
    /// </summary>
    /// <param name="args">コマンドライン引数。</param>
    /// <returns>生成された <see cref="IHostBuilder" />。</returns>
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostingContext, services) =>
                services.AddConfiguredServices(hostingContext.Configuration, hostingContext.HostingEnvironment));

    /// <summary>
    /// 各サービスの依存関係を追加します。
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />。</param>
    /// <param name="configuration"><see cref="IConfiguration" />。</param>
    /// <param name="environment"><see cref="IHostEnvironment" />。</param>
    /// <returns>依存関係が追加された <see cref="IServiceCollection" />。</returns>
    // ReSharper disable once UnusedMethodReturnValue.Local
    private static IServiceCollection AddConfiguredServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment) =>
        services
            .AddProgressForm()
            .AddTransient<MainForm>();
}
