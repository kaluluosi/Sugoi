using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet.Window
{
    /// <summary>
    /// GetWindow 函数的操作项
    /// </summary>
    public enum Option
    {
        /// <summary>
        /// 获得父窗口
        /// </summary>
        Parent = 0,
        /// <summary>
        /// 获得第一个子窗口
        /// </summary>
        FirstChild = 1,
        /// <summary>
        /// 获得第一个窗口
        /// </summary>
        First = 2,
        /// <summary>
        /// 获得最后一个窗口
        /// </summary>
        Last = 3,
        /// <summary>
        /// 获得下一个窗口
        /// </summary>
        Next = 4,
        /// <summary>
        /// 获得上一个窗口
        /// </summary>
        Pre = 5,
        /// <summary>
        /// 获得持有者
        /// </summary>
        Owner = 6,
        /// <summary>
        /// 获得最顶部窗口
        /// </summary>
        Top = 7
    }
}
