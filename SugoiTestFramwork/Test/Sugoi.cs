using DmNet.Windows;
using SugoiTestFramwork.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Test {
    public class Sugoi {
        public static Screen DesktopScreen {
            get {
                return Screen.Instance;
            }
        }

        public static Region MonitorWindow(int hwnd,string mode) {
            Window win = new Window(hwnd);
            if (mode == "Foreground") {
                win.BindingDmsoft(BindingInfo.DefaultForeground);
            }
            else {
                win.BindingDmsoft(BindingInfo.DefaultBackground);
            }
            return new Region(win);
        }
    }
}
