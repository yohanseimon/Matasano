using Matasano.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matasano.Library.Tests
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod, TestCategory("Utilities")]
        public void GetHammingDistance()
        {
            Assert.AreEqual(37, new StringUtilities().GetHammingDistance("this is a test", "wokka wokka!!!"));
        }
    }
}
