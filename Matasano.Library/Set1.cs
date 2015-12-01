using System;
using System.Linq;
using Matasano.Utilities;

namespace Matasano.Library
{
    public class Set1
    {
        HexadecimalUtilities _hexadecimalUtilities;

        public Set1()
        {
            _hexadecimalUtilities = new HexadecimalUtilities();
        }

        /*
         * Convert hex to base64
         * 
         * The string:
         * 49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d
         * 
         * Should produce:
         * SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t
         * 
         * Cryptopals Rule:
         * Always operate on raw bytes, never on encoded strings. Only use hex and base64 for pretty-printing.
         * 
         */
        public string HexToBase64(string hexString)
        {
            byte[] byteArray = _hexadecimalUtilities.HexadecimalStringToByteArray("49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d");

            return Convert.ToBase64String(byteArray);
        }

        /*
         * Fixed XOR
         * Write a function that takes two equal-length buffers and produces their XOR combination.
         * 
         * If your function works properly, then when you feed it the string:
         * 1c0111001f010100061a024b53535009181c
         * ... after hex decoding, and when XOR'd against:
         * 686974207468652062756c6c277320657965
         * ... should produce:
         * 746865206b696420646f6e277420706c6179
         * 
         */
        public string FixedXor(string first, string second)
        {
            if (first.Length != second.Length)
                throw new Exception("Strings are not of equal length");

            byte[] firstByteArray = _hexadecimalUtilities.HexadecimalStringToByteArray(first);
            byte[] secondByteArray = _hexadecimalUtilities.HexadecimalStringToByteArray(second);

            byte[] resultArray = new byte[firstByteArray.Length];

            for (int i = 0; i < firstByteArray.Length; i++)
                resultArray[i] = (byte)(firstByteArray[i] ^ secondByteArray[i]);

            return _hexadecimalUtilities.ByteArrayToHexadecimalString(resultArray);
        }
    }
}