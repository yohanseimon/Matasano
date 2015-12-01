using System;
using System.Linq;

namespace Matasano.Utilities
{
    public class HexadecimalUtilities
    {
        public byte[] HexadecimalStringToByteArray(string hexadecimalString)
        {
            return Enumerable.Range(0, hexadecimalString.Length).Where(h => h % 2 == 0).Select(h => Convert.ToByte(hexadecimalString.Substring(h, 2), 16)).ToArray();
        }
    }
}