// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinFormsApplication.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DummyFileCreator.App
{
    /// <summary>
    /// 汎用ホストに対応した <see cref="System.Windows.Forms" /> アプリケーションを表します。
    /// </summary>
    internal static class WinFormsApplication
    {
        /// <summary>
        /// 現在のスレッドで標準のアプリケーションメッセージループの実行を開始し、<typeparamref name="TMainForm" /> フォームを表示します。
        /// </summary>
        /// <typeparam name="TMainForm">メインウィンドウの型。</typeparam>
        /// <param name="hostBuilder"><see cref="IHostBuilder" />。</param>
        public static void Run<TMainForm>(IHostBuilder hostBuilder)
            where TMainForm : Form
        {
            ArgumentNullException.ThrowIfNull(hostBuilder);

            using var host = hostBuilder.Build();
            using var serviceScope = host.Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;

            // var serviceProviderIsService = services.GetService<IServiceProviderIsService>();
            var env = services.GetRequiredService<IHostEnvironment>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(WinFormsApplication));
            try
            {
                logger.LogInformation("起動します。{env}", env.EnvironmentName);

                Application.Run(services.GetRequiredService<TMainForm>());

                logger.LogInformation("終了しました。{env}", env.EnvironmentName);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "起動中に例外が発生しました。{ex}", ex.Message);
                throw;
            }
        }
    }
}
