using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmNet;
using System.Drawing;
using System.Diagnostics;

namespace DmNet.Window
{
    

 

    public class Window
    {
        /// <summary>
        /// 大漠插件对象
        /// </summary>
        private static dmsoft dm = Dm.Instance;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hwnd">句柄</param>
        public Window(int hwnd){
            this.Hwnd = hwnd;
        }


        #region property
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public int Hwnd { get; set; }

        /// <summary>
        /// 客户区大小
        /// </summary>
        public Size ClientSize {
            get {
                COMParam<int> x,y;
                x = new COMParam<int>(0);
                y = new COMParam<int>(0);
                dm.GetClientSize(Hwnd,out x.Data,out y.Data);
                return new Size(x.Value, y.Value);
            }
            set {
                dm.SetClientSize(Hwnd, value.Width, value.Height);
            }
        }
        /// <summary>
        /// 客户区区域
        /// </summary>
        public Rectangle ClientRect {
            get {
                COMParam<int> x1,y1,x2,y2;
                x1 = new COMParam<int>(0);
                y1 = new COMParam<int>(0);
                x2 = new COMParam<int>(0);
                y2 = new COMParam<int>(0);
                dm.GetClientRect(Hwnd, out x1.Data, out y1.Data, out x2.Data, out y2.Data);
                return new Rectangle(x1.Value, y1.Value, Math.Abs(x1.Value - x2.Value), Math.Abs(y1.Value -y2.Value));
            }
        }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Title {
            get {
                return dm.GetWindowTitle(Hwnd);
            }
            set {
                dm.SetWindowText(Hwnd,value);
            }
        }

        /// <summary>
        /// 获取窗口类名
        /// </summary>
        public string ClassName {
            get {
                return dm.GetWindowClass(Hwnd);
            }
        }

        /// <summary>
        /// 获取窗口进程exe路径
        /// </summary>
        public string ProcessPath {
            get {
                return dm.GetWindowProcessPath(Hwnd);
            }
        }

        /// <summary>
        /// 获取窗口进程id
        /// </summary>
        public long ProcessID {
            get {
               return dm.GetWindowProcessId(Hwnd);
            }
        }

        /// <summary>
        /// 窗口矩形
        /// </summary>
        public Rectangle WindowRect {
            get {
                COMParam<int> x1, y1, x2, y2;
                x1 = new COMParam<int>(0);
                y1 = new COMParam<int>(0);
                x2 = new COMParam<int>(0);
                y2 = new COMParam<int>(0);
                dm.GetWindowRect(Hwnd,out x1.Data,out y1.Data,out x2.Data,out y2.Data);
                return new Rectangle(x1.Value, y1.Value, Math.Abs(x1.Value - x2.Value), Math.Abs(y1.Value - y2.Value));
            }
        }

