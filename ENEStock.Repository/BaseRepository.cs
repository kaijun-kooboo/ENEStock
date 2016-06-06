using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using ENEStock.Model;

namespace ENEStock.Repository
{
    public class BaseRespository<T> : IRepository.IBaseRepository<T>
        where T:class,new()
    {
        //从线程中获取 EF容器
        StockEntities db = EFFactory.GetEFContext();

        #region 1.0 新增 方法 +int Add(T model)
        /// <summary>
        /// 1.0 新增 方法
        /// </summary>
        /// <param name="model">新增实体</param>
        /// <returns></returns>
        public int Add(T model)
        {
            db.Set<T>().Add(model);
            return 0;
        }
        #endregion

        #region 2.1 删除指定实体 +int Delete(T model)
        /// <summary>
        /// 2.1 删除指定实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(T model)
        {
            var dbQuery = db.Set<T>();
            dbQuery.Attach(model);
            dbQuery.Remove(model);
            return 0;
        }
        #endregion

        #region 2.2 根据指定条件 删除 +int DeleteBy(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 2.2 根据指定条件 删除
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns></returns>
        public int DeleteBy(Expression<Func<T, bool>> whereLambda)
        {
            var dbQuery = db.Set<T>();
            //先查询 对应表的 集合
            var list = dbQuery.Where(whereLambda).ToList();
            //遍历集合 里要删除的元素
            foreach (var item in list)
            {
                //标记为 删除状态
                dbQuery.Remove(item);
            }
            return 0;
        }
        #endregion

        #region 3.1 修改指定实体 +int Modify(T model)
        /// <summary>
        /// 3.1 修改指定实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modifiedPropertyNames">修改值的属性名称</param>
        /// <returns></returns>
        public int Modify(T model, params string[] modifiedPropertyNames)
        {
            //1.获取 代理 对象里的 标识管理器
            var dbEntry = db.Entry<T>(model);
            //2.设置状态为 未修改状态
            dbEntry.State = System.Data.EntityState.Unchanged;
            //3.遍历 修改值属性名 数组，并设置为 修改状态
            foreach (string pName in modifiedPropertyNames)
            {
                dbEntry.Property(pName).IsModified = true;
            }
            return 0;
        }
        #endregion

        #region 3.2 根据指定条件 修改 +int ModifyBy(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 2.3 根据指定条件 修改
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="propertyName">要修改的属性名</param>
        /// <param name="perpertyValues">属性要修改的值</param>
        /// <returns></returns>
        public int ModifyBy(Expression<Func<T, bool>> whereLambda, string[] propertyNames, object[] perpertyValues)
        {
            //1.查询要修改的对象集合
            var list = db.Set<T>().Where(whereLambda).ToList();
            //2.获取 要修改对象 的类型属性
            Type t = typeof(T);
            //3.循环 要修改的 实体对象，并根据 要修改的属性名 修改对象对应的属性值
            foreach (var item in list)
            {
                //循环 要修改的属性 名称， 并 反射取出 t 中的 属性对象
                for (int index = 0; index < propertyNames.Length;index++ )
                {
                    //获取要修改的属性名
                    string pName = propertyNames[index];
                    //获取属性对象
                    PropertyInfo pi = t.GetProperty(pName);
                    //调用属性对象的 SetValue方法 为当前循环的 item对象 对应的属性赋值
                    pi.SetValue(item, perpertyValues[index], null);
                }
            }
            return 0;
        }
        #endregion

        #region 4.1 根据条件查询 +IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 4.1 根据条件查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda != null)
                return db.Set<T>().Where(whereLambda);
            else
                return db.Set<T>();
        }
        #endregion

        #region 4.2 根据条件 进行 连接 查询 +IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames);
        /// <summary>
        /// 4.2 根据条件 进行 连接 查询
        /// </summary>
        /// <param name="whereLambda">查询条件 - 可以为 null </param>
        /// <param name="foreignKeyPropertyNames">要连接查询的 外键属性名</param>
        /// <returns></returns>
        public IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames)
        {
            //1.获取普通查询对象(返回的是 DBSet 对象，但继承了 DbQuery，所以可以用父类变量接收)
            DbQuery<T> dbQuery = db.Set<T>();
            //2.将剩下的 要连接查询的 外键属性 通过 dbQuery对象 Include 进来
            foreach (string foreignKey in foreignKeyPropertyNames)
            {
                dbQuery = dbQuery.Include(foreignKey);
            }
            if (whereLambda != null)
                return dbQuery.Where(whereLambda);
            else
                return dbQuery;
        }
        #endregion

        #region 4.3 根据条件查询 +IEnumerable<T> WhereOrderBy(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 4.3 根据条件查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IEnumerable<T> WhereOrderBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true)
        {
            if (isAsc) return db.Set<T>().Where(whereLambda).OrderBy(orderBy);
            else return db.Set<T>().Where(whereLambda).OrderByDescending(orderBy);
        }
        #endregion

        #region 4.4 根据条件 进行 连接 排序查询 +IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames);
        /// <summary>
        /// 4.3 根据条件 进行 连接 查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="foreignKeyPropertyNames">要连接查询的 外键属性名</param>
        /// <returns></returns>
        public IEnumerable<T> WhereOrderBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true, params string[] foreignKeyPropertyNames)
        {
            //1.获取普通查询对象
            DbQuery<T> dbQuery = db.Set<T>();
            //2.将剩下的 要连接查询的 外键属性 通过 dbQuery对象 Include 进来
            foreach (string foreignKey in foreignKeyPropertyNames)
            {
                dbQuery = dbQuery.Include(foreignKey);
            }
            return isAsc ? dbQuery.OrderBy(orderBy).Where(whereLambda) : dbQuery.OrderByDescending(orderBy).Where(whereLambda);
        }
        #endregion

        #region 4.5 分页 查询 +IEnumerable<T> WherePaged<TKey>
        /// <summary>
        /// 4.5 分页 查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pagedData">分数据对象,查询后 包含 所有分页后的数据</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="foreignKeyPropertyNames">需要连接查询的外键属性名数组</param>
        /// <returns></returns>
        //public IEnumerable<T> WherePaged<TKey>(MODEL.FormatMODEL.PagedData pagedData, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true, params string[] foreignKeyPropertyNames)
        //{
        //    DbQuery<T> dbQuery = db.Set<T>();
        //    //1.将 要连接查询的 外键属性 通过 dbQuery对象 Include 进来
        //    foreach (string foreignKey in foreignKeyPropertyNames)
        //    {
        //        dbQuery = dbQuery.Include(foreignKey);
        //    }
        //    //2.排序
        //    IOrderedQueryable<T> iorderQuery = null;
        //    if (isAsc) iorderQuery = dbQuery.OrderBy(orderLambda);
        //    else iorderQuery = dbQuery.OrderByDescending(orderLambda);
        //    //3.根据页码和页容量查询分页数据
        //    pagedData.rows = iorderQuery.Where(whereLambda).Skip((pagedData.PageIndex - 1) * pagedData.PageSize).Take(pagedData.PageSize);
        //    //4.查询总行数
        //    pagedData.total = iorderQuery.Where(whereLambda).Count();
        //    return pagedData.rows as IEnumerable<T>;
        //} 
        #endregion

        #region 5.0 执行 增删改 语句 nt ExcuteSql(string strSql, params System.Data.SqlClient.SqlParameter[] paras)
        /// <summary>
        /// 执行 增删改 语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int ExcuteSql(string strSql, params System.Data.SqlClient.SqlParameter[] paras)
        {
            return db.Database.ExecuteSqlCommand(strSql, paras);
        } 
        #endregion
    }
}
