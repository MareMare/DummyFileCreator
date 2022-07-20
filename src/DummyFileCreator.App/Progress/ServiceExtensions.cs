// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceExtensions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace DummyFileCreator.App.Progress;

/// <summary>
/// <see cref="IServiceCollection" /> の拡張機能を提供します。
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// <see cref="ProgressForm" /> に関するサービスの依存関係を追加します。
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" />。</param>
    /// <returns>依存関係が追加された <see cref="IServiceCollection" />。</returns>
    public static IServiceCollection AddProgressForm(this IServiceCollection services) =>
        services.AddScoped<ProgressForm>();

    /// <summary>
    /// <see cref="ProgressForm" /> を使用します。
    /// </summary>
    /// <param name="serviceProvider"><see cref="IServiceProvider" />。</param>
    /// <param name="owner">フォームを所有するトップレベルウィンドウ。</param>
    /// <param name="configure"><see cref="ProgressForm" /> を構築するメソッドのデリゲート。</param>
    /// <returns><see cref="IProgressScope" />。</returns>
    public static IProgressScope UseProgressForm(
        this IServiceProvider serviceProvider,
        IWin32Window owner,
        Action<ProgressForm>? configure = null)
    {
        var progressForm = serviceProvider.GetRequiredService<ProgressForm>();
        configure?.Invoke(progressForm);
        return progressForm.UseProgressFormScope(owner);
    }
}
