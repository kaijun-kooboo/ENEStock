using ENEStock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ENEStock.Repository
{
    public static class EFFactory
    {
        [Obsolete("此方法不推荐使用，请使用 GetEFContext() 方法")]
        public static object GetDataContext()
        {
            object db = CallContext.GetData("EFContext");
            if (db == null)
            {
                var efDB = new ENEStock.Model.StockEntities();
                //关闭 根据 配置文件 里的 属性是否可以为空的 验证
                efDB.Configuration.ValidateOnSaveEnabled = false;
                db = efDB;
                CallContext.SetData("EFContext", db);
            }
            return db;
        }

        /// <summary>
        /// 从线程中 获取 EF容器对象
        /// </summary>
        /// <returns></returns>
        public static StockEntities GetEFContext()
        {
            return GetDataContext() as StockEntities;
        }
    }
}
