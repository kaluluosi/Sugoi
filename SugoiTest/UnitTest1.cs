using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SugoiTestFramwork;


namespace SugoiTest
{
    [TestClass]
    public class UnitTest1
    {
        private static Sugoi sugoi = new Sugoi();

        [TestMethod]
        public void Case_OpenComputer() {
            sugoi.DoubleClick("computer.bmp");
            Assert.IsTrue(sugoi.Exists("flg.bmp"),"flg no found");
        }
    }
}
