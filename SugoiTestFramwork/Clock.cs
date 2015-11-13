using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork
{
    public class Clock
    {
        private static DateTime start;

        public static void Start() {
            start = DateTime.Now;
        }

        public static double Tick() {
            TimeSpan span = DateTime.Now - start;
            return span.TotalMilliseconds;
        }

    }
}
