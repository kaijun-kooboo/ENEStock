using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENEStock.Service.Models
{
    public class StockDayData
    {
        //股票交易代码
        public string ticker { set; get; }
        //名称
        public string secShortName { set; get; }
        //今日收盘价
        public double closePrice { set; get; }
        //交易日期
        public DateTime tradeDate { set; get; }
        //是否开盘
        public bool isOpen { set; get; }
        //复权因子, 未除权为1
        public double accumAdjFactor { set; get; }
    }
}
