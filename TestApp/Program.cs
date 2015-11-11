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

namespace TestApp
{
    class Program
    {
        static void Main(string[] args) {

            try {
                Sugoi sugoi = new Sugoi();
                ImgPattern imgPtn = new ImgPattern("computer.bmp");
                imgPtn.SetCenterOffset(30, 30);

                sugoi.DoubleClick(imgPtn);
            }
            catch(System.Exception ex) {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

    }
}
