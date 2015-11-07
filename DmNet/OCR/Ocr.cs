using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmNet.Windows;
using System.Drawing;

namespace DmNet.OCR
{
    public class Ocr
    {
        public static readonly Ocr GlobalOcr = new Ocr(Window.Destop);
        
        private Window win;

        /// <summary>
        /// 绑定dm对象的构造函数
        /// </summary>
        /// <param name="dm"></param>
        public Ocr(Window win) {
            this.win = win;
        }


        /// <summary>
        /// 在屏幕范围(x1,y1,x2,y2)内,查找string(可以是任意个字符串的组合),并返回符合color_format的坐标位置,相似度sim同Ocr接口描述.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="text"></param>
        /// <param name="color_format"></param>
        /// <param name="sim"></param>
        /// <returns></returns>
        public Point FindStr(int x1,int y1,int x2,int y2,string text,string color_format,double sim=0.9){
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            win.Dmsoft.FindStr(x1, y1, x2, y2, text, color_format, sim,out x.Data,out y.Data);
            return new Point(x.Value, y.Value);
        }

        /// <summary>
        /// 全屏找字
        /// </summary>
        public Point FindStr(string text, string color_format, double sim = 0.9) {
            return FindStr(0,0,win.ClientSize.Width,win.ClientSize.Height, text, color_format, sim);
        }

        /// <summary>
        /// 区域找所有字
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="text"></param>
        /// <param name="color_format"></param>
        /// <param name="sim"></param>
        /// <returns></returns>
        public List<Point> FindAllStr(int x1, int y1, int x2, int y2, string text, string color_format, double sim = 0.9) {
            string result = win.Dmsoft.FindStrEx(x1, y1, x2, y2,text, color_format, sim);
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
        /// 全屏找所有字
        /// </summary>
        public List<Point> FindAllStr(string text, string color_format, double sim = 0.9) {
            return FindAllStr(0, 0, win.ClientSize.Width,win.ClientSize.Height, text, color_format, sim);
        }

        /// <summary>
        /// 快速模糊查找
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="text"></param>
        /// <param name="color_format"></param>
        /// <param name="sim"></param>
        /// <returns></returns>
        public Point FindStrFast(int x1, int y1, int x2, int y2, string text, string color_format, double sim = 0.9) {
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            win.Dmsoft.FindStrFast(x1, y1, x2, y2, text, color_format, sim, out x.Data, out y.Data);
            return new Point(x.Value, y.Value);
        }

        /// <summary>
        /// 全屏快速找字
        /// </summary>
        public Point FindStrFast(string text, string color_format, double sim = 0.9) {
            return FindStrFast(0,0,win.ClientSize.Width,win.ClientSize.Height, text, color_format, sim);
        }

        /// <summary>
        /// 区域快速找所有字
        /// </summary>
        public List<Point> FindAllStrFast(int x1, int y1, int x2, int y2, string text, string color_format, double sim = 0.9) {
            string result = win.Dmsoft.FindStrFastEx(x1, y1, x2, y2, text, color_format, sim);
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
        /// 全屏找所有字
        /// </summary>
        public List<Point> FindAllStrFast(string text, string color_format, double sim = 0.9) {
            return FindAllStrFast(0,0,win.ClientSize.Width,win.ClientSize.Height, text, color_format, sim);
        }

        /// <summary>
        /// 区域用系统字库查找
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="text"></param>
        /// <param name="color_format"></param>
        /// <param name="sim"></param>
        /// <param name="fontInfo"></param>
        /// <returns></returns>
        public Point FindStrWithFont(int x1,int y1,int x2,int y2,string text,string color_format,double sim,FontInfo fontInfo){
            COMParam<int> x = new COMParam<int>(0);
            COMParam<int> y = new COMParam<int>(0);
            win.Dmsoft.FindStrWithFont(x1, y1, x2, y2, text, color_format, sim, fontInfo.Name, fontInfo.Size, fontInfo.Flag,out x.Data,out y.Data);
            return new Point(x.Value, y.Value);
        }

        /// <summary>
        /// 全屏用系统字库找字
        /// </summary>
        public Point FindStrWithFont(string text, string color_format, double sim, FontInfo fontInfo) {
            return FindStrWithFont(0,0,win.ClientSize.Width,win.ClientSize.Height, text, color_format, sim, fontInfo);
        }

        /// <summary>
        /// 区域用系统字库找所有字
        /// </summary>
        public List<Point> FindAllStrWithFont(int x1, int y1, int x2, int y2, string text, string color_format, double sim, FontInfo fontInfo) {
            string result = win.Dmsoft.FindStrWithFontEx(x1, y1, x2, y2, text, color_format, sim, fontInfo.Name, fontInfo.Size, fontInfo.Flag);
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
        /// 区域用系统字库找所有字
        /// </summary>
        public List<Point> FindAllStrWithFont(string text, string color_format, double sim, FontInfo fontInfo) {
            return FindAllStrWithFont(0,0,win.ClientSize.Width,win.ClientSize.Height, text, color_format, sim, fontInfo);
        }

        /// <summary>
        /// 识别屏幕中的文字
        /// </summary>
        public string OcrScreen(int x1,int y1,int x2,int y2,string color_format,double sim){
            return win.Dmsoft.Ocr(x1, y1, x2, y2, color_format, sim);
        }
        public string OcrScreen(string color_format, double sim) {
            return win.Dmsoft.Ocr(0, 0, win.ClientSize.Width, win.ClientSize.Height, color_format, sim);
        }


        public void SetDict(int index, string fileName) {
            win.Dmsoft.SetDict(index, fileName);
        }

        public void UseDict(int index) {
            win.Dmsoft.UseDict(index);
        }


    }
}
