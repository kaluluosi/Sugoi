using System;
using SugoiTestFramwork;
using DmNet.Windows;
using System.Threading;
using System.IO;
namespace TestApp {
    class Program
    {
        static void Main(string[] args) {
            //加入搜索路径后脚本excute的时候会在这个路径下查找import的模块
            ScriptLoader.engine.SetSearchPaths(new[] { @".\Lib" });
            TestCase tc = ScriptLoader.LoadTestScript(args[0]);
            if(Directory.Exists(Sugoi.ScriptPath + @"Log")) {
                Directory.Delete(Sugoi.ScriptPath + @"Log",true);
            }
            Directory.CreateDirectory(Sugoi.ScriptPath + "Log/Error");
            Directory.CreateDirectory(Sugoi.ScriptPath + "Log/Failed");

            tc.Failed += tc_Failed;
            tc.Error += tc_Error;
            TextTestRunner ttruner = new TextTestRunner();
            ttruner.Run(tc);
            Console.ReadKey();
        }

        static void tc_Error(object sender, TestMethod e) {
            
            ScriptLoader.sugoi.ScreenShot(e.Name,@"Log/Error/");
        }

        static void tc_Failed(object sender, TestMethod e) {
            ScriptLoader.sugoi.ScreenShot(e.Name,@"Log/Failed/");
        }


    }
}
