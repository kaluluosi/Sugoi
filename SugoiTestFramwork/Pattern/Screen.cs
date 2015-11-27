using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern
{
    /// <summary>
    /// 代表整个前台屏幕
    /// </summary>
    public class Screen:Region
    {
        private static Screen screen;
        public static Screen Instance {
            get {
                return screen == null ? screen = new Screen() : screen;
            }
        }

        private Screen() : base(Window.Desktop) {

        }
    }
}
