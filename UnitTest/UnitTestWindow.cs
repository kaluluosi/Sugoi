using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using DmNet.Window;

namespace UnitTest
{
    /// <summary>
    /// UnitTestWindow 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTestWindow
    {
        private static Process app = new Process();
        public UnitTestWindow() {

        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }


        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [ClassInitialize()]
        public static void UnitTestInit(TestContext testContext) {
            app.StartInfo.FileName = @"c:\WINDOWS\SysWow64\notepad.exe";
            app.Start();
            Thread.Sleep(1000);
        }

        [ClassCleanup()]
        public static void UnitTestCleanup() {
            app.Kill();
        }

        #endregion

        [TestMethod]
        public void Case_FindWindowExisted() {
            Window win = Window.FindWindow("记事本");
            Assert.IsNotNull(win);
            Assert.IsTrue(win.Title.Contains("记事本"));
        }

        [TestMethod]
        public void Case_FindWindowNoExisted() {
            Window win = Window.FindWindow("epogjepg[aogjaejgaegjdsjf");
            Assert.IsNull(win);
        }


        public void Case_ClienSize() {
            Window win = Window.FindWindow("记事本");
            Size size = win.ClientSize;
            Assert.AreNotEqual<Size>(new Size(0, 0), size);
            Assert.AreEqual(new Size(916, 616), size);
        }


        public void Case_ClentRect() {
            Window win = Window.FindWindow("记事本");
            Rectangle rect = win.ClientRect;
            Console.WriteLine(Rectangle.Empty);
            Assert.AreNotEqual(Rectangle.Empty, rect);
            Assert.AreEqual(new Rectangle(925, 316,916,616), rect);
        }


        [TestMethod]
        public void Case_Say() {
            Window win = Window.FindWindow("记事本");
            List<Window> wins = win.FindChildren("", "Edit", FilterOption.Name);
            if(wins != null) {
                Window edit = wins[0];
                edit.Say("kk");
                Assert.IsTrue("kk".Contains(edit.Title));
            }
        }

        [TestMethod]
        public void Case_ClientToScreen() {
            Window win = Window.FindWindow("记事本");
            Point p = win.ClientToScreen(0, 0);
            Point leftTop = new Point(win.ClientRect.X, win.ClientRect.Y);
            Assert.AreEqual(p, leftTop);
        }

        [TestMethod]
        public void Case_FindChildrenExisted() {
            Window win = new Window(0);
            List<Window> windows = win.FindChildren("记事本");
            Assert.AreNotEqual(windows.Count, 0);

            bool existed = false;
            foreach(var w in windows) {
                if(w.Title.Contains("记事本")) {
                    existed = true;
                }
            }

            Assert.IsTrue(existed,"目标窗口不存在");
        }

        [TestMethod]
        public void Case_FindChildrenNotExisted() {
            Window win = new Window(0);
            List<Window> windows = win.FindChildren("flakfl;jfsaljf",option:FilterOption.Title);
            Assert.IsNull(windows);
        }

        [TestMethod]
        public void Case_MinimizeMaximize() {
            Window win = Window.FindWindow("记事本");
            win.Minimize();
            Assert.IsTrue(win.Minimized,"理应是最小化");
            Assert.IsFalse(win.Maximized, "理应不是最大化");
            win.Maximize();
            Assert.IsFalse(win.Minimized,"理应不是最小化");
            Assert.IsTrue(win.Maximized, "理应是最大化");
        }

        [TestMethod]
        public void Case_Restore() {
            Window win = Window.FindWindow("记事本");
            win.Minimize();
            win.Restore();
            Assert.IsFalse(win.Minimized, "理应不是最小化");
        }

        [TestMethod]
        public void Case_Transparent() {
            Window win = Window.FindWindow("记事本");
            win.Transparent = 30;
        }

        [TestMethod]
        public void Case_processPath() {
            Window win = Window.FindWindow("记事本");
            Assert.AreEqual(@"c:\WINDOWS\SysWow64\notepad.exe", win.ProcessPath, true);
        }

        [TestMethod]
        public void Case_C2SS2C() {
            Window win = Window.FindWindow("记事本");
            int sX = win.ClientRect.X;
            int sY = win.ClientRect.Y;
            Point cp = win.ScreenToClient(sX, sY);
            Point sp = win.ClientToScreen(cp.X, cp.Y);
            Assert.AreEqual(new Point(sX,sY), sp);
        }
    }
}
