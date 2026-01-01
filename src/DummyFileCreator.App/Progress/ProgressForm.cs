// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressForm.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using Microsoft.Extensions.Logging;

namespace DummyFileCreator.App.Progress;

/// <summary>
/// 進捗画面のユーザーインターフェイスを構成するウィンドウを表します。
/// </summary>
internal partial class ProgressForm : Form
{
    /// <summary><see cref="ILogger{T}" /> を表します。</summary>
    private readonly ILogger<ProgressForm> _logger;

    /// <summary>進行状況の報告者を表します。</summary>
    private readonly IProgressReporter _progressReporter;

    /// <summary>
    /// <see cref="ProgressForm" /> クラスの新しいインスタンスを生成します。
    /// </summary>
    /// <param name="logger"><see cref="ILogger{T}" />。</param>
    public ProgressForm(ILogger<ProgressForm> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        this._logger = logger;
        this._progressReporter = new ProgressReporter();
        this._progressReporter.ProgressInfoChanged += this.OnProgressReporterOnProgressChanged;
        this.InitializeComponent();
    }

    /// <summary>
    /// タイトルを取得または設定します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="string" /> 型。
    /// <para>タイトル。既定値は null です。</para>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? Title
    {
        get => this.Text;
        set => this.Text = value;
    }

    /// <summary>
    /// メッセージを取得または設定します。
    /// </summary>
    /// <value>
    /// 値を表す <see cref="string" /> 型。
    /// <para>メッセージ。既定値は null です。</para>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? Message
    {
        get => this.labelMessage.Text;
        set => this.labelMessage.Text = value;
    }

    /// <summary>
    /// 非同期操作として、モーダル表示する範囲を使用します。
    /// </summary>
    /// <param name="owner">フォームを所有するトップレベルウィンドウ。</param>
    /// <returns>進捗表示する範囲。</returns>
    public IProgressScope UseProgressFormScope(IWin32Window owner) =>
        new ProgressFormScope(this, this.ShowDialogAsync(owner));

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this._progressReporter.ProgressInfoChanged -= this.OnProgressReporterOnProgressChanged;
            if (this.components != null)
            {
                this.components.Dispose();
            }
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnLoad(EventArgs e)
    {
        this._logger.LogTrace($"{nameof(ProgressForm)}.{nameof(this.OnLoad)}");
        base.OnLoad(e);
    }

    /// <inheritdoc />
    protected override void OnShown(EventArgs e)
    {
        this._logger.LogTrace($"{nameof(ProgressForm)}.{nameof(this.OnShown)}");
        base.OnShown(e);
    }

    /// <summary>
    /// 進捗情報で表示更新します。
    /// </summary>
    /// <param name="progressInfo">進捗情報。</param>
    private void UpdateBy(ProgressInfo progressInfo) => this.Message = progressInfo.Message;

    /// <summary>
    /// フォームが読み込まれたときに発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void ProgressForm_Load(object sender, EventArgs e)
    {
        this.StartPosition = FormStartPosition.Manual;
        this.Location = new Point(
            this.Owner.Location.X + ((this.Owner.Width - this.Width) / 2),
            this.Owner.Location.Y + ((this.Owner.Height - this.Height) / 2));
    }

    /// <summary>
    /// 進捗が変更になったときに発生するイベントのイベントハンドラです。
    /// </summary>
    /// <param name="sender">イベントソース。</param>
    /// <param name="e">イベントデータ。</param>
    private void OnProgressReporterOnProgressChanged(object? sender, ProgressInfoEventArgs e) => this.UpdateBy(e.Info);

    /// <summary>
    /// 進捗表示する範囲を表します。
    /// </summary>
    private class ProgressFormScope : IProgressScope
    {
        /// <summary><see cref="ProgressForm" /> を表します。</summary>
        private readonly ProgressForm _progressForm;

        /// <summary>表示完了を表すタスクを表します。</summary>
        private readonly Task<DialogResult> _progressFormTask;

        /// <summary>
        /// <see cref="ProgressFormScope" /> クラスの新しいインスタンスを生成します。
        /// </summary>
        /// <param name="progressForm"><see cref="ProgressForm" />。</param>
        /// <param name="progressFormTask">表示完了を表すタスク。</param>
        public ProgressFormScope(ProgressForm progressForm, Task<DialogResult> progressFormTask)
        {
            ArgumentNullException.ThrowIfNull(progressForm);
            ArgumentNullException.ThrowIfNull(progressFormTask);

            progressForm._logger.LogTrace($"{nameof(ProgressFormScope)}.ctor");

            this._progressForm = progressForm;
            this._progressFormTask = progressFormTask;
            this.Reporter = progressForm._progressReporter;
        }

        /// <inheritdoc />
        public IProgressReporter Reporter { get; }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            this._progressForm._logger.LogTrace($"{nameof(ProgressFormScope)}.{nameof(this.DisposeAsync)}...start");

            // NOTE: https://stackoverflow.com/a/33411037
            this._progressForm.Close();
            await this._progressFormTask.ConfigureAwait(true);

            this._progressForm._logger.LogTrace($"{nameof(ProgressFormScope)}.{nameof(this.DisposeAsync)}...finish");
        }
    }
}
