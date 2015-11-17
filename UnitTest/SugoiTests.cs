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

        [ClassInitialize()]
        public static void UnitTestInit(TestContext testContext) {
            sugoi.RunAndBindApp(@"D:\GitHub\Sugoi\TestWindow\bin\Debug\TestWindow.exe");
            //sugoi.RunApp(@"D:\GitHub\Sugoi\TestWindow\bin\Debug\TestWindow.exe");
            sugoi.Wait(500);
        }

        [ClassCleanup()]
        public static void UnitTestCleanup() {
            //sugoi.CloseApp();
        }

        [Ignore]
        [TestMethod()]
        public void FindTest() {
            var p = sugoi.Find("computer.bmp");
            Assert.IsNotNull(p);
        }

        [Ignore]
        [TestMethod()]
        public void ExistsTest() {
            bool r = sugoi.Exists("computer.bmp");
            Assert.IsTrue(r, "返回值理应为true，实际为false");
        }

        [Ignore]
        [TestMethod]
        public void DragDropTest() {
            sugoi.DragDrop("xiang.bmp","flod.bmp");
            Assert.IsFalse(sugoi.Exists("xiang.bmp"));
        }
        
        [TestMethod]
        public void FindAllTest() {
            var matchs = sugoi.FindAll(new ImgPattern("checkbox.bmp") {Offset_X=10,Offset_Y=10 });
            foreach(var m in matchs) {
                sugoi.Click(m);
            }
        }
    }
}