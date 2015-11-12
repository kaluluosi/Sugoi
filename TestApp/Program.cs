using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SugoiTestFramwork;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Drawing;
using DmNet.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args) {

            //             try {
            //                 Sugoi sugoi = new Sugoi();
            //                 ImgPattern imgPtn = new ImgPattern("computer.bmp");
            //                 imgPtn.SetCenterOffset(30, 30);
            // 
            //                 sugoi.DoubleClick(imgPtn);
            //             }
            //             catch(System.Exception ex) {
            //                 Console.WriteLine(ex.Message);
            //             }
            // 
            //             Console.ReadKey();
            //             Sugoi sugoi = new Sugoi();
            //             SugoiTest asrt = new SugoiTest();
            //             ScriptEngine engine = Python.CreateEngine();
            //             ScriptScope scope = engine.CreateScope();
            //             engine.Runtime.IO.RedirectToConsole();
            //             scope.SetVariable("sugoi", sugoi);
            //             scope.SetVariable("asrt",asrt);
            // 
            //             var code = engine.CreateScriptSourceFromFile("test.py");
            // 
            //             try {
            //                 var result = code.Execute(scope);
            //                 IEnumerable<string> names = scope.GetVariableNames();
            //                 foreach(string name in names) {
            //                     Console.WriteLine(name);
            //                 }
            //             }
            //             catch(Exception ex) {
            //                 Console.WriteLine(ex.Message);
            //             }
            // 
            //             Console.ReadKey();

            TestRunner runner = new TestRunner();
            runner.LoadTestScript("test.py");
            runner.RunAll();
            
            foreach(string key in runner.Result.Keys) {
                    Console.WriteLine("{0} is {1}:{2}", key,runner.Result[key],runner.Message[key]);
            }

            Console.ReadKey();
        }

    }
}
