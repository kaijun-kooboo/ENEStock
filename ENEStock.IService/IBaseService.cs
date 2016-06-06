using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace ENEStock.IService
{
    /// <summary>
    /// 业务父接口
    /// </summary>
    /// <typeparam name="T">要操作的【实体类】 类型</typeparam>
    public interface IBaseService<T>
        where T : class,new()
    {
        #region 1.0 新增 方法 +int Add(T model)
        /// <summary>
        /// 1.0 新增 方法
        /// </summary>
        /// <param name="model">新增实体</param>
        /// <returns></returns>
        int Add(T model); 
        #endregion

        #region 2.1 删除指定实体 +int Delete(T model)
        /// <summary>
        /// 2.1 删除指定实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(T model); 
        #endregion

        #region 2.2 根据指定条件 删除 +int DeleteBy(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 2.2 根据指定条件 删除
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns></returns>
        int DeleteBy(Expression<Func<T, bool>> whereLambda); 
        #endregion

        #region 3.1 修改指定实体 +int Modify(T model)
        /// <summary>
        /// 3.1 修改指定实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modifiedPropertyNames">修改值的属性名称</param>
        /// <returns></returns>
        int Modify(T model,params string[] modifiedPropertyNames); 
        #endregion

        #region 3.2 根据指定条件 修改 +int ModifyBy(Expression<Func<T, bool>> whereLambda,string [] propertyNames,object [] perpertyValues)
        /// <summary>
        /// 2.3 根据指定条件 修改
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="propertyName">要修改的属性名</param>
        /// <param name="perpertyValues">属性要修改的值</param>
        /// <returns></returns>
        int ModifyBy(Expression<Func<T, bool>> whereLambda,string [] propertyNames,object [] perpertyValues);
        #endregion

        #region 4.1 根据条件查询 +IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 4.1 根据条件查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda); 
        #endregion

        #region 4.2 根据条件 进行 连接 查询 +IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames);
        /// <summary>
        /// 4.2 根据条件 进行 连接 查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="foreignKeyPropertyNames">要连接查询的 外键属性名</param>
        /// <returns></returns>
        IEnumerable<T> Where(Expression<Func<T, bool>> whereLambda, params string[] foreignKeyPropertyNames);
        #endregion

        #region 4.3 根据条件查询 +IEnumerable<T> WhereOrderBy(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 4.3 根据条件查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IEnumerable<T> WhereOrderBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true);
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
        IEnumerable<T> WhereOrderBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true, params string[] foreignKeyPropertyNames);
        #endregion

        #region 4.5 分页 查询 +IEnumerable<T> WherePaged<TKey>
        /// <summary>
        /// 4.5 分页 查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pagedData">分数据对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="foreignKeyPropertyNames">需要连接查询的外键属性名数组</param>
        /// <returns></returns>
        //IEnumerable<T> WherePaged<TKey>(MODEL.FormatMODEL.PagedData pagedData, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true, params string[] foreignKeyPropertyNames); 
        #endregion
    }
}
