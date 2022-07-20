// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProgressReporter.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator.App.Progress;

/// <summary>
/// 進行状況の報告を提供するインターフェイスを表します。
/// </summary>
public interface IProgressReporter
{
    /// <summary>
    /// 進行状況の値が報告されたときに発生するイベントを表します。
    /// </summary>
    event EventHandler<ProgressInfoEventArgs>? ProgressInfoChanged;

    /// <summary>
    /// 進行状況の更新を報告します。
    /// </summary>
    /// <param name="info">進捗情報。</param>
    void Report(ProgressInfo info);

    /// <summary>
    /// 進行状況のクリアを報告します。
    /// </summary>
    void ReportClear();

    /// <summary>
    /// 進行状況の開始を報告します。
    /// </summary>
    /// <param name="message">進捗メッセージ。</param>
    void ReportStarting(string message);

    /// <summary>
    /// 非同期操作として、進行状況の完了を報告します。
    /// </summary>
    /// <param name="message">進捗メッセージ。</param>
    /// <param name="waitingTimeSpan">表示待機時間。</param>
    /// <returns>完了を表す <see cref="Task" />。</returns>
    Task ReportCompletedAsync(string message, TimeSpan? waitingTimeSpan = null);

    /// <summary>
    /// 非同期操作として、進行状況の失敗を報告します。
    /// </summary>
    /// <param name="message">進捗メッセージ。</param>
    /// <param name="waitingTimeSpan">表示待機時間。</param>
    /// <returns>完了を表す <see cref="Task" />。</returns>
    Task ReportFailedAsync(string message, TimeSpan? waitingTimeSpan = null);
}
