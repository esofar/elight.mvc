using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    /// <summary>
    /// 数据访问层父接口。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 记录是否存在。
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        bool Exists(object primaryKey);
        /// <summary>
        /// 查询一条记录。
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        TEntity Get(object primaryKey);
        /// <summary>
        /// 新增一条记录。
        /// </summary>
        /// <param name="model">新增对象</param>
        /// <returns></returns>
        object Insert(TEntity model);
        /// <summary>
        /// 删除一条记录。
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        int Delete(object primaryKey);
        /// <summary>
        /// 删除一条记录。
        /// </summary>
        /// <param name="model">删除对象</param>
        /// <returns></returns>
        int Delete(TEntity model);
        /// <summary>
        /// 更新一条记录。
        /// </summary>
        /// <param name="model">修改对象</param>
        /// <returns></returns>
        int Update(TEntity model);
        /// <summary>
        /// 更新一条记录。
        /// </summary>
        /// <param name="model">修改对象</param>
        /// <param name="columns">修改指定属性（列）</param>
        /// <returns></returns>
        int Update(TEntity model, IEnumerable<string> columns);
    }
}
