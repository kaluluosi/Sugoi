using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmNet;
using System.Drawing;
using System.Diagnostics;
using DmNet.Input;
using DmNet.ImageRecognition;
using DmNet.OCR;

namespace DmNet.Windows
{
    
    public class Window
    {
        public static readonly Window Destop = new Window();

        /// <summary>
        /// 大漠插件对象
        /// Window对象创建出来时所有函数都是调用默认的dm对象操作(单例)，而默认dm对象没绑定到window上
        /// </summary>
        private dmsoft dm = Dm.Default;

        private Keyboard keyboard;
        private Mouse mouse;
        private IR ir;
        private Ocr ocr;

        /// <summary>
        /// 默认 桌面窗口
        /// </summary>
        private Window(){
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hwnd">句柄</param>
        public Window(int hwnd) {
            this.Hwnd = hwnd;
        }


        #region property
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public int Hwnd { get; set; }

        /// <summary>
        /// 大漠对象
        /// </summary>
        public dmsoft Dmsoft {
            get {
                return dm;
            }
        }

        /// <summary>
        /// 客户区大小
        /// 没有绑定窗口使用的话将获的0
        /// </summary>
        public Size ClientSize {
            get {
                if(IsBinding) {
                    COMParam<int> x, y;
                    x = new COMParam<int>(0);
                    y = new COMParam<int>(0);
                    dm.GetClientSize(Hwnd, out x.Data, out y.Data);
                    return new Size(x.Value, y.Value);
                }
                else {
                    return new Size(dm.GetScreenWidth(), dm.GetScreenHeight());
                }
            }
            set {
                if(IsBinding == false)
                    return;
                dm.SetClientSize(Hwnd, value.Width, value.Height);
            }
        }
        /// <summary>
        /// 客户区区域
        /// </summary>
        public Rectangle ClientRect {
            get {
                COMParam<int> x1, y1, x2, y2;
                x1 = new COMParam<int>(0);
                y1 = new COMParam<int>(0);
                x2 = new COMParam<int>(0);
                y2 = new COMParam<int>(0);
                dm.GetClientRect(Hwnd, out x1.Data, out y1.Data, out x2.Data, out y2.Data);
                return new Rectangle(x1.Value, y1.Value, Math.Abs(x1.Value - x2.Value), Math.Abs(y1.Value - y2.Value));
            }
        }

        public Keyboard Keyborad {
            get {
                if(keyboard == null)
                    keyboard = new Keyboard(this);
                return keyboard;
            }
        }

        public Mouse Mouse {
            get {
                if(mouse == null)
                    mouse = new Mouse(this);
                return mouse;
            }
        }

        public IR IR {
            get {
                if(ir == null)
                    ir = new IR(this);
                return ir;
            }
        }

        public Ocr Ocr {
            get {
                if(ocr == null)
                    ocr = new Ocr(this);
                return ocr;
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
                dm.SetWindowText(Hwnd, value);
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
                dm.GetWindowRect(Hwnd, out x1.Data, out y1.Data, out x2.Data, out y2.Data);
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
                return dm.GetWindowState(Hwnd, 0) == 1 ? true : false;
            }
        }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Active {
            get {
                return dm.GetWindowState(Hwnd, 1) == 1 ? true : false;
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

        /// <summary>
        /// 收费功能，免费无效
        /// </summary>
        public bool IsBinding { get; set; }


        #endregion

        #region method
        /// <summary>
        /// 取消绑定
        /// </summary>
        /// <returns></returns>
        public bool UnBindingDmsoft() {
            bool result = Convert.ToBoolean(dm.UnBindWindow());
            if(result){
                this.dm = Dm.Default;
                IsBinding = false;
            }
            return result;
        }

        /// <summary>
        /// 为窗口绑定独立的dm对象,没有为窗口对象绑定dm的话，窗口中调用键鼠和图像识别将是对整个屏幕操作。
        /// 如果绑定的句柄为0或-1，直接抛错。
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool BindingDmsoft(dmsoft dm, BindingInfo info) {
            this.dm = dm;
            if(Hwnd <= 0)
                throw new InvalidHandleException(Hwnd, "Invalid Handle");
            int result = dm.BindWindow(this.Hwnd, info.Display.ToString(), info.Mouse.ToString(), info.Keyboard.ToString(), (int)info.Mode);
            IsBinding = result == 1 ? true : false;
            return IsBinding;
        }

        public bool BindingDmsoft(BindingInfo info) {
            //将默认的dm对象替换成新建的dm对象new dmsoft();
            return BindingDmsoft(new dmsoft(), info);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public bool Close(bool immediately = false) {
            if(immediately) {
                return Convert.ToBoolean(dm.SetWindowState(Hwnd, 13));
            }
            else {
                return Convert.ToBoolean(dm.SetWindowState(Hwnd, 0));
            }
        }

        /// <summary>
        /// 是否活动
        /// </summary>
        public bool Activate() {
            return Convert.ToBoolean(dm.SetWindowState(Hwnd, 1));
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="activa">是否取消激活</param>
        public bool Minimize(bool activa = false) {
            if(activa) {
                return Convert.ToBoolean(dm.SetWindowState(Hwnd, 3));
            }
            else {
                return Convert.ToBoolean(dm.SetWindowState(Hwnd, 2));
            }
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public bool Maximize() {
            return Convert.ToBoolean(dm.SetWindowState(Hwnd, 4));
        }

        /// <summary>
        /// 还原窗口
        /// </summary>
        public bool Restore() {
            return Convert.ToBoolean(dm.SetWindowState(Hwnd, 5));
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
            return Convert.ToBoolean(dm.SetWindowState(Hwnd, 7));
        }

        /// <summary>
        /// 窗口粘帖命令
        /// </summary>
        public bool Paste() {
            return Convert.ToBoolean(dm.SendPaste(Hwnd));
        }

        /// <summary>
        /// 往窗口输入文字。
        /// 注：如果此函数调用后无效，那么应该是窗口句柄抓错了,窗口必须是能接收输入的对象，比如textbox，换窗口里的子窗口试一下。
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="ime">是否用ime输入[收费接口]</param>
        public bool Say(string msg, bool ime = false) {
            if(ime)
                return Convert.ToBoolean(dm.SendStringIme(msg));

            int result = dm.SendString(Hwnd, msg);
            if(result == 0) {
                result = dm.SendString2(Hwnd, msg);
            }
            return Convert.ToBoolean(result);
            
        }

        public void PasteString(string msg) {
            dm.SetClipboard(msg);
            dm.SendPaste(Hwnd);
        }


        /// <summary>
        /// 把窗口坐标转换为屏幕坐标 
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>转换后的坐标点</returns>
        public Point ClientToScreen(int x, int y) {
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
            int result = dm.MoveWindow(Hwnd, x, y);
            return result == 1 ? true : false;
        }

        /// <summary>
        /// 查找子窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="className">窗口类名</param>
        /// <param name="option">过滤</param>
        /// <returns>返回所有符合的子窗口</returns>
        public List<Window> FindChildren(string title, string className = "", FilterOption option = FilterOption.Default) {
            return EnumWindow(this, title, className, option);
        }

        /// <summary>
        /// 获取子窗口，这个方法一般没什么用，即使有这方面需求也建议重新封装一个更具体的。
        /// </summary>
        /// <param name="option">操作选项</param>
        /// <returns></returns>
        public Window GetWindow(Option option = Option.Parent) {
            Window win = new Window(dm.GetWindow(Hwnd, (int)option));
            return win;
        }

        /// <summary>
        /// 截取客户区画面保存为png
        /// 如果没有绑定窗口
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ScreenShot(string fileName,string imgType="bmp") {
            int result = 0;
            switch(imgType.ToLower()) {
                case "png":
                    result = dm.CapturePng(WindowRect.Left, WindowRect.Top, WindowRect.Right, WindowRect.Bottom, fileName+'.'+imgType);
                    break;
                case "jpg":
                    result = dm.CaptureJpg(WindowRect.Left, WindowRect.Top, WindowRect.Right, WindowRect.Bottom, fileName + '.' + imgType, 100);
                    break;
                case "bmp":
                    result = dm.Capture(WindowRect.Left, WindowRect.Top, WindowRect.Right, WindowRect.Bottom, fileName + '.' + imgType);
                    break;
                default:
                    result = dm.Capture(WindowRect.Left, WindowRect.Top, WindowRect.Right, WindowRect.Bottom, fileName + '.' + imgType);
                    break;
            }
            return Convert.ToBoolean(result);
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
        public static Window FindWindow(string title, string className = "", Window parent = null) {
            int hwnd = 0;
            if(parent == null) {
                hwnd = Dm.Default.FindWindow(className, title);
            }
            else {
                hwnd = Dm.Default.FindWindowEx(parent.Hwnd, className, title);
            }
            return hwnd > 0 ? new Window(hwnd) : null;
        }


        /// <summary>
        /// 根据指定条件,枚举系统中符合条件的窗口,可以枚举到按键自带的无法枚举到的窗口
        /// </summary>
        /// <param name="paren">父窗口</param>
        /// <param name="title">标题</param>
        /// <param name="className">窗口类名</param>
        /// <param name="option">过滤</param>
        /// <returns>所有窗口</returns>
        public static List<Window> EnumWindow(Window paren, string title = "", string className = "", FilterOption option = FilterOption.Default) {
            string allHwnds = Dm.Default.EnumWindow(paren.Hwnd, title, className, (int)option);
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
            string allHwnds = Dm.Default.EnumWindowByProcess(processName, title, className, (int)option);
            return HwndString2Window(allHwnds);
        }

        /// <summary>
        /// 获取前面有焦点的窗口
        /// </summary>
        public static Window GetForegroundFocusWindow() {
            int hwnd = Dm.Default.GetForegroundFocus();
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 获取最上层的窗口
        /// </summary>
        public static Window GetForegroundWindow() {
            int hwnd = Dm.Default.GetForegroundWindow();
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 直接获取当前鼠标位置的窗口
        /// </summary>
        public static Window GetMousePointWindow() {
            int hwnd = Dm.Default.GetMousePointWindow();
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        /// <summary>
        /// 获取某座标的窗口
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static Window GetPointWindow(int x, int y) {
            int hwnd = Dm.Default.GetPointWindow(x, y);
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
        private static Window CreateWindow(int hwnd) {
            return hwnd > 0 ? new Window(hwnd) : null;
        }

        #endregion
    }
}
