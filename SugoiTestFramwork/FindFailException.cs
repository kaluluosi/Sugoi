using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    public class FindFailException:Exception
    {
        public string  Images { get; set; }
        public FindFailException(string images):base(string.Format("can't find {0}",images)){
            this.Images = images;
        }
    }
}
