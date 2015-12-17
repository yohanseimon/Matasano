using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Matasano.Utilities;

namespace Matasano.Library
{
    public class Set1
    {
        StringUtilities _stringUtilities;
        HexadecimalUtilities _hexadecimalUtilities;

        public Set1()
        {
            _stringUtilities = new StringUtilities();
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

            for (int i = 0; i < 256; i++)
            {
                decipheredByteArray = _hexadecimalUtilities.XorByteArrayByKey(_hexadecimalUtilities.HexadecimalStringToByteArray(hexadecimalString), (byte)i);
                decipheredByteString = Encoding.ASCII.GetString(decipheredByteArray);

                score = _stringUtilities.ScoreStringByLetter(decipheredByteString);

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

            StringUtilities stringScorer = new StringUtilities();

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

            return _hexadecimalUtilities.ByteArrayToHexadecimalString(_hexadecimalUtilities.XorByteArrayByRepeatingKey(bytes, keyBytes));
        }

        /*
         * Break repeating-key XOR
         * It is officially on, now.
         * This challenge isn't conceptually hard, but it involves actual error-prone coding. The other challenges in this set are there to bring you up to speed. This one is there to qualify you. If you can do this one, you're probably just fine up to Set 6.
         * 
         * There's a file here. It's been base64'd after being encrypted with repeating-key XOR.
         * Decrypt it.
         * 
         * Here's how:
         * 1. Let KEYSIZE be the guessed length of the key; try values from 2 to (say) 40.
         * 2. Write a function to compute the edit distance/Hamming distance between two strings. The Hamming distance is just the number of differing bits. The distance between:
         * 'this is a test' and 'wokka wokka!!!' is 37. Make sure your code agrees before you proceed.
         * 3. For each KEYSIZE, take the first KEYSIZE worth of bytes, and the second KEYSIZE worth of bytes, and find the edit distance between them. Normalize this result by dividing by KEYSIZE.
         * 4. The KEYSIZE with the smallest normalized edit distance is probably the key. You could proceed perhaps with the smallest 2-3 KEYSIZE values. Or take 4 KEYSIZE blocks instead of 2 and average the distances.
         * 5. Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.
         * 6. Now transpose the blocks: make a block that is the first byte of every block, and a block that is the second byte of every block, and so on.
         * 7. Solve each block as if it was single-character XOR. You already have code to do this.
         * 8. For each block, the single-byte XOR key that produces the best looking histogram is the repeating-key XOR key byte for that block. Put them together and you have the key.
         * 
         * This code is going to turn out to be surprisingly useful later on. Breaking repeating-key XOR ("Vigenere") statistically is obviously an academic exercise, a "Crypto 101" thing. But more people "know how" to break it than can actually break it, and a similar technique breaks something much more important.
         * 
         * No, that's not a mistake.
         * We get more tech support questions for this challenge than any of the other ones. We promise, there aren't any blatant errors in this text. In particular: the "wokka wokka!!!" edit distance really is 37.
         * 
         */
        public string BreakRepeatingKeyXor(string stringToDecrypt)
        {
            var encryptedByteArray = Convert.FromBase64String(stringToDecrypt);

            Dictionary<int, float> hammingDistanceDictionary = new Dictionary<int, float>();

            for (int i = 2; i <= 40; i++)
            {
                hammingDistanceDictionary.Add(i, (float)(_stringUtilities.GetHammingDistance(encryptedByteArray.Take(i).ToArray(), encryptedByteArray.Skip(i).Take(i).ToArray()) +
                    _stringUtilities.GetHammingDistance(encryptedByteArray.Skip(i * 2).Take(i).ToArray(), encryptedByteArray.Skip(i * 3).Take(i).ToArray())) / i);
            }

            foreach (int key in hammingDistanceDictionary.OrderBy(h => h.Value).Select(h => h.Key))
            {                
                int highestScoreKey;
                double score, highestScore;

                List<byte> byteBlock;
                byte[] possibleKey = new byte[key];

                for (int keyIndex = 0; keyIndex < key; keyIndex++)
                {
                    score = 0;
                    highestScore = 0;
                    highestScoreKey = -1;
                    byteBlock = new List<byte>();

                    for (int i = 0; i < encryptedByteArray.Length / key; i++)
                    {
                        byteBlock.Add(encryptedByteArray[i * key + keyIndex]);
                    }

                    for (int possibleKeyChar = 0; possibleKeyChar <= 256; possibleKeyChar++)
                    {
                        byte[] xoredByteArray = _hexadecimalUtilities.XorByteArrayByKey(byteBlock.ToArray(), (byte)possibleKeyChar);

                        if (!xoredByteArray.Any(c => (c < 32 || c > 128) && c != '\r' && c != '\n'))
                        {
                            score = _stringUtilities.ScoreStringByLetter(Encoding.UTF8.GetString(xoredByteArray)) / xoredByteArray.Length;

                            if (score > highestScore)
                            {
                                highestScore = score;
                                highestScoreKey = possibleKeyChar;
                            }
                        }
                    }

                    if (highestScore > 0)
                    {
                        possibleKey[keyIndex] = (byte)highestScoreKey;
                    }
                }

                if (possibleKey.All(k => k != 0))
                {
                    return Encoding.UTF8.GetString(possibleKey);
                }
            }

            return String.Empty;
        }
    }
}