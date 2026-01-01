// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ByteSizeExtensions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Text.RegularExpressions;

namespace DummyFileCreator;

/// <summary>
/// バイトサイズに関する拡張メソッドを提供します。
/// </summary>
internal static class ByteSizeExtensions
{
    /// <summary>バイトサイズの単位を表します。</summary>
    private static readonly string[] SizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB" };

    /// <summary>バイトサイズの文字列を分解する正規表現を表します。</summary>
    private static readonly Regex RegexPattern = new (
        "(?<value>[\\d,.]{1,})[\\s]*(?<unit>(B|KB|MB|GB|TB|PB))",
        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    /// <summary>
    /// 指定されたバイトサイズの文字列からバイト単位の数値への変換を試みます。
    /// </summary>
    /// <param name="text">バイトサイズの文字列。</param>
    /// <param name="value">バイト単位の数値。</param>
    /// <returns>変換に成功した場合は <see langword="true" />。それ以外は<see langword="true" />。</returns>
    public static bool TryParseToByteSize(this string text, out long value)
    {
        value = 0L;
        var match = ByteSizeExtensions.RegexPattern.Match(text);
        if (!match.Success)
        {
            return false;
        }

        var numberPartText = match.Groups["value"].Value;
        var unitPartText = match.Groups["unit"].Value.ToUpperInvariant();
        var unitIndex = ByteSizeExtensions.SizeSuffixes.ToList().IndexOf(unitPartText);
        if (!double.TryParse(numberPartText, NumberStyles.Number, CultureInfo.InvariantCulture, out var number))
        {
            return false;
        }

        value = (long)(number * Math.Pow(1024, unitIndex));
        return true;
    }

    /// <summary>
    /// 指定されたバイト単位の数値をバイトサイズの文字列へ変換します。
    /// </summary>
    /// <param name="value">バイト単位の数値。</param>
    /// <param name="decimalPlaces">小数点以下桁数。</param>
    /// <returns>バイトサイズの文字列。</returns>
    public static string ToPrettySize(this long value, int decimalPlaces = 1)
    {
        if (decimalPlaces < 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(decimalPlaces);
        }

        switch (value)
        {
            case < 0:
                return "-" + (-value).ToPrettySize(decimalPlaces);
            case 0:
                return string.Format(CultureInfo.InvariantCulture, $"{{0:n{decimalPlaces}}} {{1}}", 0, ByteSizeExtensions.SizeSuffixes[0]);
        }

        // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
        var mag = (int)Math.Log(value, 1024);

        // 1L << (mag * 10) == 2 ^ (10 * mag)
        // [i.e. the number of bytes in the unit corresponding to mag]
        var adjustedSize = (decimal)value / (1L << (mag * 10));

        // make adjustment when the value is large enough that
        // it would round up to 1000 or more
        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }

        return string.Format(CultureInfo.InvariantCulture, $"{{0:n{decimalPlaces}}} {{1}}", adjustedSize, ByteSizeExtensions.SizeSuffixes[mag]);
    }
}
