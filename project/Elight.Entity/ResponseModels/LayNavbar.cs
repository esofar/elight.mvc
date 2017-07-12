using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.Entity
{

    /// <summary>
    /// 菜单视图模型。
    /// </summary>
    public class LayNavbar
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        ///  图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        ///  是否展开
        /// </summary>
        public bool spread { get; set; }
        /// <summary>
        /// 子级菜单集合
        /// </summary>
        public List<LayChildNavbar> children { get; set; }
    }

    /// <summary>
    /// 子级菜单模型。
    /// </summary>
    public class LayChildNavbar
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string href { get; set; }
    }
}
