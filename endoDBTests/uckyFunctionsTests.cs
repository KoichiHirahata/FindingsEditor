using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using endoDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace endoDB.Tests
{
    [TestClass()]
    public class uckyFunctionsTests
    {
        [TestMethod()]
        public void dateTo8charTest()
        {
            Assert.AreEqual("20140206", uckyFunctions.dateTo8char("2014/02/06", "ja"));
            Assert.AreEqual("20140206", uckyFunctions.dateTo8char("02/06/2014", "en"));
        }
    }
}
