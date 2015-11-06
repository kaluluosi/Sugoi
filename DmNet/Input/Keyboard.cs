using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmNet;

namespace DmNet.Input
{
    public class Keyboard
    {
        private dmsoft dm = Dm.Default;

        public Keyboard() {
            
        }

        /// <summary>
        /// 绑定dm对象的构造函数
        /// </summary>
        /// <param name="dm"></param>
        public Keyboard(dmsoft dm) {
            this.dm = dm;
        }

        public void KeyDown(Keys key) {
            dm.KeyDown((int)key);
        }
    }
}
