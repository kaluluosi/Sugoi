using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SugoiTestFramework.Recognition {
    public class Region {
        protected Window appWin;

        /// <summary>
        /// 区域所属的应用窗口
        /// </summary>
        public Window AppWin {
            get { return appWin; }
            set { appWin = value; }
        }

        private int x1, y1, x2, y2;
        /// <summary>
        /// find函数等待超时时间
        /// </summary>
        private double autoWaitTimeout = 3f;
        /// <summary>
        /// find函数等待时扫描频率，默认每0.5s扫描一次
        /// </summary>
        private double waitScanRate = 0.5f;

        /// <summary>
        /// 父区域
        /// </summary>
        public Region Parent { get; set; }

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

        /// <summary>
        /// 中心点
        /// </summary>
        public virtual Point Pivot {
            get {
                return new Point((x1 + x2) / 2, (y1 + y2) / 2);
            }
        }

        public double AutoWaitTimeout {
            get {
                return autoWaitTimeout;
            }

            set {
                autoWaitTimeout = value;
            }
        }

        public double WaitScanRate {
            get {
                return waitScanRate;
            }

            set {
                waitScanRate = value;
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
        public Region(Region parent) : this(parent.appWin) {
            this.Parent = parent;
        }

        #region 定位
        public Region Inside() {
            return this;
        }

        /// <summary>
        /// 以left，top为左下角坐标，向上range划一个框框
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public Region Above(int range = 0) {
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

        /// <summary>
        /// 获取感兴趣的子区域
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public Region GetRegionOfInterst(int x1, int y1, int x2, int y2) {
            //这里要做一个校验x1,y1,x2,y2不能为负数，或者(x2,y2)-(x1,y1)不能为负
            Region r = new Region(Parent) { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 };
            return r;
        }

        #endregion

        #region 找图找色找字
        /// <summary>
        /// 不抛错的找图
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Match FindSafe(PatternBase pattern) {
            Clock.Start();
            double timeout = autoWaitTimeout * 1000;
            Match m = null;
            while (Clock.Tick() < timeout) {
                m = pattern.Find(this);
                if (m != null) {
                    return m;
                }
                Wait(WaitScanRate);
            }
            return m;
        }

        public Match FindSafe(string picName) {
            return FindSafe(new ImgPattern(picName));
        }

        /// <summary>
        /// 不抛错的找图
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public List<Match> FindAllSafe(PatternBase pattern) {
            Clock.Start();
            double timeout = autoWaitTimeout * 1000;
            List<Match> ms = new List<Match>();
            while (Clock.Tick() < timeout) {
                ms = pattern.FindAll(this);
                if (ms.Count != 0) {
                    return ms;
                }
                Wait(WaitScanRate);
            }
            return ms;
        }

        public List<Match> FindAllSafe(string picName) {
            return FindAllSafe(new ImgPattern(picName));
        }

        public Match Find(PatternBase pattern) {
            Clock.Start();
            double timeout = autoWaitTimeout * 1000;
            Match m = null;
            while (Clock.Tick() < timeout) {
                m = pattern.Find(this);
                if (m != null) {
                    return m;
                }
                Wait(WaitScanRate);
            }
            throw new FindFailException(pattern.ToString());
        }

        public Match Find(string picName) {
            return Find(new ImgPattern(picName));
        }

        public List<Match> FindAll(PatternBase pattern) {
            Clock.Start();
            double timeout = autoWaitTimeout * 1000;
            List<Match> ms = new List<Match>();
            while (Clock.Tick() < timeout) {
                ms = pattern.FindAll(this);
                if (ms.Count != 0) {
                    return ms;
                }
                Wait(WaitScanRate);
            }
            throw new FindFailException(pattern.ToString());
        }

        public List<Match> FindAll(string picName) {
            return FindAll(new ImgPattern(picName));
        }

        public bool Exists(PatternBase pattern, double seconds = 0) {
            double timeout = seconds==0?autoWaitTimeout * 1000:seconds*1000;
            Match m = null;
            Clock.Start();
            while (Clock.Tick() < timeout) {
                m = pattern.Find(this);
                if (m != null) {
                    return true;
                }
                Wait(WaitScanRate);
            }
            return false;
        }

        public bool Exists(string picName,double seconds = 0) {
            return Exists(new ImgPattern(picName), seconds);
        }

        public void Wait(PatternBase pattern,double seconds = 0) {
            Clock.Start();
            double timeout = seconds == 0 ? autoWaitTimeout * 1000 : seconds * 1000;
            Match m = null;
            while (Clock.Tick() < timeout) {
                m = pattern.Find(this);
                if (m!=null) {
                    return;
                }
                Wait(WaitScanRate);
            }
        }

        public void Wait(string picName,double seconds = 0) {
            Wait(new ImgPattern(picName), seconds);
        }

        public void Wait(double second=0) {
            if (second == 0) second = autoWaitTimeout;
            Thread.Sleep((int)(second * 1000));
        }

        public void WaitVanish(PatternBase pattern,double seconds = 0) {
            Clock.Start();
            double timeout = seconds == 0 ? autoWaitTimeout * 1000 : seconds * 1000;
            Match m = null;
            while (Clock.Tick() < timeout) {
                m = pattern.Find(this);
                if (m==null) {
                    return;
                }
                Wait(WaitScanRate);
            }
        }

        public void WaitVanish(string picName,double seconds = 0) {
            WaitVanish(new ImgPattern(picName), seconds);
        }
        #endregion

        #region 鼠标操作

        public void Click(PatternBase pattern) {
            Match m = Find(pattern);
            Click(m);
        }

        public void Click(string picName) {
            Click(new ImgPattern(picName));
        }

        public void Click(Region r) {
            appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            appWin.Mouse.LeftClick();
        }

        public void DoubleClick(PatternBase pattern) {
            Match m = Find(pattern);
            DoubleClick(m);
        }

        public void DoubleClick(Region r) {
            appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            appWin.Mouse.LeftDoubleClick();
        }

        public void DoubleClick(string picName) {
            DoubleClick(new ImgPattern(picName));
        }

        public void RightClick(PatternBase pattern) {
            Match m = Find(pattern);
            RightClick(m);
        }

        public void RightClick(Region r) {
            appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            appWin.Mouse.RightClick();
        }

        public void RightClick(string picName) {
            RightClick(new ImgPattern(picName));
        }

        public void MiddleClick(PatternBase pattern) {
            Match m = Find(pattern);
            MiddleClick(m);
        }

        public void MiddleClick(Region r) {
            appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            appWin.Mouse.MiddleClick();
        }

        public void MiddleClick(string picName) {
            MiddleClick(new ImgPattern(picName));
        }

        public void Hover(Region r) {
            appWin.Mouse.MoveTo(r.Pivot.X, r.Pivot.Y);
            appWin.Mouse.MiddleClick();
        }

        public void Hover(PatternBase pattern) {
            Match m = Find(pattern);
            Hover(m);
        }

        public void Hover(string picName) {
            Hover(new ImgPattern(picName));
        }

        public void DragDrop(PatternBase from, PatternBase to) {
            Hover(from);
            appWin.Mouse.LeftDown();
            Hover(to);
            appWin.Mouse.LeftUp();
        }
        public void DragDrop(Region from, Region to) {
            Hover(from);
            appWin.Mouse.LeftDown();
            Hover(to);
            appWin.Mouse.LeftUp();
        }

        public void DragDrop(string fromPicName,string toPicName) {
            DragDrop(new ImgPattern(fromPicName), new ImgPattern(toPicName));
        }

        #endregion

        #region 键盘操作
        public void Say(PatternBase pattern, string text) {
            DoubleClick(pattern);
            ShortcutKey("LControl", "A");
            Press("Back");
            Window edit = Window.GetMousePointWindow();
            edit.Say(text);
        }

        public void Say(string picName, string text) {
            Say(new ImgPattern(picName), text);
        }

        public void Say(Region r, string text) {
            DoubleClick(r);
            Window edit = Window.GetMousePointWindow();
            edit.Say(text);
        }

        public void Enter() {
            appWin.Keyborad.KeyPress(Keys.Enter);
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
        /// Ctrl Alt键不能直接用Ctrl或Alt，要用LControl,LAlt
        /// </summary>
        /// <param name="shortcut"></param>
        public void ShortcutKey(string modifier, string key) {
            //处理快捷键 组合键
            string[] ctrls = modifier.Split('+');
            foreach (string c in ctrls) {
                KeyDown(c);
            }
            Press(key);
            foreach (string c in ctrls) {
                KeyUp(c);
            }
        }

        #endregion

        #region 事件驱动测试
        private List<System.Timers.Timer> timers = new List<System.Timers.Timer>();

        public void OnApear(PatternBase pattern, Action<PatternBase,Match> action) {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = waitScanRate;
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e) {
                Match m = null;
                if ((m = pattern.Find(this)) != null)
                    action(pattern,m);
            };
            timers.Add(timer);
        }

        public void OnApear(string PicName, Action<PatternBase,Match> action) {
            OnApear(new ImgPattern(PicName), action);
        }

        public void OnVanish(PatternBase pattern,Action<PatternBase, Match> action) {
            //预留
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = waitScanRate;
            bool flag = false;
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e) {
                Match m = null;
                if ((m = pattern.Find(this)) != null)
                    flag = true;
                else {
                    if( flag == true) {
                        flag= false;
                        action(pattern,m);
                    }
                }
            };
            timers.Add(timer);
        }

        public void OnVanish(string picName, Action<PatternBase, Match> action) {
            //预留
            OnVanish(new ImgPattern(picName), action);
        }

        /// <summary>
        /// 开始监听事件
        /// </summary>
        /// <param name="timeout">监听持续多少时间后停止</param>
        public void Observer(int timeout = -1) {
            foreach (System.Timers.Timer t in timers) {
                t.Start();
            }
            //如果timeout<0，那么就不会执行下面代码，这意味着监听会一直进行下去。
            if (timeout >= 0) {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.AutoReset = false;
                timer.Interval = timeout;
                timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e) {
                    StopObserver();
                };
                timer.Start();
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void StopObserver() {
            foreach (System.Timers.Timer t in timers) {
                t.Stop();
            }
        }

        public void ClearOberserver() {
            StopObserver();
            timers.Clear();
        }
        #endregion

        #region
        /// <summary>
        /// 屏幕截图，默认bmp格式
        /// </summary>
        /// <param name="filePath"></param>
        public void ScreenShot(string filePath) {
            appWin.ScreenShot(filePath);
        }
        #endregion
    }
}
