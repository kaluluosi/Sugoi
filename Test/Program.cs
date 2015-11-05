using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DmNet.Window;
using Dm;

namespace Test
{
    class Program
    {
        static void Main(string[] args) {
            dmsoft dm = new dmsoft();
            Window win = Window.FindWindow("记事本");
            dm.BindWindow(win.Hwnd,"dx","windows","windows",1);
            Console.WriteLine(win.Title);
            dm.SendString(win.Hwnd,"hello");
            Console.Read();
        }
    }
}
