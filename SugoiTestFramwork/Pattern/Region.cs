using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SugoiTestFramwork.Pattern
{
    public class Region:IOperable
    {
        private Window appWin = Window.Desktop;

        public Window AppWin {
            get { return appWin; }
            set { appWin = value; }
        }
        private int x1, y1, x2, y2;

        public int Y2 {
            get { return y2; }
            set { y2 = value; }
        }

        public int X2 {
            get { return x2; }
            set { x2 = value; }
        }

        public int Y1 {
            get { return y1; }
            set { y1 = value; }
        }

        public int X1 {
            get { return x1; }
            set { x1 = value; }
        }

        public Point Center {
            get {
                return new Point((x1 + x2) / 2, (y1 + y2) / 2);
            }
        }

        public Region(Window appWin) {
            this.appWin = appWin;
            x1 = appWin.ClientRect.Left;
            y1 = appWin.ClientRect.Top;
            x2 = appWin.ClientRect.Right;
            y2 = appWin.ClientRect.Bottom;
        }

        public Region SetRect(int left, int top, int right, int bottom) {
            Region r = this.MemberwiseClone() as Region;
            r.x1 = left;
            r.y1 = top;
            r.x2 = right;
            r.y2 = bottom;
            return r;
        }

        #region 方位
        public Region Inside() {
            return this;
        }

        /// <summary>
        /// 已left，top为左下角坐标，向上range划一个框框
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public Region Above(int range=0) {
            return new Region(appWin).SetRect(x1, y1 - range, x2, y1);
        }

        public Region Left(int range = 0) {
            return null;
        }

        public Region Right(int range = 0) {
            return null;
        }

        public Region Below(int range = 0) {
            return null;
        }

        #endregion

        public Match Find(PatternBase pattern) {
            return null;
        }

        public void Click(IOperable opObj) {
            opObj.Click(this);
        }

        public void Click(Region r) {
            r.appWin.Mouse.MoveTo(Center.X, Center.Y);
            r.appWin.Mouse.LeftClick();
        }
    }
}
