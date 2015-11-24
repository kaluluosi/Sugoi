using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern
{
    public interface IOperable
    {
        public void Click(Region r);
        public void DoubleClick(Region r);
        public void RightClick(Region r);
    }
}
