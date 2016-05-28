using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENEStock.Service.Models
{
    public class CurrentStockData
    {
        //所属板块
        public string stockid { set; get; }

        //股票名称
        public string stockname { set; get; }

        //股票代码
        public string stockcode { set; get; }

        //最新价
        public double zxj { set; get; }

        //涨跌额
        public double zde { set; get; }

        //涨跌幅
        public double zdf { set; get; }

        //昨日收
        public double zs { set; get; }

        //开盘价
        public double jk { set; get; }

        //最高价
        public double zgj { set; get; }

        //最低价
        public float zdj { set; get; }

        // 成交量/手
        public int cjl { set; get; }

        //成交额
        public double cje { set; get; }

        //资金净流入
        public double jlr { set; get; }

        //换手率
        public double hsl { set; get; }

        //最新时间
        public DateTime rtime { set; get; }
    }
}