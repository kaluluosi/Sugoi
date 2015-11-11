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
        public FindFailException(string images,string message):base(message){
            this.Images = images;
        }
    }
}
