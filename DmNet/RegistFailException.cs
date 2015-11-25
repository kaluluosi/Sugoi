using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet
{
    public class RegistFailException:Exception
    {
        public RegistFailException():base("dm.dll regist fail") {
            
        }
    }
}
