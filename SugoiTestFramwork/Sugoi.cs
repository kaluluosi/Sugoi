using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmNet.Windows;
using DmNet.ImageRecognition;
using System.Drawing;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace SugoiTestFramwork
{
    /// <summary>
    /// Sugoi测试框架运行对象
    /// </summary>
    public class Sugoi:SugoiTest
    {
        public static readonly Process app = new Process();
        private Window appWin = Window.Destop;
        private static string imgPath = "";

        /// <summary>
        /// 自动等待超时
        /// </summary>
        private int autoWaitTimeout = 3000;
        private int opInterval = 400;


        /// <summary>
        /// 测试的软件的窗口对象
        /// </summary>
        public Window AppWin {
            get {
                return appWin;
            }
            set {
                appWin = value;
            }
        }

        public static string ImgPath {
            get {
                return imgPath;
            }
        }

        public void SetAutoWaitTimeout(int timeout) {
            this.autoWaitTimeout = timeout;
        }

       

        #region 查找
        /// <summary>
        /// 快速查找图像,直接查找当前帧
        /// </summary>
        /// <param name="imgPtn">找到就返回计算过的p，没找到就返回p(-1,-1)</param>
        /// <returns></returns>
        public Point FindFast(ImgPattern imgPtn) {
            Point p;
            if(imgPtn.IsFullScreen) {
                p = appWin.IR.FindPic(imgPath+ imgPtn.PicNames, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            else {
                p = appWin.IR.FindPic(imgPtn.X1, imgPtn.Y1, imgPtn.X2, imgPtn.Y2,imgPath+ imgPtn.PicNames, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }

            //没有找到就直接返回
            if(IR.PointExist(p) == false) {
                return p;
            }
            //返回目标元素的中心坐标
            p.X += imgPtn.Offset_X;
            p.Y += imgPtn.Offset_Y;
            return p;
        }

        public Point FindFast(string picNames) {
            return FindFast(new ImgPattern(picNames));
        }


        public List<Point> FindAll(ImgPattern imgPtn) {
            List<Point> matchs = new List<Point>();
            matchs = appWin.IR.FindAllPic(imgPtn.PicNames, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            var temp = from m in matchs
                       select new Point() { X = m.X + imgPtn.Offset_X, Y = m.Y + imgPtn.Offset_Y };
            return temp.ToList();
        }

        public List<Point> FindAll(string picNames) {
            return FindAll(new ImgPattern(picNames));
        }

        /// <summary>
        /// 查找图像，会不断重试直到找到或超时抛错
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Point Find(ImgPattern imgPtn,int timeout=0) {
            int waitTimeOut = timeout == 0 ? autoWaitTimeout : timeout;
            Clock.Start();
            while (Clock.Tick() < waitTimeOut) {
                Point p = FindFast(imgPtn);
                if (IR.PointExist(p) == false) continue;
                return p;
            }
            //最后没有找到就抛错
            throw new FindFailException(imgPtn.PicNames);
        }

        public Point Find(string picNames, int timeout = 0) {
            return Find(new ImgPattern(picNames), timeout);
        }


        /// <summary>
        /// 循环等待判断图像是否存在，不抛出错误，返回值
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout">毫秒</param>
        /// <returns></returns>
        public bool Exists(ImgPattern imgPtn, int timeout = 0) {
            int waitTimeOut = timeout == 0 ? autoWaitTimeout : timeout;
            Clock.Start();
            //循环等待检测直到找到或超时
            while(Clock.Tick() < waitTimeOut) {
                Point p = FindFast(imgPtn);
                if(IR.PointExist(p))
                    return true;
            }
            return false;
        }

        public bool Exists(string picNames,int timeout=0) {
            ImgPattern imgPtn = new ImgPattern(picNames);
            bool result = Exists(imgPtn,timeout);
            return result;
        }

        /// <summary>
        /// 判断图片是否不存在,不循环检测，直接查找然后返回
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <returns></returns>
        public bool NotExist(ImgPattern imgPtn) {
            Point p = FindFast(imgPtn);
            return IR.PointExist(p) == false;
        }

        public bool NotExist(string picName) {
            return NotExist(new ImgPattern(picName));
        }

        /// <summary>
        /// 等待一个固定的时间
        /// </summary>
        /// <param name="millisecond"></param>
        public void Wait(int millisecond) {
            Thread.Sleep(millisecond);
        }

        /// <summary>
        /// 等待某元素出现，不返回值
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout">手动设置超时</param>
        public void Wait(ImgPattern imgPtn, int timeout = 0) {
            if(Exists(imgPtn, timeout))
                return;
            else
                throw new FindFailException(imgPtn.PicNames);
        }

        public void Wait(string picNames, int timeout = 0) {
            Wait(new ImgPattern(picNames), timeout);
        }

        /// <summary>
        /// 等待某图片消失
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout"></param>
        public void WaitVanish(ImgPattern imgPtn, int timeout = 0) {
            int waitTimeOut = timeout == 0 ? autoWaitTimeout : timeout;
            Clock.Start();
            //循环等待检测直到找到或超时
            while(Clock.Tick() < waitTimeOut) {
                if(NotExist(imgPtn))
                    return;
            }

            throw new VanishFailException(imgPtn.PicNames);
        }

        public void WaitVanish(string picNames, int timeout = 0) {
            WaitVanish(new ImgPattern(picNames),timeout);
        }

        #endregion

        #region 鼠标

        public void Click(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.LeftClick(p.X, p.Y);
            Wait(opInterval);
        }

        public void Click(string picNames) {
            Click(new ImgPattern(picNames));
        }

        public void Click(Point p) {
            appWin.Mouse.LeftClick(p.X, p.Y);
        }

        public void DoubleClick(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.LeftDoubleClick(p.X, p.Y);
            Wait(opInterval);
        }
        public void DoubleClick(string picNames) {
            DoubleClick(new ImgPattern(picNames));
        }

        public void DoubleClick(Point p) {
            appWin.Mouse.LeftDoubleClick(p.X, p.Y);
        }

        public void RightClick(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.RightClick(p.X, p.Y);
            Wait(opInterval);
        }

        public void RightClick(string picNames) {
            RightClick(new ImgPattern(picNames));
        }

        public void RightClick(Point p) {
            appWin.Mouse.RightClick(p.X, p.Y);
        }

        public void Hover(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.MoveTo(p.X, p.Y);
            Wait(opInterval);
        }

        public void Hover(string img) {
            Hover(new ImgPattern(img));
        }

        public void Hover(Point p) {
            appWin.Mouse.MoveTo(p.X, p.Y);
        }

        public void DragDrop(ImgPattern fromImgPtn, ImgPattern toImgPtn) {
            Hover(fromImgPtn);
            appWin.Mouse.LeftDown();
            Hover(toImgPtn);
            appWin.Mouse.LeftUp();
            Wait(opInterval);
        }

        public void DragDrop(string fromImg, string toImg) {
            DragDrop(new ImgPattern(fromImg), new ImgPattern(toImg));
        }

        public void DragDrop(Point from, Point to) {
            Hover(from);
            appWin.Mouse.LeftDown();
            Hover(to);
            appWin.Mouse.LeftUp();
            Wait(opInterval);
        }
        #endregion

        #region 键盘
        /// <summary>
        /// 清除文本框内容 输入新内容
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="text"></param>
        public void Say(ImgPattern imgPtn,string text) {
            DoubleClick(imgPtn);
            appWin.Say(text);
        }

        public void Say(string picName,string text) {
            Say(new ImgPattern(picName),text);
        }

        public void Say(Point p,string text) {
            DoubleClick(p);
            appWin.Say(text);
        }

        public void Enter() {
            appWin.Keyborad.KeyPress(Keys.Enter);
            Wait(opInterval);
        }

        public void Press(string keyname) {
            appWin.Keyborad.KeyPress(keyname);
        }

        public void Press(int keycode) {
            appWin.Keyborad.KeyPress(keycode);
        }

        public void KeyDown(string keyname) {
            appWin.Keyborad.KeyPress(keyname);
        }

        public void KeyDown(int keycode) {
            appWin.Keyborad.KeyPress(keycode);
        }

        public void KeyUp(string keyname) {
            appWin.Keyborad.KeyUp(keyname);
        }

        public void KeyUp(int keycode) {
            appWin.Keyborad.KeyUp(keycode);
        }

        /// <summary>
        /// 还没写好
        /// </summary>
        /// <param name="shortcut"></param>
        public void ShortcutKey(string shortcut) {
            //处理快捷键 组合键
            string[] keynames = shortcut.Split('+');
            foreach(string key in keynames) {
                KeyDown(key);
            }
            foreach(string key in keynames) {
                KeyUp(key);
            }
        }

        #endregion

        #region 事件
        private List<System.Timers.Timer> timers = new List<System.Timers.Timer>();

        public void OnApear(ImgPattern imgPtn,Action<ImgPattern> action){
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) {
                if(Exists(imgPtn))
                    action(imgPtn);
            };
            timers.Add(timer);
        }

        public void OnApear(string picNames, Action<ImgPattern> action) {
            OnApear(new ImgPattern(picNames), action);
        }

        /// <summary>
        /// 开始监听事件
        /// </summary>
        /// <param name="timeout">监听持续多少时间后停止</param>
        public void Observer(int timeout=-1) {
            foreach(System.Timers.Timer t in timers) {
                t.Start();
            }
            if(timeout >= 0) {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.AutoReset = false;
                timer.Interval = timeout;
                timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) {
                    StopObserver();
                };
                timer.Start();
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void StopObserver() {
            foreach(System.Timers.Timer t in timers) {
                t.Stop();
            }
        }

        #endregion

        #region 测试工具
        public void AssertExist(string img, string message, int timeout = 0) {
            AssertTrue(Exists(img,timeout), message);
        }

        public void AssertNotExist(string img, string message, int timeout = 0) {
            AssertFalse(Exists(img, timeout), message);
        }

        public void RunAndBindApp(string exePath,string arguments="",string mode = "Foreground") {
            RunApp(exePath,arguments);
            Thread.Sleep(1000);
            appWin = new Window(app.MainWindowHandle.ToInt32());
            switch (mode) {
                case "Foreground":
                    appWin.BindingDmsoft(BindingInfo.DefaultForeground);
                    break;
                case "Background":
                    appWin.BindingDmsoft(BindingInfo.DefaultBackground);
                    break;
            }
            if (appWin.IsBinding == false) throw new Exception("Binding window fail.Can't bind mode " + mode);
        }

        public void RunApp(string exePath,string arguments="") {
            app.StartInfo.FileName =exePath;
            app.StartInfo.Arguments = arguments;
            app.Start();
        }

        /// <summary>
        /// 用浏览器打开链接
        /// </summary>
        /// <param name="url"></param>
        /// <param name="borwserPath"></param>
        public void Browser(string url, string borwserPath = @"C:\Program Files\Internet Explorer\iexplore.exe") {
            RunAndBindApp(borwserPath, url);
        }

        public void CloseApp() {
            if (app != null)
                app.Kill();
        }

        /// <summary>
        /// 找到窗口并绑定
        /// </summary>
        /// <param name="title"></param>
        /// <param name="mode"></param>
        public void BindingWindow(string title, string mode) {
            appWin = Window.FindWindow(title);
            if (appWin == null) throw new Exception("Binding window fail.Can't find " + title);
            switch (mode) {
                case "Foreground":
                    appWin.BindingDmsoft(BindingInfo.DefaultForeground);
                    break;
                case "Background":
                    appWin.BindingDmsoft(BindingInfo.DefaultBackground);
                    break;
            }
            if (appWin.IsBinding == false) throw new Exception("Binding window fail.Can't bind mode " + mode);
        }
        #endregion

        #region 静态方法
        public static void SetImgPath(string path) {
            imgPath = path;
        }

        #endregion
    }
}
