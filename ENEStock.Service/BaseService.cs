using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace ENEStock.Service
{
    /// <summary>
    /// 业务层父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IService.IBaseService<T>
        where T:class,new()
    {
        public BaseService()
        {
            baseDAl = new ENEStock.Repository.BaseRespository<T>();
            //SetBaseDal();
        }

        //需要一个数据 父接口 变量，来提供 基本 增删改查 方法
        protected IRepository.IBaseRepository<T> baseDAl = null;

        //提供给子类 为 父类 的 baseDAl 赋值
        // public abstract void SetBaseDal();

        #region 数据仓储属性：从当前 处理线程中 获取 +IRespository.IDBSession DBSession
        /// <summary>
        /// 数据仓储属性：从当前 处理线程中 获取
        /// </summary>
        //protected IRepository.IDBSession DBSession
        //{
        //    get
        //    {
        //        return DBSessionFactory.GetDBSession();
        //    }
        //} 
        #endregion

        #region 1.0 新增 方法 +int Add(T model)
        /// <summary>
        /// 1.0 新增 方法
        /// </summary>
        /// <param name="model">新增实体</param>
        /// <returns></returns>
        public int Add(T model)
        {
            return baseDAl.Add(model);
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
            return baseDAl.Delete(model);
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
            return baseDAl.DeleteBy(whereLambda);
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
            return baseDAl.Modify(model, modifiedPropertyNames);
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
            return baseDAl.ModifyBy(whereLambda, propertyNames, perpertyValues);
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
            return baseDAl.Where(whereLambda);
        }
        #endregion

        #region 4.2 根据条件 进行 连接 查询 +IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames);
        /// <summary>
        /// 4.2 根据条件 进行 连接 查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="foreignKeyPropertyNames">要连接查询的 外键属性名</param>
        /// <returns></returns>
        public IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames)
        {
            return baseDAl.Where(whereLambda, foreignKeyPropertyNames);
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
            return baseDAl.WhereOrderBy(whereLambda, orderBy, isAsc);
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
            return baseDAl.WhereOrderBy(whereLambda, orderBy, isAsc, foreignKeyPropertyNames);
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
        //    return baseDAl.WherePaged<TKey>(pagedData, whereLambda, orderLambda, isAsc, foreignKeyPropertyNames);
        //} 
        #endregion
    }
}
