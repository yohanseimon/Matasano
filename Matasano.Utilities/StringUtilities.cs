using System;
using System.Linq;
using System.Collections.Generic;

namespace Matasano.Utilities
{
    public class StringUtilities
    {
        private Dictionary<char, double> letterDictionary;

        public StringUtilities()
        {
            // Source: https://en.wikipedia.org/wiki/Letter_frequency
            double[] letterFrequency = new[]
            {
                8.167, 1.492, 2.782, 4.253, 12.702, 2.228, 2.015, 6.094, 6.966, 0.153, 0.772, 4.025, 2.406, 6.749, 7.507, 1.929, 0.095, 5.987, 6.327, 9.056, 2.758, 0.978, 2.361, 0.150, 1.974, 0.074
            };

            letterDictionary = new Dictionary<char, double>(26);

            for (int i = 0; i < 26; i++)
                letterDictionary.Add((char)(65 + i), letterFrequency[i]);
        }

        public double ScoreStringByLetter(string stringToScore)
        {
            double score = 0;

            foreach (char c in stringToScore.ToUpper())
            {
                if (letterDictionary.ContainsKey(c))
                    score += letterDictionary[c];
                else if (c == ' ')
                    score += 20;
            }

            return score;
        }

        public int GetHammingDistance(string source, string target)
        {
            byte[] sourceByteArray = GetByteArray(source);
            byte[] targetByteArray = GetByteArray(target);

            return GetHammingDistance(sourceByteArray, targetByteArray);
        }

        public int GetHammingDistance(byte[] sourceByteArray, byte[] targetByteArray)
        {
            return sourceByteArray.Zip(targetByteArray, (first, second) => NumberOfBitSetsInNumber(first ^ second)).Sum();
        }

        // Source: https://yesteapea.wordpress.com/2013/03/03/counting-the-number-of-set-bits-in-an-integer
        private int NumberOfBitSetsInNumber(int number)
        {
            number = number - ((number >> 1) & 0x55555555);
            number = (number & 0x33333333) + ((number >> 2) & 0x33333333);
            return (((number + (number >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }

        public byte[] GetByteArray(string stringToConvert)
        {
            byte[] byteArray = new byte[stringToConvert.Length * sizeof(char)];
            
            System.Buffer.BlockCopy(stringToConvert.ToCharArray(), 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }
    }
}