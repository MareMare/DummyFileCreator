// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyFileWriter.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator;

/// <summary>
/// <see cref="FileStream" /> と <see cref="StreamWriter" /> を内包するクラスを表します。
/// </summary>
internal class DummyFileWriter : IDisposable, IAsyncDisposable
{
    /// <summary><see cref="FileStream" /> を表します。</summary>
    private FileStream? _stream;

    /// <summary><see cref="StreamWriter" /> を表します。</summary>
    private StreamWriter? _writer;

    /// <summary><see cref="IDisposable.Dispose" /> メソッドが呼び出されたかをスレッドセーフで管理する値を表します。</summary>
    private long _disposableState;

    /// <summary>
    /// <see cref="DummyFileWriter" /> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="filepath">ファイルのパス。</param>
    /// <param name="bufferSize">バッファサイズ。</param>
    public DummyFileWriter(string filepath, int bufferSize)
    {
        this._stream = File.Open(filepath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        this._writer = new StreamWriter(this._stream, bufferSize: bufferSize);
    }

    /// <summary>
    /// <see cref="DummyFileWriter" /> クラスのインスタンスが GC に回収される時に呼び出されます。
    /// </summary>
    ~DummyFileWriter()
    {
        this.Dispose(false);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await this.DisposeAsyncCore().ConfigureAwait(false);
        this.Dispose(false);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 非同期操作として、0を書き込みます。
    /// </summary>
    /// <param name="bytesToWrite">書き込むバイト数。</param>
    /// <returns>書き込んだバイトサイズ。</returns>
    public async Task<long> WriteZeroValue(long bytesToWrite)
    {
        if (this._writer is null)
        {
            throw new InvalidOperationException();
        }

        ReadOnlyMemory<char> dataToFill = new char[bytesToWrite];
        await this._writer.WriteAsync(dataToFill).ConfigureAwait(false);
        return dataToFill.Length;
    }

    /// <summary>
    /// 非同期操作として、ランダムな文字列を書き込みます。
    /// </summary>
    /// <param name="bytesToWrite">書き込むバイト数。</param>
    /// <returns>書き込んだバイトサイズ。</returns>
    public async Task<long> WriteRandomText(long bytesToWrite)
    {
        if (this._writer is null)
        {
            throw new InvalidOperationException();
        }

        long totalLength = 0;
        const int maxChunkSize = 128; // Password.Generate の制約に合わせた最大チャンクサイズです。
        while (totalLength < bytesToWrite)
        {
            var remainingLength = bytesToWrite - totalLength;
            var currentChunkSize = (int)Math.Min(maxChunkSize, remainingLength);
            var dataToWrite = Password.Generate(currentChunkSize, currentChunkSize / 4).AsMemory();
            await this._writer.WriteAsync(dataToWrite).ConfigureAwait(false);
            totalLength += dataToWrite.Length;
        }

        return totalLength;
    }

    /// <summary>
    /// <see cref="DummyFileWriter" /> クラスのインスタンスによって使用されているアンマネージリソースを解放し、オプションでマネージリソースも解放します。
    /// </summary>
    /// <param name="disposing">
    /// マネージリソースとアンマネージリソースの両方を解放する場合は <see langword="true" />。アンマネージリソースだけを解放する場合は
    /// <see langword="false" />。
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (Interlocked.CompareExchange(ref this._disposableState, 1L, 0L) == 0L)
        {
            if (disposing)
            {
                this._writer?.Dispose();
                this._writer = null;

                this._stream?.Dispose();
                this._stream = null;
            }
        }
    }

    /// <summary>
    /// 非同期操作として、<see cref="DummyFileWriter" /> クラスのインスタンスによって使用されているアンマネージリソースを解放し、オプションでマネージリソースも解放します。
    /// </summary>
    /// <returns>完了を表す <see cref="ValueTask" />。</returns>
    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (this._writer is not null)
        {
            await this._writer.DisposeAsync().ConfigureAwait(false);
            this._writer = null;
        }

        if (this._stream is not null)
        {
            await this._stream.DisposeAsync().ConfigureAwait(false);
            this._stream = null;
        }
    }
}
