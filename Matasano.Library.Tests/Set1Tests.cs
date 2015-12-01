using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matasano.Library.Tests
{
    [TestClass]
    public class Set1Tests
    {
        [TestMethod]
        public void Challenge01()
        {
            Set1 set = new Set1();

            string base64String = set.HexToBase64("49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d");

            Assert.AreEqual("SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t", base64String, false);
        }

        [TestMethod]
        public void Challenge02()
        {
            Set1 set = new Set1();

            string fixedXorString = set.FixedXor("1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965");

            Assert.AreEqual("746865206b696420646f6e277420706c6179", fixedXorString, false);
        }
    }
}