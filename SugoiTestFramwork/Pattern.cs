using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    /// <summary>
    /// 模式匹配类 基类
    /// 用来记录模式匹配需要的各种参数
    /// 为了能让python使用，函数做了些调整。
    /// </summary>
    public class Pattern
    {
        private double similar = 0.9;
        private int direction = 0;

        //有效识别区域默认（0，0，0，0）全屏识别
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public int Offset_X { get; set; }
        public int Offset_Y { get; set; }

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

        //全屏识别
        public bool IsFullScreen {
            get {
                return X1 == 0 && X2 == 0 && Y1 == 0 && Y2 == 0;
            }
        }


        public Pattern Region(int x1, int y1, int x2, int y2) {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            return this;
        }

        public Pattern SetSimilar(double sim) {
            Similar = sim;
            return this;
        }

        public Pattern SetDirection(int direction) {
            Direction = direction;
            return this;
        }

        public Pattern SetCenterOffset(int x, int y) {
            Offset_X = x;
            Offset_Y = y;
            return this;
        }
    }
}
