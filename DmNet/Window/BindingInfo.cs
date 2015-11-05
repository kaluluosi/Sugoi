using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet.Window
{
    public enum DisplayMode
    {
        Normal,
        GDI,
        GDI2,
        DX2,
        DX3,
        DX
    }

    public enum MouseMode
    {
        Normal,
        Windows,
        Windows2,
        Windows3,
        DX,
        DX2,
    }

    public enum KeyboardMode
    {
        Normal,
        Windows,
        DX
    }

    public enum Mode
    {
        Mode0=0,
        Mode1=1,
        Mode2=2,
        Mode3=3,
        Mode4=4,
        Mode5=5,
        Mode6=6,
        Mode7=7,
        Mode101=101,
        Mode103=103
    }

    public class BindingInfo
    {
        #region 内置配置
        public static BindingInfo DefaultForeground = new BindingInfo() { Display = DisplayMode.Normal, Mouse = MouseMode.Normal, Keyboard = KeyboardMode.Normal, Mode = Mode.Mode0 };
        public static BindingInfo GDIForground = new BindingInfo() { Display = DisplayMode.GDI, Mouse = MouseMode.Normal, Keyboard = KeyboardMode.Normal, Mode = Mode.Mode1 };
        public static BindingInfo DXFroground = new BindingInfo() { Display = DisplayMode.DX, Mouse = MouseMode.Normal, Keyboard = KeyboardMode.Normal, Mode = Mode.Mode0 };
        public static BindingInfo DXBackground = new BindingInfo() { Display = DisplayMode.DX, Mouse = MouseMode.Windows, Keyboard = KeyboardMode.Windows, Mode = Mode.Mode1 };
        public static BindingInfo ALLDXBackground = new BindingInfo() { Display = DisplayMode.DX, Mouse = MouseMode.DX, Keyboard = KeyboardMode.DX, Mode = Mode.Mode1 };
        public static BindingInfo Win3DXBackground = new BindingInfo() { Display = DisplayMode.DX, Mouse = MouseMode.Windows3, Keyboard = KeyboardMode.Windows, Mode = Mode.Mode0 };
        #endregion

        public DisplayMode Display { get; set; }
        public MouseMode Mouse { get; set; }
        public KeyboardMode Keyboard { get; set; }
        public Mode Mode { get; set; }
    }

}
