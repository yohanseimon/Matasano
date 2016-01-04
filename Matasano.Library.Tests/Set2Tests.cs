using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matasano.Library.Tests
{
    [TestClass]
    public class Set2Tests
    {
        [TestMethod, TestCategory("Set 2")]
        public void Challenge09()
        {
            Set2 set = new Set2();

            string pkcs7PaddedString = set.PKCS7Padding("YELLOW SUBMARINE", 20);

            Assert.AreEqual("YELLOW SUBMARINE04040404", pkcs7PaddedString);
        }
    }
}