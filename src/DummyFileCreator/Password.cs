// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Password.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Cryptography;

namespace DummyFileCreator;

/// <summary>
/// パスワードの生成を提供します。
/// </summary>
internal static class Password
{
    /// <summary>句読点を表します。</summary>
    private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

    /// <summary>
    /// パスワードを生成します。
    /// </summary>
    /// <param name="length">生成するパスワードの 1 以上 128 以下の文字数。</param>
    /// <param name="numberOfNonAlphanumericCharacters">英数字以外の文字数。</param>
    /// <returns>生成されたランダムなパスワード。</returns>
    /// <remarks>
    /// NOTE: [asp\.net \- Alternative to System\.Web\.Security\.Membership\.GeneratePassword in aspnetcore \(netcoreapp1\.0\)
    /// \- Stack
    /// Overflow](https://stackoverflow.com/questions/38995379/alternative-to-system-web-security-membership-generatepassword-in-aspnetcore-ne/38997554#38997554)
    /// </remarks>
    /// <example>
    /// <para><see cref="Password.Generate" /> メソッドの使用例を以下に示します。</para>
    /// <![CDATA[
    /// var password = Password.Generate(32, 12);
    /// // output: Fy.:v@R6/eD#S_3T.}z-yF9h^nxR{Ab_
    /// ]]>
    /// </example>
    public static string Generate(int length, int numberOfNonAlphanumericCharacters)
    {
        if (length is < 1 or > 128)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfNonAlphanumericCharacters));
        }

        var byteBuffer = new byte[length];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(byteBuffer);

        var count = 0;

        // var characterBuffer = new char[length];
        Span<char> characterBuffer = new char[length];

        for (var index = 0; index < length; index++)
        {
            var i = byteBuffer[index] % 87;
            switch (i)
            {
                case < 10:
                    characterBuffer[index] = (char)('0' + i);
                    break;
                case < 36:
                    characterBuffer[index] = (char)('A' + i - 10);
                    break;
                case < 62:
                    characterBuffer[index] = (char)('a' + i - 36);
                    break;
                default:
                    characterBuffer[index] = Password.Punctuations[i - 62];
                    count++;
                    break;
            }
        }

        if (count >= numberOfNonAlphanumericCharacters)
        {
            return new string(characterBuffer);
        }

        for (var j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
        {
            int k;
            while (true)
            {
                k = RandomNumberGenerator.GetInt32(0, length);
                if (char.IsLetterOrDigit(characterBuffer[k]))
                {
                    break;
                }
            }

            characterBuffer[k] = Password.Punctuations[RandomNumberGenerator.GetInt32(0, Password.Punctuations.Length)];
        }

        return new string(characterBuffer);
    }
}
