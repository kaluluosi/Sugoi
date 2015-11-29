using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramework.Test
{
    public class VanishFailException:Exception
    {
        public VanishFailException(string imgs):base(string.Format("{0} hasn't vanish.",imgs)) {

        }
    }
}
