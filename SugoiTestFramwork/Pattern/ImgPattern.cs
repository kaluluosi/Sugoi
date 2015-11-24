using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern {
    public class ImgPattern:PatternBase{
        private static string imgPath = "";
        private string deltaColor;
        private int direction;
        private string picName;

        public override Match Find(Region r) {
            string fileName = imgPath + picName;
            if(File.Exists(fileName) == false)
                throw new FileNotFoundException("file not found!", fileName);
            Image img = Image.FromFile(fileName);
            int h = img.Height;
            int w = img.Width;

            Point p = r.AppWin.IR.FindPic(r.X1, r.Y1, r.X2, r.Y2, fileName,deltaColor,Similar,direction);
            return new Match(r,this) { X1 = p.X, Y1 = p.Y, X2 = p.X + w, Y2 = p.Y + h };
        }

        public override List<Match> FindAll(Region r) {
            throw new NotImplementedException();
        }

        public static void SetImgPath(string path) {
            if(path.EndsWith(@"\") == false)
                path += @"\";
            imgPath = path;
        }
    }
}
