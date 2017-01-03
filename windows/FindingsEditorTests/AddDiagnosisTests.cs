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
    public class AddDiagnosisTests
    {
        [TestMethod()]
        public void isValidStringOfTheRangeOfDiagnosesTest()
        {
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses(null));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses(""));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses("  "));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses("test"));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses("(no>=1000"));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses("no>=1000)AND(no<=2000)"));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses(" (no>=1000"));
            Assert.IsFalse(AddDiagnosis.isValidStringOfTheRangeOfDiagnoses("(no>=)AND(no<=2000)"));
        }

        [TestMethod()]
        public void getStartOfTheRangeTest()
        {
            Assert.AreEqual(1000,AddDiagnosis.getStartOfTheRange("(no>=1000)AND(no<=2000)"));
        }

        [TestMethod()]
        public void getEndOfTheRangeTest()
        {
            Assert.AreEqual(2000, AddDiagnosis.getEndOfTheRange("(no>=1000)AND(no<=2000)"));
        }
    }
}
