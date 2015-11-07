using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using DmNet.Windows;

namespace UnitTest
{
    /// <summary>
    /// UnitTestMouse 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTestMouse
    {
        private static Process app = new Process();

        public UnitTestMouse() {
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
        #endregion

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

        [TestMethod]
        public void Case_MoveMouse() {
            Window win = Window.FindWindow("记事本");
            int x = win.ClientRect.X;
            int y = win.ClientRect.Y;
            win.BindingDmsoft(BindingInfo.DefaultForeground);
            win.Mouse.MoveTo(0, 0);
            DmNet.Input.Mouse m = DmNet.Input.Mouse.DestopMouse;
            int m_x = m.Position.X;
            int m_y = m.Position.Y;
            Assert.AreEqual(x, m_x);
            Assert.AreEqual(y, m_y);
        }
    }
}
