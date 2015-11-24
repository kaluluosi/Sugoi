using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern
{
    public class Match:Region,IOperable
    {
        private PatternBase pattern;

        public Match(Region r,PatternBase pattern):base(r.AppWin) {
            this.pattern = pattern;
        }

        public PatternBase Pattern {
            get { return pattern; }
            set { pattern = value; }
        }

        public Point GetTarget() {
            Point p = new Point(X1 + pattern.TargetOffset.X, Y1 + pattern.TargetOffset.Y);
            return p;
        }

        void IOperable.Click(Region r) {
            r.AppWin.Mouse.MoveTo(GetTarget().X, GetTarget().Y);
            r.AppWin.Mouse.LeftClick();
        }
    }
}
