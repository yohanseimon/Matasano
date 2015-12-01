using System;
using System.Linq;
using System.Text;

namespace Matasano.Utilities
{
    public class HexadecimalUtilities
    {
        public byte[] HexadecimalStringToByteArray(string hexadecimalString)
        {
            return Enumerable.Range(0, hexadecimalString.Length).Where(h => h % 2 == 0).Select(h => Convert.ToByte(hexadecimalString.Substring(h, 2), 16)).ToArray();
        }

        public string ByteArrayToHexadecimalString(byte[] byteArray)
        {
            StringBuilder stringBuilder = new StringBuilder(byteArray.Length);

            foreach (byte b in byteArray)
                stringBuilder.Append(b.ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}