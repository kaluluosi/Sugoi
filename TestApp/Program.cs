using System;
using SugoiTestFramework;
using DmNet.Windows;
using System.Threading;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace TestApp {
    class Program {
        static void Main(string[] args) {
            //这样做是可行的！
            ScriptEngine engine = Python.CreateEngine();
            string dllPath = Path.GetFullPath("./DLLs");
            string libPath = Path.GetFullPath("./Lib");
            string pluginPath = Path.GetFullPath("./Plugins");

            //searchPath 只认绝对路径！
            engine.SetSearchPaths(new []{ dllPath,libPath,pluginPath});
            try
            {
	            engine.ImportModule("SugoiScript");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }


    }
}
