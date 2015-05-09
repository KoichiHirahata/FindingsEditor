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
    public class EditFindingsTests
    {
        [TestMethod()]
        public void shouldFillOperatorWithUserTest()
        {
            //exam_status
            //0;"Blank"
            Assert.IsTrue(EditFindings.shouldFillOperatorWithUser(true, 0));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(false, 0));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(true, 1));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(false, 1));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(true, 2));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(false, 2));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(true, 3));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(false, 3));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(true, 9));
            Assert.IsFalse(EditFindings.shouldFillOperatorWithUser(false, 9));
        }
    }
}
