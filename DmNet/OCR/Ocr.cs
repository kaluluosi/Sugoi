using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmNet.Windows;

namespace DmNet.OCR
{
    public class Ocr
    {
        
        private Window win;

        /// <summary>
        /// 桌面键盘
        /// </summary>
        public Ocr()
            : this(new Window()) {

        }

        /// <summary>
        /// 绑定dm对象的构造函数
        /// </summary>
        /// <param name="dm"></param>
        public Ocr(Window win) {
            this.win = win;
        }
    }
}
