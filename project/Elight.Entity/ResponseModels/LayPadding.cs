using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elight.Entity
{
    /// <summary>
    /// Laytpl + Laypage 分页模型。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LayPadding<TEntity> where TEntity : class
    {
        public int code { get; set; }

        /// <summary>
        /// 获取结果。
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 备注信息。
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 数据列表。
        /// </summary>
        public List<TEntity> list { get; set; }

        /// <summary>
        /// 记录条数。
        /// </summary>
        public long count { get; set; }
    }
}