// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressInfo.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator.App.Progress;

/// <summary>
/// 進捗情報を表します。
/// </summary>
internal class ProgressInfo
{
    /// <summary>
    /// <see cref="ProgressInfo" /> クラスの新しいインスタンスを生成します。
    /// </summary>
    private ProgressInfo()
    {
    }

    /// <summary>
    /// 進捗メッセージを取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="string" /> 型。
    /// <para>進捗メッセージ。既定値は <see langword="null" /> です。</para>
    /// </value>
    public string Message { get; private init; } = null!;

    /// <summary>
    /// 進捗率 (0～100%) を取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="double" /> 型。
    /// <para>進捗率 (0～100%)。既定値は 0d です。</para>
    /// </value>
    public double Percentage { get; private init; }

    /// <summary>
    /// 失敗となった例外を取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="Exception" /> 型。
    /// <para>失敗となった例外。既定値は <see langword="null" /> です。</para>
    /// </value>
    public Exception? Error { get; private init; }

    /// <summary>
    /// 失敗かどうかを取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="bool" /> 型。
    /// <para>失敗した場合は <see langword="true" />。それ以外は <see langword="false" />。</para>
    /// </value>
    public bool IsFailure { get; private init; }

    /// <summary>
    /// 進捗率 (0～100%) を算出します。
    /// </summary>
    /// <param name="current">現在値。</param>
    /// <param name="total">最終値。</param>
    /// <returns>進捗率 (0～100%)。</returns>
    public static double CalculatePercentage(long current, long total)
    {
        var value = 100d * current / total;
        var percent = value < 0d ? 0d : value > 100d ? 100d : value;
        return percent;
    }

    /// <summary>
    /// 新しいインスタンスを生成します。
    /// </summary>
    /// <param name="message">進捗メッセージ。</param>
    /// <param name="percent">進捗率 (0～100%)。</param>
    /// <param name="isFailure">失敗した場合は <see langword="true" />。それ以外は <see langword="false" />。</param>
    /// <param name="error">失敗となった例外。</param>
    /// <returns>進捗情報。</returns>
    public static ProgressInfo New(string message, double percent, bool isFailure = false, Exception? error = null) =>
        new () { Message = message, Percentage = percent, IsFailure = isFailure, Error = error };
}
