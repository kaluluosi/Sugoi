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

namespace TestApp {
    class Program {
        static void Main(string[] args) {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            Sugoi sugoi = new Sugoi();
            sugoi.AppWin = Window.FindWindow("game.bmp");
            sugoi.AppWin.BindingDmsoft(BindingInfo.GdiBackground);
            scope.SetVariable("sugoi",sugoi);
            scope.SetVariable("ImgPattern",typeof(ImgPattern));
            ScriptSource script = engine.CreateScriptSourceFromFile("test.py");
            
            var result = script.Execute(scope);
            Console.ReadKey();
        }

    }
}
