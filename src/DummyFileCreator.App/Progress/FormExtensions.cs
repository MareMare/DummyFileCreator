// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormExtensions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator.App.Progress;

/// <summary>
/// <see cref="Form" /> クラスの拡張メソッドを提供します。
/// </summary>
internal static class FormExtensions
{
    /// <summary>
    /// 非同期操作として、所有側フォームを指定してフォームをユーザーに表示します。
    /// </summary>
    /// <param name="form">フォーム。</param>
    /// <param name="owner">フォームを所有するトップレベルウィンドウ。</param>
    /// <returns>完了を表すタスク。</returns>
    public static async Task ShowAsync(this Form form, IWin32Window owner)
    {
        var tcs = new TaskCompletionSource<bool>();
        form.FormClosed += (_, _) => tcs.TrySetResult(true);
        form.Show(owner);
        await tcs.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// 非同期操作として、所有側フォームを指定してフォームをモーダル表示します。
    /// </summary>
    /// <param name="form">フォーム。</param>
    /// <param name="owner">フォームを所有するトップレベルウィンドウ。</param>
    /// <returns>完了を表すタスク。</returns>
    public static async Task<DialogResult> ShowDialogAsync(this Form form, IWin32Window owner)
    {
        // NOTE: https://stackoverflow.com/a/33411037
        await Task.Yield();
        return form.IsDisposed
            ? DialogResult.Cancel
            : form.ShowDialog(owner);
    }
}
