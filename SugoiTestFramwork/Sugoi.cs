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

namespace SugoiTestFramwork
{
    /// <summary>
    /// Sugoi测试框架运行对象
    /// </summary>
    public class Sugoi
    {
        private Window appWin = Window.Destop;
        private static string imgPath = "";

        /// <summary>
        /// 自动等待超时
        /// </summary>
        private int autoWaitTimeout = 3000;

        /// <summary>
        /// 操作间隔
        /// </summary>
        private int opInterval = 500;


        public Sugoi() {

        }

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

        public void SetOpInterval(int millisecond) {
            opInterval = millisecond;
        }

        public static void SetImgPath(string path) {
            imgPath = path;
        }

        #region 查找
        public Point Find(ImgPattern imgPtn) {
            //自动等待一个间隔
            Wait(opInterval);
            Point p;
            if(imgPtn.IsFullScreen) {
                p = appWin.IR.FindPic(imgPath+ imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            else {
                p = appWin.IR.FindPic(imgPtn.X1, imgPtn.Y1, imgPtn.X2, imgPtn.Y2,imgPath+ imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }

            if(IR.PointExist(p) == false) {
                throw new FindFailException(imgPtn.Images);
            }
            //返回目标元素的中心坐标
            p.X += imgPtn.Offset_X;
            p.Y += imgPtn.Offset_Y;
            return p;
        }

        public Point Find(string imgs) {
            return Find(new ImgPattern(imgs));
        }

        /// <summary>
        /// 循环等待判断图像是否存在，不抛出错误
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout">毫秒</param>
        /// <returns></returns>
        public bool Exists(ImgPattern imgPtn, int timeout = 0) {
            int waitTimeOut;
            if(timeout == 0)
                waitTimeOut = autoWaitTimeout;
            else
                waitTimeOut = timeout;
            Clock.Start();
            //循环等待检测直到找到或超时
            double msecond = Clock.Tick();
            while((msecond = Clock.Tick()) < waitTimeOut) {
                Point p;
                if(imgPtn.IsFullScreen) {
                    p = appWin.IR.FindPic(ImgPath+imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
                }
                else {
                    p = appWin.IR.FindPic(imgPtn.X1, imgPtn.Y1, imgPtn.X2, imgPtn.Y2,ImgPath+ imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
                }
                if(IR.PointExist(p))
                    return true;
            }
            return false;
        }

        public bool Exists(string imgs) {
            ImgPattern imgPtn = new ImgPattern(imgs);
            bool result = Exists(imgPtn);
            return result;
        }

        /// <summary>
        /// 等待一个固定的时间
        /// </summary>
        /// <param name="millisecond"></param>
        public void Wait(int millisecond) {
            Thread.Sleep(millisecond);
        }

        /// <summary>
        /// 等待某元素出现
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout">手动设置超时</param>
        public void Wait(ImgPattern imgPtn, int timeout = 0) {
            if(Exists(imgPtn, timeout))
                return;
            else
                throw new FindFailException(imgPtn.Images);
        }

        public void Wait(string imgs, int timeout = 0) {
            Wait(new ImgPattern(imgs), timeout);
        }

        /// <summary>
        /// 等待某图片消失
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="timeout"></param>
        public void WaitVanish(ImgPattern imgPtn, int timeout = 0) {
            int waitTimeOut;
            if(timeout == 0)
                waitTimeOut = autoWaitTimeout;
            else
                waitTimeOut = timeout;
            Clock.Start();
            //循环等待检测直到找到或超时
            while(Clock.Tick() < waitTimeOut) {
                Point p;
                if(imgPtn.IsFullScreen) {
                    p = appWin.IR.FindPic(imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
                }
                else {
                    p = appWin.IR.FindPic(imgPtn.X1, imgPtn.Y1, imgPtn.X2, imgPtn.Y2, imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
                }
                if(IR.PointExist(p) == false)
                    return;
            }

            throw new VanishFailException(imgPtn.Images);
        }

        public void WaitVanish(string imgs, int timeout = 0) {
            WaitVanish(new ImgPattern(imgs),timeout);
        }

        #endregion

        #region 鼠标

        public void Click(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.LeftClick(p.X, p.Y);
        }

        public void Click(string imgs) {
            Point p = Find(imgs);
            appWin.Mouse.LeftClick(p.X, p.Y);
        }

        public void DoubleClick(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.LeftDoubleClick(p.X, p.Y);
        }
        public void DoubleClick(string imgs) {
            Point p = Find(new ImgPattern(imgs));
            appWin.Mouse.LeftDoubleClick(p.X, p.Y);
        }

        public void RightClick(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.RightClick(p.X, p.Y);
        }

        public void RightClick(string imgs) {
            RightClick(new ImgPattern(imgs));
        }

        public void Hover(ImgPattern imgPtn) {
            Point p = Find(imgPtn);
            appWin.Mouse.MoveTo(p.X, p.Y);
        }

        public void DragDrop(ImgPattern fromImg, ImgPattern toImg) {
            Hover(fromImg);
            appWin.Mouse.LeftDown();
            Hover(toImg);
            appWin.Mouse.LeftUp();
        }
        #endregion

        #region 键盘
        /// <summary>
        /// 清楚文本框内容 输入新内容
        /// </summary>
        /// <param name="imgPtn"></param>
        /// <param name="text"></param>
        public void Say(ImgPattern imgPtn,string text) {
            DoubleClick(imgPtn);
            appWin.Keyborad.KeyPress(Keys.Control);
            appWin.Keyborad.KeyPress(Keys.A);
            appWin.Keyborad.KeyUp(Keys.Control);
            appWin.Keyborad.KeyUp(Keys.A);
            appWin.Keyborad.KeyPress(Keys.Delete);
            appWin.Say(text);
        }

        public void Say(string text) {
            appWin.Say(text);
        }

        public void Enter() {
            appWin.Keyborad.KeyPress(Keys.Enter);
        }

        #endregion

        #region 事件

        public void OnApear(ImgPattern imgPtn,Action action){
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) {
                if(Exists(imgPtn))
                    action();
            };
            timer.Start();
        }


        #endregion

        #region 工具

        #endregion
    }
}
