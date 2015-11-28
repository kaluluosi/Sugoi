using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SugoiTestFramwork.Pattern
{
    public class Region
    {
        protected Window appWin;

        /// <summary>
        /// 区域所属的应用窗口
        /// </summary>
        public Window AppWin {
            get { return appWin; }
            set { appWin = value; }
        }

        /// <summary>
        /// 父区域
        /// </summary>
        public Region Parent { get; set; }

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

        public virtual Point Pivot {
            get {
                return new Point((x1 + x2) / 2, (y1 + y2) / 2);
            }
        }

        /// <summary>
        /// 如果Region是从AppWin中创建的，那么就以appwin的客户区为区域。
        /// </summary>
        /// <param name="appWin"></param>
        public Region(Window appWin) {
            this.appWin = appWin;
            X1 = Y1 = 0;
            X2 = appWin.ClientSize.Width;
            Y2 = appWin.ClientSize.Height;
        }

        /// <summary>
        /// 从父region创建子region
        /// 若不另外设置区域那么区域将跟父region一样大
        /// </summary>
        /// <param name="parent"></param>
        public Region(Region parent):this(parent.appWin) {
            this.Parent = parent;
        }

        #region 方位
        public Region Inside() {
            return this;
        }

        /// <summary>
        /// 以left，top为左下角坐标，向上range划一个框框
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public Region Above(int range=0) {
            return new Region(appWin) { X1 = x1, Y1 = y1 - range, X2 = x2, Y2 = y1 };
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

        public Region GetRegionOfInterst(int x1,int y1,int x2,int y2) {
            //这里要做一个校验x1,y1,x2,y2不能为负数，或者(x2,y2)-(x1,y1)不能为负
            Region r = new Region(Parent) { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 };
            return r;
        }

        #endregion

        #region 找图找色找字
        public Match Find(PatternBase pattern) {
            return pattern.Find(this);
        }

        public Match Find(string picName) {
            return new ImgPattern(picName).Find(this);
        }

        public List<Match> FindAll(PatternBase pattern) {
            return pattern.FindAll(this);
        }

        public List<Match> FindAll(string picName) {
            return new ImgPattern(picName).FindAll(this);
        }

        #endregion

        #region 操作相关

        public void Click(PatternBase pattern) {
            Match m = Find(pattern);
            Click(m);
        }

        public void Click(string picName) {
            Match m = Find(picName);
            Click(m);
        }

        public void Click(Region r) {
            r.appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            r.appWin.Mouse.LeftClick();
        }

        public void DoubleClick(PatternBase pattern) {
            Match m = Find(pattern);
            DoubleClick(m);
        }

        public void DoubleClick(Region r) {
            r.appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            r.appWin.Mouse.LeftDoubleClick();
        }

        public void DoubleClick(string picName) {
            Match m = Find(picName);
            DoubleClick(m);
        }
        #endregion

    }
}
