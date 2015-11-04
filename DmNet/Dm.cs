using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmNet;

namespace DmNet
{
    /// <summary>
    /// 大漠对象单例工厂
    /// </summary>
    public class Dm
    {
        private static dmsoft dm;

        public static dmsoft Instance {
            get {
                return dm == null ? dm = new dmsoft() : dm;
            } 
        }
    }
}
