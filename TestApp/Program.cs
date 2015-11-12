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
            Sugoi sugoi = new Sugoi();
            SugoiTest assert = new SugoiTest();
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("sugoi", sugoi);
            scope.SetVariable("asrt",assert);
            var code = engine.CreateScriptSourceFromFile("test.py");
            try {
                code.Execute(scope);
            }
            catch(System.Exception ex) {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();

        }

    }
}
