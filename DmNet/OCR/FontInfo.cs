using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet.OCR
{
    public class FontInfo
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
        public bool Underline { get; set; }
        public bool DeleteLine { get; set; }

        public int Flag {
            get {
                int flag = 0;
                if(Bold) 
                    flag += 1;
                if(Italic) 
                    flag += 2;
                if(Underline)
                    flag += 4;
                if(DeleteLine)
                    flag += 8;

                return flag;
            }
        }
    }
}
