// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressInfoEventArgs.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator.App.Progress;

/// <summary>
/// <see cref="IProgressReporter.ProgressInfoChanged" /> イベントのイベントデータを表します。
/// </summary>
internal class ProgressInfoEventArgs : EventArgs
{
    /// <summary>
    /// <see cref="ProgressInfoEventArgs" /> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="info">進捗情報。</param>
    internal ProgressInfoEventArgs(ProgressInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);
        this.Info = info;
    }

    /// <summary>
    /// 進捗情報を取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="ProgressInfo" /> 型。
    /// <para>進捗情報。既定値は <see langword="null" /> です。</para>
    /// </value>
    public ProgressInfo Info { get; }
}
