using System;
using SugoiTestFramwork;
using DmNet.Windows;
using System.Threading;
namespace TestApp {
    class Program
    {
//         static void Main(string[] args) {
//             //加入搜索路径后脚本excute的时候会在这个路径下查找import的模块
//             ScriptLoader.engine.SetSearchPaths(new[] { @".\Lib" });
//             TestCase tc = ScriptLoader.LoadTestScript(args[0]);
//             TextTestRunner ttruner = new TextTestRunner();
//             ttruner.Run(tc);
//             Console.ReadKey();
//         }

        static void Main(string[] args) {
            Window win = Window.FindWindow("192.168.1.98");

            win.BindingDmsoft(BindingInfo.DefaultBackground);

            Console.WriteLine(win.IsBinding);
            while(true) {
                win.Say("hello");
                Thread.Sleep(100);
            }
            Console.ReadKey();

        }

    }
}
