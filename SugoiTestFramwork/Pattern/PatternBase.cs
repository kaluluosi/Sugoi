using DmNet.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugoiTestFramwork.Pattern {
    public abstract class PatternBase{
        private float similar = 0.7f;

        public float Similar {
            get {
                return similar;
            }

            set {
                similar = value;
            }
        }

        public Point TargetOffset { get; set; }

        /// <summary>
        /// 找点
        /// </summary>
        /// <param name="r">负责找这个模式的区域对象</param>
        /// <returns>返回匹配对象</returns>
        public abstract Match Find(Region r);
        /// <summary>
        /// 找匹配到的所有点
        /// </summary>
        /// <param name="r">负责找这个模式的区域对象</param>
        /// <returns>返回匹配对象集合</returns>
        public abstract List<Match> FindAll(Region r);
    }
}
