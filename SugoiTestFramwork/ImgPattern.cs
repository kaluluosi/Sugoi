using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    public class ImgPattern:Pattern
    {
        private string imgs;
        private string delta = "000000";

        public ImgPattern(string imgs) {
            this.imgs = imgs;
        }

        public string Images{
            get {
                return imgs;
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
