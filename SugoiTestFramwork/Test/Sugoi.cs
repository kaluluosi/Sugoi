using DmNet.Windows;
using SugoiTestFramework.Recognition;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SugoiTestFramework.Test {
    /// <summary>
    /// 工具类,负责运行测试程序并返回region
    /// </summary>
    public class Sugoi {
        public static readonly Process app = new Process();
        public static Region RunAndGetAppRegion(string exePath, string arguments = "", string mode = "Foreground") {
            RunApp(exePath, arguments);
            Thread.Sleep(1000);
            Window appWin = new Window(app.MainWindowHandle.ToInt32());
            switch (mode) {
                case "Foreground":
                    appWin.BindingDmsoft(BindingInfo.DefaultForeground);
                    break;
                case "Background":
                    appWin.BindingDmsoft(BindingInfo.DefaultBackground);
                    break;
            }
            if (appWin.IsBinding == false) throw new Exception("Binding window fail.Can't bind mode " + mode);
            return new Region(appWin);
        }

        public static void RunApp(string exePath, string arguments = "") {
            app.StartInfo.FileName = exePath;
            app.StartInfo.Arguments = arguments;
            app.Start();
        }

        /// <summary>
        /// 用浏览器打开链接
        /// </summary>
        /// <param name="url"></param>
        /// <param name="borwserPath"></param>
        public static Region Browser(string url, string borwserPath = @"C:\Program Files\Internet Explorer\iexplore.exe", string mode = "Foreground") {
            return RunAndGetAppRegion(borwserPath, url);
        }

        public static void CloseApp() {
            if (app != null)
                app.Kill();
        }

    }
}
