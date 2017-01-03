using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindingsEdior;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace FindingsEdior.Tests
{
    [TestClass()]
    public class uckyFunctionsTests
    {
        [TestMethod()]
        public void dateTo8charTest()
        {
            Assert.AreEqual("20140206", uckyFunctions.dateTo8char("2014/02/06", "ja"));
            Assert.AreEqual("20140206", uckyFunctions.dateTo8char("2014/2/6", "ja"));
            Assert.AreEqual("20140216", uckyFunctions.dateTo8char("2014/2/16", "ja"));
            Assert.AreEqual("20140206", uckyFunctions.dateTo8char("02/06/2014", "en"));
            Assert.AreEqual("20140216", uckyFunctions.dateTo8char("2/16/2014", "en"));
            Assert.AreEqual("20141206", uckyFunctions.dateTo8char("12/6/2014", "en"));
        }
    }
}
