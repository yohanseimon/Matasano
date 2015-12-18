using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Matasano.Utilities
{
    public class EcbUtilities
    {
        public string Decrypt(string encryptedString, string keyString)
        {
            byte[] encryptedByteArray = Convert.FromBase64String(encryptedString);

            var aesManaged = new AesManaged
            {
                KeySize = 128,                
                BlockSize = 128,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.Zeros,
                IV = Enumerable.Repeat((byte)0, 16).ToArray(),
                Key = Encoding.ASCII.GetBytes(keyString),
            };

            byte[] decryptedByteArray = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV).TransformFinalBlock(encryptedByteArray, 0, encryptedByteArray.Length);

            return Encoding.ASCII.GetString(decryptedByteArray);
        }
    }
}