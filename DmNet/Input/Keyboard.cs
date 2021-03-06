﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmNet;
using DmNet.Windows;

namespace DmNet.Input
{
    public class Keyboard
    {
        public static readonly Keyboard DestopKeyboard = new Keyboard(Window.Desktop);

        private Window win;

        /// <summary>
        /// 绑定dm对象的构造函数
        /// </summary>
        /// <param name="dm"></param>
        public Keyboard(Window win) {
            this.win = win;
        }

        /// <summary>
        /// 按键延迟
        /// </summary>
        public void SetKeyboardDelay(InputMode mode, int delay) {
            win.Dmsoft.SetKeypadDelay(mode.ToString(), delay);
        }

        public bool WaitKey(Keys key,int timeout=0) {
            int result = win.Dmsoft.WaitKey((int)key, timeout);
            return Convert.ToBoolean(result);
        }

        public bool WaitKey(string keyname, int timeout = 0) {
            return WaitKey(String2Keys(keyname), timeout);
        }

        public void KeyDown(int keycode) {
            win.Dmsoft.KeyDown(keycode);
        }

        /// <summary>
        /// 使用windows.forms的keys枚举来触发按键
        /// </summary>
        /// <param name="key"></param>
        public void KeyDown(Keys key) {
            win.Dmsoft.KeyDown((int)key);
        }

        public void KeyDown(string keyname) {
            win.Dmsoft.KeyDownChar(keyname);
        }

        public void KeyPress(int keycode) {
            win.Dmsoft.KeyPress(keycode);
        }

        /// <summary>
        /// 使用windows.forms的keys枚举来触发按键
        /// </summary>
        /// <param name="key"></param>
        public void KeyPress(Keys key) {
            win.Dmsoft.KeyPress((int)key);
        }

        public void KeyPress(string keyname) {
            win.Dmsoft.KeyPressChar(keyname);
        }

        public void KeyUp(int keycode) {
            win.Dmsoft.KeyPress(keycode);
        }

        /// <summary>
        /// 使用windows.forms的keys枚举来触发按键
        /// </summary>
        /// <param name="key"></param>
        public void KeyUp(Keys key) {
            win.Dmsoft.KeyUp((int)key);
        }

        public void KeyUp(string keyname) {
            win.Dmsoft.KeyUpChar(keyname);
        }

        public bool IsDown(int keycode) {
            return win.Dmsoft.GetKeyState(keycode) == 1;
        }

        /// <summary>
        /// 使用windows.forms的keys枚举来触发按键
        /// </summary>
        /// <param name="key"></param>
        public bool IsDown(Keys key) {
            return win.Dmsoft.GetKeyState((int)key)==1;
        }

        public bool IsDown(string keyname) {
            Keys k = String2Keys(keyname);
            return IsDown(k);
        }

        public bool IsUp(int keycode) {
            return win.Dmsoft.GetKeyState(keycode) == 0;
        }

        /// <summary>
        /// 使用windows.forms的keys枚举来触发按键
        /// </summary>
        /// <param name="key"></param>
        public bool IsUp(Keys key) {
            return win.Dmsoft.GetKeyState((int)key)==0;
        }

        public bool IsUp(string keyname) {
            Keys k = String2Keys(keyname);
            return IsUp(k);
        }


        public static Keys String2Keys(string keyname) {
            return (Keys)Enum.Parse(typeof(Keys), keyname);
        }
    }
}
