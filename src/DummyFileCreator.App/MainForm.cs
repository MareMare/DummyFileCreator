// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using DummyFileCreator.App.Progress;

namespace DummyFileCreator.App;

/// <summary>
/// メインフォームを表します。
/// </summary>
public partial class MainForm : Form
{
    /// <summary>非同期操作の実行時間としての最低時間を表します。</summary>
    private static readonly TimeSpan DelayTimeSpan = TimeSpan.FromSeconds(2);

    /// <summary>進行状況の報告者を表します。</summary>
    private readonly IProgressReporter _progressReporter;

    /// <summary>
    /// <see cref="MainForm" /> クラスの新しいインスタンスを生成します。
    /// </summary>
    public MainForm()
    {
        this.InitializeComponent();
        this.InitializeHandlers();

        this._progressReporter = new ProgressReporter();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.ReleaseHandlers();
            if (this.components != null)
            {
                this.components.Dispose();
            }
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// イベントハンドラを登録します。
    /// </summary>
    private void InitializeHandlers()
    {
        this._progressReporter.ProgressInfoChanged += this.ProgressReporterOnProgressInfoChanged;
        this.buttonToCreate.Click += this.ButtonToCreateOnClick;
    }

    /// <summary>
    /// イベントハンドラを登録解除します。
    /// </summary>
    private void ReleaseHandlers()
    {
        this._progressReporter.ProgressInfoChanged -= this.ProgressReporterOnProgressInfoChanged;
        this.buttonToCreate.Click -= this.ButtonToCreateOnClick;
    }

    /// <summary>
    /// 進捗が変更になったときに発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void ProgressReporterOnProgressInfoChanged(object? sender, ProgressInfoEventArgs e)
    {
        var nowPercent = (int)e.Info.Percentage;
        Debug.Print($"{nowPercent:n0}");
        this.toolStripStatusLabel1.Text = e.Info.Message;
        this.toolStripProgressBar1.Value = nowPercent;
    }

    /// <summary>
    /// [ダミーファイル生成] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
    /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
    private async void ButtonToCreateOnClick(object? sender, EventArgs e)
    {
        var reporter = this._progressReporter;
        try
        {
            reporter.ReportStarting("書き込み中です。しばらくお待ちください。");

            var oldValue = 0;
            await DummyFile.CreateAsync(
                    "dummy.txt",
                    "500MB",
                    "10MB",
                    fillWithZeros: true,
                    progress: (current, total) =>
                    {
                        var nowPercent = (int)ProgressInfo.CalculatePercentage(current, total);
                        var nowValue = nowPercent / 5;
                        if (oldValue != nowValue)
                        {
                            oldValue = nowValue;
                            reporter.Report(ProgressInfo.New("書き込み中です。しばらくお待ちください。", nowPercent));
                        }
                    })
                .ConfigureAwait(true);
            await reporter.ReportCompletedAsync("書き込みが完了しました。").ConfigureAwait(true);
        }
#pragma warning disable CA1031 // 一般的な例外の種類はキャッチしません
        catch
#pragma warning restore CA1031 // 一般的な例外の種類はキャッチしません
        {
            await reporter
                .ReportFailedAsync("書き込み中に例外が発生しました。", MainForm.DelayTimeSpan)
                .ConfigureAwait(true);
        }
    }
}
