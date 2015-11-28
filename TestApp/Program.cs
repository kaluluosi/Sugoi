using System;
using SugoiTestFramwork;
using DmNet.Windows;
using System.Threading;
using System.IO;
using SugoiTestFramwork.Pattern;
namespace TestApp {
    class Program {
        static void Main(string[] args) {
//             Match m = Screen.Instance.Find("cpu.bmp");
//             Screen.Instance.Click(m);
//             Screen.Instance.Click(m);
//             Console.Read();
            //能否后台绑定成功要看绑定的句柄是否是DX刷新的，像Chrome这样的浏览器不能只绑定标题要绑定内部客户区的句柄。
            Region r = new Region(new Window(857250));
            r.AppWin.BindingDmsoft(BindingInfo.DefaultBackground);
            ImgPattern.SetImgPath(@"C:\Users\kalul\Desktop");
            var imgPtn = new ImgPattern("head.bmp");
            imgPtn.Similar = 0.5f;
            imgPtn.TargetOffset = new System.Drawing.Point(10, 10);
            r.Click(imgPtn);
            Console.Read();

//             Window win = new Window(857250);
//             win.BindingDmsoft(new BindingInfo() { Display = DisplayMode.dx2, Mouse = MouseMode.windows, Keyboard = KeyboardMode.windows, Mode = Mode.Mode0 });
//             var p = win.IR.FindPic(@"C:\Users\kalul\Desktop\head.bmp",sim:0.5);
//             win.Mouse.LeftClick(p.X, p.Y);
//             Console.Read();
        }


    }
}
