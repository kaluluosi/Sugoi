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

namespace SugoiTestFramwork
{
    /// <summary>
    /// Sugoi测试框架运行对象
    /// </summary>
    public class Sugoi
    {
        private Window appWin = Window.Destop;
        private int autoWaitTimeout = 3000;
        private int tryCount = 10;
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

        public void SetAutoWaitTimeout(int timeout) {
            this.autoWaitTimeout = timeout;
        }

        public void SetOpInterval(int millisecond) {
            opInterval = millisecond;
        }

        public void SetTryCount(int count) {
            this.tryCount = count;
        }

        public bool Exists(ImgPattern imgPtn) {
            Point p;
            if(imgPtn.IsFullScreen){
                p = appWin.IR.FindPic(imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            else {
                p = appWin.IR.FindPic(imgPtn.X1,imgPtn.Y1,imgPtn.X2,imgPtn.Y2,imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            return IR.PointExist(p);
        }

        public bool Exists(string imgs) {
            ImgPattern imgPtn = new ImgPattern(imgs);
            bool result = Exists(imgPtn);
            return result;
        }

        public Point Find(ImgPattern imgPtn) {
            Point p;
            if(imgPtn.IsFullScreen) {
                p = appWin.IR.FindPic(imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            else {
                p = appWin.IR.FindPic(imgPtn.X1, imgPtn.Y1, imgPtn.X2, imgPtn.Y2, imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            p.X += imgPtn.Offset_X;
            p.Y += imgPtn.Offset_Y;

            if(IR.PointExist(p) == false) {
                throw new FindFailException(imgPtn.Images, imgPtn.Images + " not found!");
            }

            return p;
        }

        public Point Find(string imgs) {
            return Find(new ImgPattern(imgs));
        }

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
    }
}
