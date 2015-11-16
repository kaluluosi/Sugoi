using Microsoft.VisualStudio.TestTools.UnitTesting;
using SugoiTestFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Tests {
    [TestClass()]
    public class SugoiTests {
        private static Sugoi sugoi = new Sugoi();


        [TestMethod()]
        public void FindTest() {
            var p = sugoi.Find("computer.bmp");
            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void ExistsTest() {
            bool r = sugoi.Exists("computer.bmp");
            Assert.IsTrue(r, "返回值理应为true，实际为false");
        }

        [TestMethod]
        public void DragDropTest() {
            sugoi.DragDrop("xiang.bmp","flod.bmp");
            Assert.IsFalse(sugoi.Exists("xiang.bmp"));
        }
    }
}