using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Reloadly.Core.Internal.Utility
{
    public static class Base64
    {
        public static string Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static bool TryDecode(string encoded, [NotNullWhen(true)] out string? plainText)
        {
            var buffer = new Span<byte>(new byte[encoded.Length]);

            if (Convert.TryFromBase64String(encoded, buffer, out var _))
            {
                plainText = Encoding.UTF8.GetString(buffer.ToArray());
                plainText = plainText.TrimEnd('\0');
                return true;
            }

            plainText = null;
            return false;
        }
    }
}
