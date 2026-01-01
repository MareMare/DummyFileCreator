// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProgressScope.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator.App.Progress;

/// <summary>
/// 進捗表示する範囲を提供するインターフェイスを表します。
/// </summary>
internal interface IProgressScope : IAsyncDisposable
{
    /// <summary>
    /// 進行状況の報告者を取得します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="IProgressReporter" /> 型。
    /// <para>進行状況の報告者。既定値は null です。</para>
    /// </value>
    IProgressReporter Reporter { get; }
}
