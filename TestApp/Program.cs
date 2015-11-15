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
using System.IO;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args) {
            TestCase tc = ScriptLoader.LoadTestScript(args[0]);
            TextTestRunner ttruner = new TextTestRunner();
            ttruner.Run(tc);
            Console.ReadKey();
        }

    }
}
