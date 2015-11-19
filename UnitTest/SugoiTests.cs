using Microsoft.VisualStudio.TestTools.UnitTesting;
using SugoiTestFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Tests
{
    [TestClass()]
    public class SugoiTests
    {
        private static Sugoi sugoi = new Sugoi();

        [TestInitialize()]
        public void UnitTestInit() {
            sugoi.RunAndBindApp(@"C:\Users\dengxuan\Documents\GitHub\Sugoi\TestWindow\bin\Debug\TestWindow.exe", mode: "Foreground");
            sugoi.Wait(500);
            //sugoi.AppWin.Minimize();
        }

        [TestCleanup()]
        public void UnitTestCleanup() {
            sugoi.CloseApp();
        }

        [TestMethod]
        public void ExistsTest() {
            Assert.IsTrue(sugoi.Exists("computer.bmp"), "理应找得到computer,实际没找到");
        }

        [TestMethod]
        public void NotExistTest() {
            sugoi.Click("BtnVanish.bmp");
            sugoi.Wait(1300);
            Assert.IsTrue(sugoi.NotExist("computer.bmp"), "理应找不到computer.bmp，实际找到了。可能绑定后台失败了。");
        }

        [TestMethod]
        public void FindAllTest() {
            var matchs = sugoi.FindAll("checkbox.bmp");
            foreach(var m in matchs) {
                sugoi.Click(m);
            }
            AssertNotExist("checkbox.bmp");
        }
        [TestMethod]
        public void WaitTest() {
            sugoi.Click("BtnVanish.bmp");
            try {
                sugoi.WaitVanish("computer.bmp");
            }
            catch(Exception e) {
                Assert.Fail("computer理应消失");
            }
            sugoi.Click("BtnAppear.bmp");
            try {
                sugoi.Wait("computer.bmp");
            }
            catch(Exception e) {
                Assert.Fail("computer理应显示");
            }
        }

        [TestMethod]
        public void DoubleClickTest() {
            sugoi.DoubleClick("computer.bmp");
            AssertNotExist("computer.bmp");
        }

        [TestMethod]
        public void RightClickTest() {
            sugoi.RightClick("computer.bmp");
            AssertNotExist("computer.bmp");
        }

        [TestMethod]
        public void SayTest() {
            sugoi.Say("textbox.bmp","hello");
            sugoi.Hover(new System.Drawing.Point(0, 0));
            AssertExist("textboxflag.bmp");
        }

        [Ignore]
        [TestMethod]
        public void OcrTest() {
            /*var p = sugoi.Find(new ImgPattern("btn.bmp"){Similar=0.2});*/
            string text = sugoi.Ocr(new ImgPattern("btn.bmp") { Similar = 0.2 }, "f8f8f9-000000|ffffff-000000|f8f8f9-000000|fcfcfc-000000|fbfbfc-000000|fbfbfc-000000|e9eae");
            Assert.AreEqual("推荐阵容", text);
        }

        [Ignore]
        [TestMethod]
        public void OpenUrlTest() {
            sugoi.Browser("www.baidu.com");
            sugoi.Wait(1000);
            int handle = Sugoi.app.MainWindowHandle.ToInt32();
            Assert.IsTrue(handle != 0, "没有获得浏览器窗口句柄");
            sugoi.Wait(1000);
            AssertExist("baidulogo.bmp");
        }

        public void AssertExist(string picName) {
            Assert.IsTrue(sugoi.Exists(picName), "理应找得到{0},实际没找到",picName);
        }
        public void AssertNotExist(string picName) {
            Assert.IsTrue(sugoi.NotExist(picName), "理应找不到{0},实际找到",picName);
        }
    }
}