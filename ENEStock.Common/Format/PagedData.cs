using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENEStock.Common.Format
{
    public class PagedData
    {
        public int PageIndex;
        public int PageSize;
        public int total;// RowCount; 是为 EasyUI的datagrid组件 而改
        public object rows;//PagedList; 是为 EasyUI的datagrid组件 而改

        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(Convert.ToDouble(total) / Convert.ToDouble(PageSize));
            }
        }
    }
}
