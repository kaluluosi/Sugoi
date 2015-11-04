using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmNet.Window
{
    /// <summary>
    /// 窗口过滤选项，可互相组合
    /// </summary>
    [Flags]
    public enum FilterOption
    {
        Default = 0,
        /// <summary>
        /// 匹配窗口标题,参数title有效
        /// </summary>
        Title = 1,
        /// <summary>
        /// 匹配窗口类名,参数class_name有效.
        /// </summary>
        Name = 2,
        /// <summary>
        /// 只匹配指定父窗口的第一层孩子窗口
        /// </summary>
        FirstChild = 4,
        /// <summary>
        /// 匹配所有者窗口为0的窗口,即顶级窗口
        /// </summary>
        Top = 8,
        /// <summary>
        /// 匹配可见的窗口
        /// </summary>
        Visible = 16,
        /// <summary>
        /// 匹配出的窗口按照窗口打开顺序依次排列 收费
        /// </summary>
        Order = 32
    }
}
