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
    public class Mouse
    {
        private Window win;

        /// <summary>
        /// 桌面鼠标
        /// </summary>
        public Mouse():this(new Window()) {

        }

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

        public void LeftDoubleClick() {
            win.Dmsoft.LeftDoubleClick();
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


        public void MoveToDirection(int offset_x, int offset_y) {
            win.Dmsoft.MoveR(offset_x, offset_y);
        }

        public void MoveTo(int x, int y) {
            win.Dmsoft.MoveTo(x, y);
        }

        public void MoveToArea(int x, int y, int width, int height) {
            win.Dmsoft.MoveToEx(x, y, width, height);
        }

    }
}
