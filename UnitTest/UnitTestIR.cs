using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using DmNet.Windows;
using DmNet.ImageRecognition;
using System.Drawing;
using System.Threading;

namespace UnitTest
{
    /// <summary>
    /// UnitTestIR 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTestIR
    {
        public static Process app;
        public static Window win;

        public UnitTestIR() {
            //
            //TODO:  在此处添加构造函数逻辑
            //
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            string pic = @"game.bmp";
            app = Process.Start(@"C:\WINDOWS\system32\mspaint.exe",pic);
            Thread.Sleep(500);
            win = Window.FindWindow("game.bmp");
        }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        [ClassCleanup()]
        public static void MyClassCleanup() {
            app.CloseMainWindow();
        }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        [TestMethod]
        public void Case_WindowExited() {
            Assert.IsTrue(win.Existed);
        }

        [TestMethod]
        public void Case_FindPicAbsoluteArea() {
            bool binding = win.BindingDmsoft(BindingInfo.DefaultForeground);
            Assert.IsTrue(binding);
            Point p = win.IR.FindPic(0, 0, 1890, 1080,"head.bmp",sim:0.5f);
            Assert.IsTrue(IR.PointExist(p));
        }

        [TestMethod]
        public void Case_FindPicClientArea() {
            bool binding = win.BindingDmsoft(BindingInfo.DefaultForeground);
            Assert.IsTrue(binding);
            Point p = win.IR.FindPic("head.bmp", sim: 0.5f);
            Assert.IsTrue(IR.PointExist(p));
        }

        [TestMethod]
        public void Case_DestopIRFindPic() {
            Point p1 = Window.Destop.IR.FindPic("head.bmp", sim: 0.5f);
            Point p2 = IR.DestopIR.FindPic("head.bmp", sim: 0.5f);
            Assert.IsTrue(IR.PointExist(p1),"桌面查找失败");
            Assert.IsTrue(IR.PointExist(p2), "DestopIR 查找失败");
        }
    }
}
