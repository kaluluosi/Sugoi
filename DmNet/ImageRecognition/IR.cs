﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmNet.Windows;
using System.Drawing;

namespace DmNet.ImageRecognition
{
    /// <summary>
    /// 图色识别 组件
    /// </summary>
    public class IR
    {
        private Window win;

        /// <summary>
        /// 桌面键盘
        /// </summary>
        public IR()
            : this(new Window()) {

        }

        /// <summary>
        /// 绑定dm对象的构造函数
        /// </summary>
        /// <param name="dm"></param>
        public IR(Window win) {
            this.win = win;
        }

        public bool Capture(int x1, int y1, int x2, int y2, string fileName, string imgType = "bmp") {
            int result = 0;
            switch(imgType.ToLower()) {
                case "png":
                    result = win.Dmsoft.CapturePng(x1, y1, x2, y2, fileName);
                    break;
                case "jpg":
                    result = win.Dmsoft.CaptureJpg(x1, y1, x2, y2, fileName, 100);
                    break;
                case "bmp":
                default:
                    result = win.Dmsoft.Capture(x2, y1, x2, y2, fileName);
                    break;
            }
            return Convert.ToBoolean(result);
        }
        /// <summary>
        /// 查找一个像素的颜色 查找指定区域内符合颜色的像素点返回第一个坐标
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// <param name="sim"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Point FindColor(int x1, int y1, int x2, int y2, string color, double sim = 0.9, int dir = 0) {
            COMParam<int> intX = new COMParam<int>(0);
            COMParam<int> intY = new COMParam<int>(1);
            win.Dmsoft.FindColor(x1, y1, x2, y2, color, sim, dir, out intX.Data, out intY.Data);
            return new Point(intX.Value, intX.Value);
        }

        /// <summary>
        /// 查找指定区域内的所有符合颜色的像素点，返回所有坐标
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// <param name="sim"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public List<Point> FindAllColor(int x1, int y1, int x2, int y2, string color, double sim = 0.9, int dir = 0) {
            string result = win.Dmsoft.FindColorEx(x1, y1, x2, y2, color, sim, dir);
            int count = win.Dmsoft.GetResultCount(result);
            List<Point> points = new List<Point>();
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            for(int i = 0; i < count; i++) {
                win.Dmsoft.GetResultPos(result, i, out x.Data, out y.Data);
                points.Add(new Point(x.Value, y.Value));
            }
            return points;
        }

        /// <summary>
        /// 多点查找颜色坐标，从左上角第一个点为远点查找其他偏移点的颜色，返回匹配率最高的坐标
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="first_color"></param>
        /// <param name="offset_color"></param>
        /// <param name="sim"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Point FindMultiColor(int x1,int y1,int x2,int y2,string first_color,string offset_color,double sim=0.9,int dir=0){
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            win.Dmsoft.FindMultiColor(x1, y1, x2, y2, first_color, offset_color, sim, dir, out x.Data, out y.Data);
            return new Point(x.Value, y.Value);
        }

        /// <summary>
        /// 多点查找颜色坐标,从左上角第一个点为远点查找其他偏移点的颜色，返回所有匹配上的坐标
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="first_color"></param>
        /// <param name="offset_color"></param>
        /// <param name="sim"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public List<Point> FindMultiColorEx(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim = 0.9, int dir = 0) {
            string result = win.Dmsoft.FindMultiColorEx(x1, y1, x2, y2, first_color, offset_color, sim, dir);
            int count = win.Dmsoft.GetResultCount(result);
            List<Point> points = new List<Point>();
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            for(int i = 0; i < count; i++) {
                win.Dmsoft.GetResultPos(result, i, out x.Data, out y.Data);
                points.Add(new Point(x.Value, y.Value));
            }
            return points;
        }

        /// <summary>
        /// 查找指定区域内的图片,位图必须是24位色格式,支持透明色,当图像上下左右4个顶点的颜色一样时,则这个颜色将作为透明色处理.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="pic_name"></param>
        /// <param name="delta_color"></param>
        /// <param name="sim"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Point FindPic(int x1,int y1,int x2,int y2,string pic_name,string delta_color,double sim=0.9,int dir=0){
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            win.Dmsoft.FindPic(x1, y1, x2, y2, pic_name, delta_color, sim, dir, out x.Data, out y.Data);
            return new Point(x.Value, y.Value);
        }

        /// <summary>
        /// 查找指定区域内的图片,位图必须是24位色格式,支持透明色,当图像上下左右4个顶点的颜色一样时,则这个颜色将作为透明色处理.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="pic_name"></param>
        /// <param name="delta_color"></param>
        /// <param name="sim"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public List<Point> FindAllPic(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim = 0.9, int dir = 0) {
            string result = win.Dmsoft.FindPicEx(x1, y1, x2, y2, pic_name, delta_color, sim, dir);
            int count = win.Dmsoft.GetResultCount(result);
            List<Point> points = new List<Point>();
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            for(int i = 0; i < count; i++) {
                win.Dmsoft.GetResultPos(result, i, out x.Data, out y.Data);
                points.Add(new Point(x.Value, y.Value));
            }
            return points;
        }
    }
}
