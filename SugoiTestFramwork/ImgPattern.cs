using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DmNet.ImageRecognition;
using System.IO;

namespace SugoiTestFramwork
{
    public class ImgPattern
    {
        private string delta = "000000";
        private string picName;
        public ImgPattern(string picName){
            this.picName = picName;
            Image img = Image.FromFile(Sugoi.ScriptPath + picName);
            Height = img.Height;
            Width = img.Width;
        }

        private double similar = 0.6f;
        private int direction = 0;
        private int offset_X = 0;
        private int offset_Y = 0;

        //有效识别区域默认（0，0，0，0）全屏识别
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public int Offset_X {
            get {
                if(offset_X == 0)
                    return Width / 2;
                return offset_X;
            }
            set {
                offset_X = value;
            }
        }
        public int Offset_Y {
            get {
                if(offset_Y == 0)
                    return Height / 2;
                return offset_Y;
            }
            set {
                offset_Y = value;
            }
        }

        public double Similar {
            get {
                return similar;
            }
            set {
                similar = value;
            }
        }

        public int Direction {
            get {
                return direction;
            }
            set {
                direction = value;
            }
        }

        //是否是全屏识别
        public bool IsFullScreen {
            get {
                return X1 == 0 && X2 == 0 && Y1 == 0 && Y2 == 0;
            }
        }


        public ImgPattern Region(int x1, int y1, int x2, int y2) {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            return this;
        }

        public ImgPattern SetSimilar(double sim) {
            Similar = sim;
            return this;
        }

        public ImgPattern SetDirection(int direction) {
            Direction = direction;
            return this;
        }

        public ImgPattern SetCenterOffset(int x, int y) {
            Offset_X = x;
            Offset_Y = y;
            return this;
        }

        public ImgPattern SetOffset(int x, int y) {
            offset_X = x;
            offset_Y = y;
            return this;
        }


        public string PicName{
            get {
                return picName;
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

        public ImgPattern SetDelta(string delta) {
            Delta = delta;
            return this;
        }

    }
}
