﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyFile.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DummyFileCreator;

/// <summary>
/// ダミーファイルの生成を提供します。
/// </summary>
public static class DummyFile
{
    /// <summary>既定のバッファサイズ (10MB: 10_485_760) の文字列を表します。</summary>
    private const string DefaultBufferSizeText = "10MB";

    /// <summary>
    /// 非同期操作として、ダミーファイルを生成します。
    /// </summary>
    /// <param name="filepathToCreate">ダミーファイルのパス。</param>
    /// <param name="sizeTextToCreate">ダミーファイルのサイズを表す文字列。</param>
    /// <param name="bufferSizeText">バッファサイズ。既定値は 10 MB(10_485_760)。</param>
    /// <param name="fillWithZeros">0x00 で埋める場合は <see langword="true" />。それ以外は <see langword="false" />。</param>
    /// <param name="progress">進捗を処理するメソッドのデリゲート。</param>
    /// <returns>完了を表す <see cref="Task" />。</returns>
    public static Task CreateAsync(
        string filepathToCreate,
        string sizeTextToCreate,
        string bufferSizeText = DummyFile.DefaultBufferSizeText,
        bool fillWithZeros = false,
        Action<long, long>? progress = null)
    {
        ArgumentNullException.ThrowIfNull(sizeTextToCreate);
        ArgumentNullException.ThrowIfNull(bufferSizeText);
        if (!sizeTextToCreate.TryParseToByteSize(out var byteSizeToCreate))
        {
            throw new ArgumentOutOfRangeException(nameof(sizeTextToCreate));
        }

        if (!bufferSizeText.TryParseToByteSize(out var bufferSize))
        {
            throw new ArgumentOutOfRangeException(nameof(bufferSizeText));
        }

        return DummyFile.CreateAsync(filepathToCreate, byteSizeToCreate, (int)bufferSize, fillWithZeros, progress);
    }

    /// <summary>
    /// 非同期操作として、ダミーファイルを生成します。
    /// </summary>
    /// <param name="filepathToCreate">ダミーファイルのパス。</param>
    /// <param name="sizeTextToCreate">ダミーファイルのサイズを表す文字列。</param>
    /// <param name="bufferSize">バッファサイズ。既定値は 10 MB(10_485_760)。</param>
    /// <param name="fillWithZeros">0x00 で埋める場合は <see langword="true" />。それ以外は <see langword="false" />。</param>
    /// <param name="progress">進捗を処理するメソッドのデリゲート。</param>
    /// <returns>完了を表す <see cref="Task" />。</returns>
    public static Task CreateAsync(
        string filepathToCreate,
        string sizeTextToCreate,
        int bufferSize = 10_485_760,
        bool fillWithZeros = false,
        Action<long, long>? progress = null)
    {
        ArgumentNullException.ThrowIfNull(sizeTextToCreate);
        if (!sizeTextToCreate.TryParseToByteSize(out var byteSizeToCreate))
        {
            throw new ArgumentOutOfRangeException(nameof(sizeTextToCreate));
        }

        return DummyFile.CreateAsync(filepathToCreate, byteSizeToCreate, bufferSize, fillWithZeros, progress);
    }

    /// <summary>
    /// 非同期操作として、ダミーファイルを生成します。
    /// </summary>
    /// <param name="filepathToCreate">ダミーファイルのパス。</param>
    /// <param name="byteSizeToCreate">ダミーファイルのバイトサイズ。</param>
    /// <param name="bufferSize">バッファサイズ。既定値は 10 MB(10_485_760)。</param>
    /// <param name="fillWithZeros">0x00 で埋める場合は <see langword="true" />。それ以外は <see langword="false" />。</param>
    /// <param name="progress">進捗を処理するメソッドのデリゲート。</param>
    /// <returns>完了を表す <see cref="Task" />。</returns>
    public static async Task CreateAsync(
        string filepathToCreate,
        long byteSizeToCreate,
        int bufferSize = 10_485_760,
        bool fillWithZeros = false,
        Action<long, long>? progress = null)
    {
        ArgumentNullException.ThrowIfNull(filepathToCreate);

        DummyFileWriter? fileWriter = null;
        try
        {
            fileWriter = new DummyFileWriter(filepathToCreate, bufferSize);
            long writtenBytes = 0;
            while (writtenBytes < byteSizeToCreate)
            {
                var remainingBytes = byteSizeToCreate - writtenBytes;
                var bytesToWrite = Math.Min(bufferSize, remainingBytes);
                if (fillWithZeros)
                {
                    writtenBytes += await fileWriter.WriteZeroValue(bytesToWrite).ConfigureAwait(false);
                }
                else
                {
                    writtenBytes += await fileWriter.WriteRandomText(bytesToWrite).ConfigureAwait(false);
                }

                progress?.Invoke(writtenBytes, byteSizeToCreate);
            }
        }
        finally
        {
            if (fileWriter is not null)
            {
                await fileWriter.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}
