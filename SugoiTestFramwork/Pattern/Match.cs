using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern
{
    public class Match:Region
    {
        public Match(Region parent,PatternBase pattern):base(parent.AppWin) {
            this.Pattern = pattern;
            this.Parent = parent;
        }

        public PatternBase Pattern { get; set; }

        public override Point Pivot {
            get {
                if (Pattern.TargetOffset == Point.Empty)
                    return base.Pivot;
                return new Point(X1 + Pattern.TargetOffset.X, Y1 + Pattern.TargetOffset.Y);
            }
        }
    }
}
