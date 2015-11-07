using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmNet;

namespace TestApp {
    class Program {
        static void Main(string[] args) {
            dmsoft dm = new dmsoft();
            int hwnd = dm.FindWindow("", "game");
            int ret = dm.BindWindow(hwnd, "normal", "normal", "normal", 0);
            object x;
            object y;
            dm.FindPic(0, 0, 1890, 1080, "head.bmp", "000000", 0.5, 0, out x, out y);

            Console.Read();
        }

    }
}
