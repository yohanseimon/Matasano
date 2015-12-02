using System;
using System.Collections.Generic;
using System.Text;

namespace Matasano.Utilities
{
    public class HexadecimalUtilities
    {
        public byte[] HexadecimalStringToByteArray(string hexadecimalString)
        {
            List<byte> resultByteArray = new List<byte>();

            for (int i = 0; i < hexadecimalString.Length; i += 2)
            {
                resultByteArray.Add(Convert.ToByte(hexadecimalString.Substring(i, 2), 16));
            }

            return resultByteArray.ToArray();
        }

        public string ByteArrayToHexadecimalString(byte[] byteArray)
        {
            StringBuilder stringBuilder = new StringBuilder(byteArray.Length);

            foreach (byte b in byteArray)
                stringBuilder.Append(b.ToString("x2"));

            return stringBuilder.ToString();
        }

        public byte[] XorByteArrayByKey(byte[] byteArray, byte key)
        {
            List<byte> resultByteArray = new List<byte>(byteArray.Length);

            foreach (byte b in byteArray)
            {
                resultByteArray.Add((byte)(b ^ key));
            }

            return resultByteArray.ToArray();
        }

        public string XorByteArrayByRepeatingKey(byte[] byteArray, byte[] key)
        {
            int i = 0;
            List<byte> resultByteArray = new List<byte>(byteArray.Length);

            foreach (byte b in byteArray)
            {                
                resultByteArray.Add((byte)(b ^ key[i++]));

                if (i == key.Length)
                    i = 0;
            }

            return ByteArrayToHexadecimalString(resultByteArray.ToArray());
        }
    }
}