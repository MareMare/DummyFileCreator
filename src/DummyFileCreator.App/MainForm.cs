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
        this._progressReporter = new ProgressReporter();
        this.InitializeComponent();

        this.SetSizes();
        this.SetRadioButtons();
        this.SetControlsEnabled();

        this.InitializeStatusBar();
        this.InitializeHandlers();
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
        this.buttonToBrowseOutputPath.Click += this.ButtonToBrowseOutputPathOnClick;
        this.comboBoxOfOutputSize.SelectedIndexChanged += this.ComboBoxOfOutputSizeOnSelectedIndexChanged;
        this.comboBoxOfBufferSize.SelectedIndexChanged += this.ComboBoxOfBufferSizeOnSelectedIndexChanged;
        this.radioButtonOfRandom.CheckedChanged += this.RadioButtonOfRandomOnCheckedChanged;
        this.radioButtonOfZeros.CheckedChanged += this.RadioButtonOfZerosOnCheckedChanged;
        this.textBoxOfOutputPath.TextChanged += this.TextBoxOfOutputPathOnTextChanged;
    }

    /// <summary>
    /// イベントハンドラを登録解除します。
    /// </summary>
    private void ReleaseHandlers()
    {
        this._progressReporter.ProgressInfoChanged -= this.ProgressReporterOnProgressInfoChanged;
        this.buttonToCreate.Click -= this.ButtonToCreateOnClick;
        this.buttonToBrowseOutputPath.Click -= this.ButtonToBrowseOutputPathOnClick;
        this.comboBoxOfOutputSize.SelectedIndexChanged -= this.ComboBoxOfOutputSizeOnSelectedIndexChanged;
        this.comboBoxOfBufferSize.SelectedIndexChanged -= this.ComboBoxOfBufferSizeOnSelectedIndexChanged;
        this.radioButtonOfRandom.CheckedChanged -= this.RadioButtonOfRandomOnCheckedChanged;
        this.radioButtonOfZeros.CheckedChanged -= this.RadioButtonOfZerosOnCheckedChanged;
        this.textBoxOfOutputPath.TextChanged -= this.TextBoxOfOutputPathOnTextChanged;
    }

    /// <summary>
    /// ステータスバーを初期化します。
    /// </summary>
    private void InitializeStatusBar()
    {
        this.toolStripProgressBar1.Visible = false;
        this.toolStripStatusLabel1.Text = null;
    }

    /// <summary>
    /// 各サイズを設定します。
    /// </summary>
    private void SetSizes()
    {
        this.numericUpDownOfOutputSize.Value = 1024;
        this.comboBoxOfOutputSize.SelectedItem = "MB";

        this.numericUpDownOfBufferSize.Value = 10;
        this.comboBoxOfBufferSize.SelectedItem = "MB";
    }

    /// <summary>
    /// ラジオボタンを設定します。
    /// </summary>
    private void SetRadioButtons() => this.radioButtonOfRandom.Checked = true;

    /// <summary>
    /// <see cref="Control.Enabled" /> を設定します。
    /// </summary>
    private void SetControlsEnabled()
    {
        var hasPath = !string.IsNullOrEmpty(this.textBoxOfOutputPath.Text);
        var isSelectedUnits = this.comboBoxOfOutputSize.SelectedIndex >= 0 &&
                              this.comboBoxOfBufferSize.SelectedIndex >= 0;
        var isSelectedType = this.radioButtonOfRandom.Checked || this.radioButtonOfZeros.Checked;
        this.buttonToCreate.Enabled = hasPath && isSelectedUnits && isSelectedType;
    }

    /// <summary>
    /// 非同期操作として、ダミーファイルを生成します。
    /// </summary>
    /// <param name="reporter"><see cref="IProgressReporter"/>。</param>
    /// <returns>完了を表す <see cref="ValueTask"/>。</returns>
    private async ValueTask CreateDummyFileAsync(IProgressReporter reporter)
    {
        var path = this.textBoxOfOutputPath.Text ?? string.Empty;
        var outputSizeText = $"{this.numericUpDownOfOutputSize.Value}{this.comboBoxOfOutputSize.SelectedItem}";
        var bufferSizeText = $"{this.numericUpDownOfBufferSize.Value}{this.comboBoxOfBufferSize.SelectedItem}";
        var fillWithZero = this.radioButtonOfZeros.Checked;
        var oldValue = 0;
        await DummyFile.CreateAsync(
                path,
                outputSizeText,
                bufferSizeText,
                fillWithZero,
                (current, total) =>
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
    }

    /// <summary>
    /// テキストボックスのテキストが変更されたされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void TextBoxOfOutputPathOnTextChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

    /// <summary>
    /// コンボボックスの選択が変更されたされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void ComboBoxOfOutputSizeOnSelectedIndexChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

    /// <summary>
    /// コンボボックスの選択が変更されたされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void ComboBoxOfBufferSizeOnSelectedIndexChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

    /// <summary>
    /// ラジオボタンの選択が変更されたされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void RadioButtonOfRandomOnCheckedChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

    /// <summary>
    /// ラジオボタンの選択が変更されたされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void RadioButtonOfZerosOnCheckedChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

    /// <summary>
    /// […] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
    /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
    private void ButtonToBrowseOutputPathOnClick(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        dialog.Filter = @"text(*.txt)|*.txt";
        dialog.Title = @"ダミーファイルを選択してください。";
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        dialog.CreatePrompt = false;
        dialog.OverwritePrompt = true;
        dialog.RestoreDirectory = true;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            this.textBoxOfOutputPath.Text = dialog.FileName;
        }
    }

    /// <summary>
    /// [ダミーファイル生成] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
    /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
    private async void ButtonToCreateOnClick(object? sender, EventArgs e)
    {
        this.tableLayoutPanel1.Enabled = false;
        var reporter = this._progressReporter;
        try
        {
            reporter.ReportStarting("書き込み中です。しばらくお待ちください。");
            await this.CreateDummyFileAsync(reporter).ConfigureAwait(true);
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
        finally
        {
            reporter.ReportClear();
            this.tableLayoutPanel1.Enabled = true;
        }
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
        this.toolStripProgressBar1.Visible = nowPercent > 0d;
    }
}
