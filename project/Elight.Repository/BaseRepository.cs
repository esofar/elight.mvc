using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    /// <summary>
    /// 数据访问层父类。
    /// </summary>
    public partial class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DbContext _db = null;
        public DbContext Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new DbContext();
                }
                return _db;
            }
        }
        public bool Exists(object primaryKey)
        {
            return Db.Exists<TEntity>(primaryKey);
        }
        public TEntity Get(object primaryKey)
        {
            return Db.SingleOrDefault<TEntity>(primaryKey);
        }
        public object Insert(TEntity model)
        {
            return Db.Insert(model);
        }
        public int Delete(object primaryKey)
        {
            return Db.Delete<TEntity>(primaryKey);
        }
        public int Delete(TEntity model)
        {
            return Db.Delete(model);
        }
        public int Update(TEntity model)
        {
            return Db.Update(model);
        }
        public int Update(TEntity model, IEnumerable<string> columns)
        {
            return Db.Update(model, columns);
        }
    }
}