        /// <summary>
        /// 窗口长宽
        /// </summary>
        public Size WindowSize {
            get {
                return WindowRect.Size;
            }
            set {
                dm.SetWindowSize(Hwnd, value.Width, value.Height);
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        public bool Existed {
            get {
                return dm.GetWindowState(Hwnd,0) == 1 ? true : false;
            }
        }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Active {
            get {
                return dm.GetWindowState(Hwnd,1) == 1 ? true : false;
            }
        }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible {
            get {
                return dm.GetWindowState(Hwnd, 2) == 1 ? true : false;
            }
        }
        /// <summary>
        /// 是否最小化
        /// </summary>
        public bool Minimized {
            get {
                return dm.GetWindowState(Hwnd, 3) == 1 ? true : false;
            }
        }
        /// <summary>
        /// 是否最大化
        /// </summary>
        public bool Maximized {
            get {
                return dm.GetWindowState(Hwnd, 4) == 1 ? true : false;
            }
        }
        /// <summary>
        /// 是否总是置顶
        /// </summary>
        public bool AlwaysTop {
            get {
                return dm.GetWindowState(Hwnd, 5) == 1 ? true : false;
            }
            set {
                int flag = value ? 8 : 9;
                dm.SetWindowState(Hwnd, flag);
            }
        }
        /// <summary>
        /// 是否可响应
        /// </summary>
        public bool Unresponse {
            get {
                return dm.GetWindowState(Hwnd, 6) == 1 ? true : false;
            }
        }

        /// <summary>
        /// 透明度
        /// </summary>
        public int Transparent {
            set {
                dm.SetWindowTransparent(Hwnd, value);
            }
        }

        #endregion

        #region method

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public bool Close(bool immediately=false) {
            if(immediately) {
                return Convert.ToBoolean( dm.SetWindowState(Hwnd, 13));
            }
            else {
                return Convert.ToBoolean(dm.SetWindowState(Hwnd, 0));
            }
        }

        /// <summary>
        /// 是否活动
        /// </summary>
        public bool Activate() {
            return Convert.ToBoolean( dm.SetWindowState(Hwnd, 1));
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="activa">是否取消激活</param>
        public bool Minimize(bool activa = false) {
            if(activa) {
                return Convert.ToBoolean( dm.SetWindowState(Hwnd, 3));
            }
            else {
                return Convert.ToBoolean( dm.SetWindowState(Hwnd, 2));
            }
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public bool Maximize() {
           return Convert.ToBoolean( dm.SetWindowState(Hwnd, 4));
        }

        /// <summary>
        /// 还原窗口
        /// </summary>
        public bool Restore() {
           return Convert.ToBoolean( dm.SetWindowState(Hwnd, 5));
        }

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public bool Hidden() {
            return Convert.ToBoolean(dm.SetWindowState(Hwnd, 6));
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        public bool Show() {
           return Convert.ToBoolean( dm.SetWindowState(Hwnd, 7));
        }

        /// <summary>
        /// 窗口粘帖命令
        /// </summary>
        public bool Paste() {
            return Convert.ToBoolean(dm.SendPaste(Hwnd));
        }

        /// <summary>
        /// 往窗口输入文字
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="ime">是否用ime输入</param>
        public bool Say(string msg,bool ime=false) {
            if(ime)
                return Convert.ToBoolean( dm.SendStringIme(msg));

            int result = dm.SendString(Hwnd, msg);
            if(result == 0) {
                result = dm.SendString2(Hwnd, msg);
            }
            return Convert.ToBoolean(result);
        }


        /// <summary>
        /// 把窗口坐标转换为屏幕坐标 
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>转换后的坐标点</returns>
        public Point ClientToScreen(int x,int y) {
            COMParam<int> intX = new COMParam<int>(x);
            COMParam<int> intY = new COMParam<int>(y);
            dm.ClientToScreen(Hwnd, ref intX.Data, ref intY.Data);
            return new Point(intX.Value, intY.Value);
        }

        /// <summary>
        /// 窗口坐标转客户区坐标
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public Point ScreenToClient(int x, int y) {
            COMParam<int> intX = new COMParam<int>(x);
            COMParam<int> intY = new COMParam<int>(y);
            dm.ScreenToClient(Hwnd, ref intX.Data, ref intY.Data);
            return new Point(intX.Value, intY.Value);
        }

        /// <summary>
        /// 将窗口移动到目标坐标
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public bool Move(int x, int y) {
            int result = dm.MoveWindow(Hwnd,x, y);
            return result == 1 ? true : false;
        }

        /// <summary>
        /// 查找子窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="className">窗口类名</param>
        /// <param name="option">过滤</param>
        /// <returns>返回所有符合的子窗口</returns>
        public List<Window> FindChildren(string title, string className="", FilterOption option=FilterOption.Default) {
            return EnumWindow(this, title, className, option);
        }

        /// <summary>
        /// 获取子窗口，这个方法一般没什么用，即使有这方面需求也建议重新封装一个更具体的。
        /// </summary>
        /// <param name="option">操作选项</param>
        /// <returns></returns>
        public Window GetWindow(Option option=Option.Parent) {
            Window win = new Window(dm.GetWindow(Hwnd, (int)option));
            return win;
        }


        #endregion


        #region static method

        /// <summary>
        /// 查找最符合类名或者标题名的顶层可见窗口
        /// 例如：存在窗口 "大漠插件综合工具",那么查找"大漠"也能找到这个窗口
        /// </summary>
        /// <param name="title">标题名</param>
        /// <param name="className">窗口类名</param>
        /// <param name="parent">父窗口</param>
        /// <returns>窗口</returns>
        public static Window FindWindow(string title, string className = "",Window parent=null) {
            int hwnd = 0;
            if(parent == null) {
                hwnd = Dm.Instance.FindWindow(className, title);
            }
            else {
                hwnd = Dm.Instance.FindWindowEx(parent.Hwnd,className, title);
            }
            return hwnd>0?new Window(hwnd):null;
        }


        /// <summary>
        /// 根据指定条件,枚举系统中符合条件的窗口,可以枚举到按键自带的无法枚举到的窗口
        /// </summary>
        /// <param name="paren">父窗口</param>
        /// <param name="title">标题</param>
        /// <param name="className">窗口类名</param>
        /// <param name="option">过滤</param>
        /// <returns>所有窗口</returns>
        public static List<Window> EnumWindow(Window paren, string title="", string className="", FilterOption option=FilterOption.Default) {
            string allHwnds = Dm.Instance.EnumWindow(paren.Hwnd, title, className,(int)option);
            return HwndString2Window(allHwnds);
        }

        /// <summary>
        /// 根据指定进程名以及其它条件,枚举系统中符合条件的窗口,可以枚举到按键自带的无法枚举到的窗口
        /// </summary>
        /// <param name="processName">进程名</param>
        /// <param name="title">标题</param>
        /// <param name="className">窗口类名</param>
        /// <param name="option">过滤</param>
        /// <returns></returns>
        public static List<Window> EnumWindowByProcessName(string processName, string title = "", string className = "", FilterOption option = FilterOption.Default) {
            string allHwnds = Dm.Instance.EnumWindowByProcess(processName, title, className, (int)option);
            return HwndString2Window(allHwnds);
        }

        /// <summary>
        /// 获取前面有焦点的窗口
        /// </summary>
        public static Window GetForegroundFocusWindow() {
            int hwnd = dm.GetForegroundFocus();
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 获取最上层的窗口
        /// </summary>
        public static Window GetForegroundWindow() {
            int hwnd = dm.GetForegroundWindow();
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 直接获取当前鼠标位置的窗口
        /// </summary>
        public static Window GetMousePointWindow() {
            int hwnd = dm.GetMousePointWindow();
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 获取某座标的窗口
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static Window GetPointWindow(int x, int y) {
            int hwnd = dm.GetPointWindow(x,y);
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 私有工具函数，用来将句柄字符串转换成窗口列表
        /// </summary>
        private static List<Window> HwndString2Window(string allHwnds) {
            if(string.IsNullOrEmpty(allHwnds))
                return null;

            string[] hwnds = allHwnds.Split(',');

            var a = from h in hwnds
                    select new Window(int.Parse(h));
            return a.ToList();
        }

        /// <summary>
        /// 创建窗口，如果hwnd为0则直接返回null
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private static Window CreateWindow(int hwnd){
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        #endregion
    }
}
