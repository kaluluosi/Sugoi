using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet.Windows
{
    public enum DisplayMode
    {
        normal,
        gdi,
        gdi2,
        dx2,
        dx3,
        dx
    }

    public enum MouseMode
    {
        normal,
        windows,
        windows2,
        windows3,
        dx,
        dx2,
    }

    public enum KeyboardMode
    {
        normal,
        windows,
        dx
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
        /// <summary>
        /// 默认前台 图像识别、键盘鼠标都是前台
        /// </summary>
        public static BindingInfo DefaultForeground = new BindingInfo() { Display = DisplayMode.normal, Mouse = MouseMode.normal, Keyboard = KeyboardMode.normal, Mode = Mode.Mode0 };
        /// <summary>
        /// GDI前台 图像识别用gdi刷新窗口、键盘鼠标都是前台
        /// </summary>
        public static BindingInfo GdiForeground = new BindingInfo() { Display = DisplayMode.gdi, Mouse = MouseMode.normal, Keyboard = KeyboardMode.normal, Mode = Mode.Mode1 };
        /// <summary>
        /// DX前台，图像识别DX后台刷新，键盘鼠标都是前台
        /// </summary>
        public static BindingInfo DxForeground = new BindingInfo() { Display = DisplayMode.dx, Mouse = MouseMode.normal, Keyboard = KeyboardMode.normal, Mode = Mode.Mode0 };
        /// <summary>
        /// DX后台 图像识别DX后台刷新、键盘鼠标windows后台消息
        /// </summary>
        public static BindingInfo DxBackground = new BindingInfo() { Display = DisplayMode.dx, Mouse = MouseMode.windows, Keyboard = KeyboardMode.windows, Mode = Mode.Mode1 };
        /// <summary>
        /// 全DX后台 图像识别、键盘鼠标全用DX后台
        /// </summary>
        public static BindingInfo ALLdxBackground = new BindingInfo() { Display = DisplayMode.dx, Mouse = MouseMode.dx, Keyboard = KeyboardMode.dx, Mode = Mode.Mode1 };
        /// <summary>
        /// win3子窗口 键鼠后台
        /// </summary>
        public static BindingInfo Win3dxBackground = new BindingInfo() { Display = DisplayMode.dx, Mouse = MouseMode.windows3, Keyboard = KeyboardMode.windows, Mode = Mode.Mode0 };
        #endregion

        public DisplayMode Display { get; set; }
        public MouseMode Mouse { get; set; }
        public KeyboardMode Keyboard { get; set; }
        public Mode Mode { get; set; }
    }

}
