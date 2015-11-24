using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern {
    public abstract class PatternBase:IOperable{
        public float Similar { get; set; }
        public Point TargetOffset { get; set; }

        public PatternBase SetSimilar(float sim) {
            this.Similar = sim;
            return this;
        }

        public PatternBase SetOffset(int x, int y) {
            TargetOffset = new Point(x, y);
            return this;
        }

        public abstract Match Find(Region r);
        public abstract List<Match> FindAll(Region r);


        public void Click(Region r) {
            throw new NotImplementedException();
        }

        public void DoubleClick(Region r) {
            throw new NotImplementedException();
        }

        public void RightClick(Region r) {
            throw new NotImplementedException();
        }
    }
}
