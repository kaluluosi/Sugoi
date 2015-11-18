using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DmNet.ImageRecognition;

namespace SugoiTestFramwork
{
    public class ImgPattern:Pattern
    {
        private string picNames;
        private string delta = "000000";

        public ImgPattern(string imgs) {
            this.picNames = imgs;
            Image img = Image.FromFile(Sugoi.ImgPath+imgs);
            Height = img.Height;
            Width = img.Width;
        }

        public string PicNames{
            get {
                return picNames;
            }
        }

        /// <summary>
        /// 偏色值
        /// </summary>
        public string Delta {
            get {
                return delta;
            }
            set {
                delta = value;
            }
        }

        public void SetDelta(string delta) {
            Delta = delta;
        }
    }
}
