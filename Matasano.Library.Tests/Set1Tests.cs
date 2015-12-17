using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matasano.Library.Tests
{
    [TestClass]
    public class Set1Tests
    {
        [TestMethod, TestCategory("Set 1")]
        public void Challenge01()
        {
            Set1 set = new Set1();

            string base64String = set.HexToBase64("49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d");

            Assert.AreEqual("SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t", base64String, false);
        }

        [TestMethod, TestCategory("Set 1")]
        public void Challenge02()
        {
            Set1 set = new Set1();

            string fixedXorString = set.FixedXor("1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965");

            Assert.AreEqual("746865206b696420646f6e277420706c6179", fixedXorString, false);
        }

        [TestMethod, TestCategory("Set 1")]
        public void Challenge03()
        {
            Set1 set = new Set1();

            string xorCipherString = set.XorCipher("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736");

            Assert.AreEqual("Cooking MC's like a pound of bacon", xorCipherString, false);
        }

        [TestMethod, TestCategory("Set 1")]
        public void Challenge04()
        {
            Set1 set = new Set1();

            string xorCipherString = set.XorCipher(File.ReadLines("04.txt").Cast<string>().ToArray());

            Assert.AreEqual("Now that the party is jumping\n", xorCipherString, false);
        }

        [TestMethod, TestCategory("Set 1")]
        public void Challenge05()
        {
            Set1 set = new Set1();

            string encryptedString = set.RepeatingKeyXor("Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal", "ICE");

            Assert.AreEqual("0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f", encryptedString, false);
        }

        [TestMethod, TestCategory("Set 1")]
        public void Challenge06()
        {
            Set1 set = new Set1();

            string decryptedString = set.BreakRepeatingKeyXor(File.ReadAllText("06.txt"));

            Assert.AreEqual("Terminator X: Bring the noise", decryptedString);
        }
    }
}
