using System;
using SugoiTestFramwork;
using DmNet.Windows;
using System.Threading;
using System.IO;
using SugoiTestFramwork.Pattern;
namespace TestApp {
    class Program {
        static void Main(string[] args) {
            Match m = Screen.Instance.Find("cpu.bmp");
            Screen.Instance.Click(m);
            Screen.Instance.Click(m);
            Console.Read();
        }


    }
}
