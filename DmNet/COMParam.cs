using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet
{
    public class COMParam<T>
    {
        public COMParam(T value){
            Value = value;
        }

        /// <summary>
        /// 获取原来的值
        /// </summary>
        public T Value {
            get {
                return (T)Data;
            }
            set {
                Data = value;
            }
        }

        /// <summary>
        /// 转换为obj
        /// </summary>
        public object Data;

    }
}
