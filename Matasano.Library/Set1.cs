using System;
using System.Linq;
using System.Text;
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
         * 
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

        /*
         * Single-byte XOR cipher
         * 
         * The hex encoded string:
         * 1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736
         * ... has been XOR'd against a single character. Find the key, decrypt the message.
         * 
         * You can do this by hand. But don't: write code to do it for you.
         * How? Devise some method for "scoring" a piece of English plaintext. Character frequency is a good metric. Evaluate each output and choose the one with the best score.
         * 
         * Achievement Unlocked
         * You now have our permission to make "ETAOIN SHRDLU" jokes on Twitter.
         * 
         */
        public string XorCipher(string hexadecimalString)
        {
            byte[] decipheredByteArray;
            string decipheredByteString;

            double score = 0;
            double highestScore = 0;
            string highestScoringString = String.Empty;

            StringScorer stringScorer = new StringScorer();

            for (int i = 0; i < 256; i++)
            {
                decipheredByteArray = _hexadecimalUtilities.XorByteArrayByKey(_hexadecimalUtilities.HexadecimalStringToByteArray(hexadecimalString), (byte)i);
                decipheredByteString = Encoding.ASCII.GetString(decipheredByteArray);

                score = stringScorer.ScoreStringByLetter(decipheredByteString);

                if (score > highestScore)
                {
                    highestScore = score;
                    highestScoringString = decipheredByteString;
                }
            }

            return highestScoringString;
        }

        /*
         * Detect single-character XOR
         * One of the 60-character strings in this file has been encrypted by single-character XOR.
         * Find it.
         * 
         * (Your code from #3 should help.)
         * 
         */
        public string XorCipher(string[] hexadecimalStringArray)
        {
            double score = 0;            
            double highestScore = 0;

            string decipheredString = String.Empty;
            string highestScoringString = String.Empty;

            StringScorer stringScorer = new StringScorer();

            foreach (string hexadecimalString in hexadecimalStringArray)
            {
                decipheredString = XorCipher(hexadecimalString);
                score = stringScorer.ScoreStringByLetter(decipheredString);

                if (score > highestScore)
                {
                    highestScore = score;
                    highestScoringString = decipheredString;
                }
            }

            return highestScoringString;
        }

        /*
         * Implement repeating-key XOR
         * Here is the opening stanza of an important work of the English language:
         * 
         * Burning 'em, if you ain't quick and nimble
         * I go crazy when I hear a cymbal
         * 
         * Encrypt it, under the key "ICE", using repeating-key XOR.
         * 
         * In repeating-key XOR, you'll sequentially apply each byte of the key; the first byte of plaintext will be XOR'd against I, the next C, the next E, then I again for the 4th byte, and so on.
         * It should come out to:
         * 
         * 0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272
         * a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f
         * 
         * Encrypt a bunch of stuff using your repeating-key XOR function. Encrypt your mail. Encrypt your password file. Your .sig file. Get a feel for it. I promise, we aren't wasting your time with this.
         * 
         */
        public string RepeatingKeyXor(string stringToEncrypt, string key)
        {
            var bytes = Encoding.ASCII.GetBytes(stringToEncrypt);
            var keyBytes = Encoding.ASCII.GetBytes(key);

            return _hexadecimalUtilities.XorByteArrayByRepeatingKey(bytes, keyBytes);
        }
    }
}