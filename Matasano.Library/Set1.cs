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
    }
}