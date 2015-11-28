using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using dmNet;
using DmNet.Windows;

namespace DmNet.Input
{
    /// <summary>
    /// 鼠标在绑定了窗口后将相对于窗口移动
    /// </summary>
    public class Mouse
    {
        public static readonly Mouse DestopMouse = new Mouse(Window.Desktop);

        private Window win;

        public Mouse(Window win) {
            this.win = win;
        }


        public Point Position {
            get {
                COMParam<int> x = new COMParam<int>(0);
                COMParam<int> y = new COMParam<int>(0);
                win.Dmsoft.GetCursorPos(out x.Data, out y.Data);
                return new Point(x.Value, y.Value);
            }
        }

        public void SetMouseDelay(InputMode mode, int delay) {
            win.Dmsoft.SetMouseDelay(mode.ToString(), delay);
        }

        public void LeftClick() {
            win.Dmsoft.LeftClick();
        }

        public void LeftClick(int x, int y) {
            MoveTo(x, y);
            LeftClick();
        }

        public void LeftDoubleClick() {
            win.Dmsoft.LeftDoubleClick();
        }

        public void LeftDoubleClick(int x, int y) {
            MoveTo(x, y);
            LeftDoubleClick();
        }

        public void LeftDown() {
            win.Dmsoft.LeftDown();
        }

        public void LeftUp() {
            win.Dmsoft.LeftUp();
        }

        public void RightClick() {
            win.Dmsoft.RightClick();
        }

        public void RightClick(int x, int y) {
            MoveTo(x, y);
            RightClick();
        }

        public void RightDown() {
            win.Dmsoft.RightDown();
        }

        public void RightUp() {
            win.Dmsoft.RightUp();
        }

        public void MiddleClick() {
            win.Dmsoft.MiddleClick();
        }

        public void WheelDown() {
            win.Dmsoft.WheelDown();
        }

        public void WheelUp() {
            win.Dmsoft.WheelUp();
        }

        /// <summary>
        /// 以远点为坐标偏移过去
        /// </summary>
        /// <param name="offset_x"></param>
        /// <param name="offset_y"></param>
        public void MoveToDirection(int offset_x, int offset_y) {
            win.Dmsoft.MoveR(offset_x, offset_y);
        }

        /// <summary>
        /// 移动到客户区坐标点
        /// 注：不包括窗体框
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y) {
            win.Dmsoft.MoveTo(x, y);
        }

        /// <summary>
        /// 移动到这个区域内任意一点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void MoveToArea(int x, int y, int width, int height) {
            win.Dmsoft.MoveToEx(x, y, width, height);
        }
        
    }
}
