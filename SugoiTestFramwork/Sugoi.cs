using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmNet.Windows;
using DmNet.ImageRecognition;
using System.Drawing;

namespace SugoiTestFramwork
{
    /// <summary>
    /// Sugoi测试框架运行对象
    /// </summary>
    public class Sugoi
    {
        /// <summary>
        /// 测试的软件的窗口对象
        /// </summary>
        public Window AppWin { get; set; }

        public bool Exists(ImgPattern imgPtn) {
            Point p;
            if(imgPtn.IsFullScreen){
                p = AppWin.IR.FindPic(imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            else {
                p = AppWin.IR.FindPic(imgPtn.X1,imgPtn.Y1,imgPtn.X2,imgPtn.Y2,imgPtn.Images, imgPtn.Delta, imgPtn.Similar, imgPtn.Direction);
            }
            return IR.PointExist(p);
        }

        public bool Exists(string imgs) {
            ImgPattern imgPtn = new ImgPattern(imgs);
            bool result = Exists(imgPtn);
            return result;
        }
    }
}
