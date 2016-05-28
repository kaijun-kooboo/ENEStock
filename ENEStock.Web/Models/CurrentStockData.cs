using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENEStock.Web.Models
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
        public float zxj { set; get; }

        //涨跌额
        public float zde { set; get; }

        //涨跌幅
        public float zdf { set; get; }

        //昨日收
        public float zs { set; get; }

        //开盘价
        public float jk { set; get; }

        //最高价
        public float zgj { set; get; }

        //最低价
        public float zdj { set; get; }

        // 成交量/手
        public int cjl { set; get; }

        //成交额
        public float cje { set; get; }

        //资金净流入
        public float jlr { set; get; }

        //换手率
        public float hsl { set; get; }

        //最新时间
        public DateTime rtime { set; get; }
    }
}