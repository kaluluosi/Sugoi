using System;
using SugoiTestFramwork;
namespace TestApp {
    class Program
    {
        static void Main(string[] args) {
            //加入搜索路径后脚本excute的时候会在这个路径下查找import的模块
            ScriptLoader.engine.SetSearchPaths(new[] { @".\Lib" });
            TestCase tc = ScriptLoader.LoadTestScript(args[0]);
            TextTestRunner ttruner = new TextTestRunner();
            ttruner.Run(tc);
            Console.ReadKey();
        }

    }
}
