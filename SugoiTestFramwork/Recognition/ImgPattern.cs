using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SugoiTestFramework.Recognition {
    public class ImgPattern:PatternBase{
        private static string defaultImgPath = "";
        private string deltaColor="000000";
        private int direction=0;
        private string picName;

        public string FileName {
            get {
                return defaultImgPath + picName;
            }
        }

        public int ImgHeight { get; set; }
        public int ImgWidth { get; set; }
        public int Direction {
            get {
                return direction;
            }

            set {
                //这里要做一个检查 direction只能是[0-3]范围，如果超了应该抛个错
                if (value < 0 || value > 3) throw new ArgumentOutOfRangeException(string.Format("Direction must be in [0-3],now is {0}." + value));
                direction = value;
            }
        }

        public string DeltaColor {
            get {
                return deltaColor;
            }

            set {
                //这里要做一个检查 color必须是8个字符串长度而且字符必须是0-F
                bool isHex = Regex.IsMatch(value, "^[0-9A-Fa-f]+$");
                if (isHex == false) throw new ArgumentException(string.Format("{0} is not Hex", value));
                deltaColor = value;
            }
        }

        public ImgPattern(string picName) {
            this.picName = picName;
            if (File.Exists(FileName) == false)
                throw new FileNotFoundException("File not found!", FileName);
            Image img = Image.FromFile(FileName);
            ImgHeight = img.Height;
            ImgWidth = img.Width;
        }

        public override Match Find(Region r) {
            Point p = r.AppWin.IR.FindPic(r.X1, r.Y1, r.X2, r.Y2, FileName,deltaColor,Similar,direction);
            //这样这个match就引用了imgptn和父区域r，通过imgptn的偏移值可以计算出目标坐标
            if (DmNet.ImageRecognition.IR.PointExist(p) == false) return null;
            return new Match(r,this) { X1 = p.X, Y1 = p.Y, X2 = p.X + ImgWidth, Y2 = p.Y + ImgHeight};
        }

        public override List<Match> FindAll(Region r) {
            var matchs = from p in r.AppWin.IR.FindAllPic(r.X1, r.Y1, r.X2, r.Y2, FileName, deltaColor, Similar, direction)
                                 select new Match(r, this) { X1 = p.X, Y1 = p.Y, X2 = p.X + ImgWidth, Y2 = p.Y + ImgHeight };
            return matchs.ToList();
        }

        public override string ToString() {
            return string.Format("[Img:{0} Sim:{1}]", picName, Similar);
        }

        public static void SetDefaultImgPath(string path) {
            if(path.EndsWith(@"\") == false)
                path += @"\";
            defaultImgPath = path;
        }
    }
}
