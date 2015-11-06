using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using DmNet.Input;

namespace Test
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine(DmNet.Dm.IsRegisted);

            Keyboard kb = new Keyboard();
            kb.KeyDown(Keys.A);
            Console.ReadKey();
        }
    }
}
