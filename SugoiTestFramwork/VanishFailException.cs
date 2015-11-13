using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    public class VanishFailException:Exception
    {
        public VanishFailException(string imgs):base(string.Format("{0} wasn't vanish.",imgs)) {

        }
    }
}
