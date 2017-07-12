using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.Entity
{
    /// <summary>
    /// PetaPoco数据操作扩展方法。
    /// </summary>
    public partial class Database : IDatabase
    {
        #region operation: PageJoin

        /// <summary>
        /// 双表分页连接查询。
        /// </summary>
        /// <typeparam name="TMain">主表</typeparam>
        /// <typeparam name="T1">从表</typeparam>
        /// <typeparam name="TRet">结果表</typeparam>
        /// <param name="cb">外键属性装填函数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="sql">完整的连接查询语句（禁止出现重复列）</param>
        /// <returns></returns>
        public Page<TRet> PageJoin<TMain, T1, TRet>(Func<TMain, T1, TRet> cb, long pageIndex, long pageSize, Sql sql)
        {
            return PageJoin<TMain, T1, TRet>(cb, pageIndex, pageSize, sql.SQL, sql.Arguments);
        }
        public Page<TRet> PageJoin<TMain, T1, TRet>(Func<TMain, T1, TRet> cb, long pageIndex, long pageSize, string sql, params object[] args)
        {
            string sqlCount = string.Empty;
            string sqlPage = string.Empty;
            BuildPageQueries<TMain>((pageIndex - 1) * pageSize, pageSize, sql, ref args, out sqlCount, out sqlPage);
            return PageJoin<TMain, T1, TRet>(cb, pageIndex, pageSize, sqlCount, args, sqlPage, args);
        }
        public Page<TRet> PageJoin<TMain, T1, TRet>(Func<TMain, T1, TRet> cb, long pageIndex, long pageSize, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs)
        {
            // Save the one-time command time out and use it for both queries
            var saveTimeout = OneTimeCommandTimeout;

            // Setup the paged result
            var result = new Page<TRet>
            {
                CurrentPage = pageIndex,
                ItemsPerPage = pageSize,
                TotalItems = ExecuteScalar<long>(sqlCount, countArgs)
            };

            result.TotalPages = result.TotalItems / pageSize;
            if ((result.TotalItems % pageSize) != 0)
            {
                result.TotalPages++;
            }

            OneTimeCommandTimeout = saveTimeout;
            // Get the records
            result.Items = Fetch<TMain, T1, TRet>(cb, sqlPage, pageArgs);
            // Done
            return result;
        }

        /// <summary>
        /// 三表分页连接查询。
        /// </summary>
        /// <typeparam name="TMain">主表</typeparam>
        /// <typeparam name="T1">从表1</typeparam>
        /// <typeparam name="T2">从表2</typeparam>
        /// <typeparam name="TRet">结果表</typeparam>
        /// <param name="cb">外键属性装填函数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="sql">完整的连接查询语句（禁止出现重复列）</param>
        /// <returns></returns>
        public Page<TRet> PageJoin<TMain, T1, T2, TRet>(Func<TMain, T1, T2, TRet> cb, long pageIndex, long pageSize, Sql sql)
        {
            return PageJoin<TMain, T1, T2, TRet>(cb, pageIndex, pageSize, sql.SQL, sql.Arguments);
        }
        public Page<TRet> PageJoin<TMain, T1, T2, TRet>(Func<TMain, T1, T2, TRet> cb, long pageIndex, long pageSize, string sql, params object[] args)
        {
            string sqlCount = string.Empty;
            string sqlPage = string.Empty;
            BuildPageQueries<TMain>((pageIndex - 1) * pageSize, pageSize, sql, ref args, out sqlCount, out sqlPage);
            return PageJoin<TMain, T1, T2, TRet>(cb, pageIndex, pageSize, sqlCount, args, sqlPage, args);
        }
        public Page<TRet> PageJoin<TMain, T1, T2, TRet>(Func<TMain, T1, T2, TRet> cb, long pageIndex, long pageSize, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs)
        {
            // Save the one-time command time out and use it for both queries
            var saveTimeout = OneTimeCommandTimeout;

            // Setup the paged result
            var result = new Page<TRet>
            {
                CurrentPage = pageIndex,
                ItemsPerPage = pageSize,
                TotalItems = ExecuteScalar<long>(sqlCount, countArgs)
            };

            result.TotalPages = result.TotalItems / pageSize;
            if ((result.TotalItems % pageSize) != 0)
            {
                result.TotalPages++;
            }

            OneTimeCommandTimeout = saveTimeout;
            // Get the records
            result.Items = Fetch<TMain, T1, T2, TRet>(cb, sqlPage, pageArgs);
            // Done
            return result;
        }

        #endregion

        #region operation: Fill

        /// <summary>
        /// 填充DataTable。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public DataTable FillDataTable(string sql, params object[] args)
        {
            OpenSharedConnection();
            try
            {
                DataTable dt = new DataTable();
                using (var cmd = CreateCommand(_sharedConnection, sql, args))
                {
                    using (DbDataAdapter dbDataAdapter = _factory.CreateDataAdapter())
                    {
                        dbDataAdapter.SelectCommand = (DbCommand)cmd;
                        dbDataAdapter.Fill(dt);
                    }
                }
                return dt;
            }
            finally
            {
                CloseSharedConnection();
            }
        }

        /// <summary>
        /// 填充DataTable。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataTable FillDataTable(Sql sql)
        {
            return FillDataTable(sql.SQL, sql.Arguments);
        }

        /// <summary>
        /// 填充DataSet。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public DataSet FillDataSet(string sql, params object[] args)
        {
            OpenSharedConnection();
            try
            {
                DataSet ds = new DataSet();
                using (var cmd = CreateCommand(_sharedConnection, sql, args))
                {
                    using (DbDataAdapter dbDataAdapter = _factory.CreateDataAdapter())
                    {
                        dbDataAdapter.SelectCommand = (DbCommand)cmd;
                        dbDataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            finally
            {
                CloseSharedConnection();
            }
        }

        /// <summary>
        /// 填充DataSet。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataSet FillDataSet(Sql sql)
        {
            return FillDataSet(sql.SQL, sql.Arguments);
        }

        #endregion
    }
}
