using System.Collections.Generic;

namespace Matasano.Utilities
{
    public class StringScorer
    {
        private Dictionary<char, double> letterDictionary;

        public StringScorer()
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
    }
}