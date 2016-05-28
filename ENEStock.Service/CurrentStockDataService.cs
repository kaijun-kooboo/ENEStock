using ENEStock.Common;
using ENEStock.Models;
using ENEStock.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENEStock.Service
{
    public class CurrentStockDataService
    {
        /// <summary>
        /// 获取同花顺涨跌幅排名数据,每页50条数据,默认500条
        /// </summary>
        public List<ENEStock.Service.Models.CurrentStockData> GetCurrentTHSStockData(int dataCount = 500)
        {
            List<ENEStock.Service.Models.CurrentStockData> dataList = null;
            int page =(int)Math.Ceiling((double)dataCount / 50);
            StringBuilder thsZDFSB = new StringBuilder(); 
            string s = string.Empty;
            for (int i = 1; i <= page; i++)
            {
                if (dataList == null)
                {
                    dataList = new List<ENEStock.Service.Models.CurrentStockData>();
                }

                thsZDFSB.AppendFormat("http://q.10jqka.com.cn/interface/stock/fl/zdf/asc/{0}/hsa/quote", i);
                string jsonStr = ENEStock.Common.WebCatchHelper.CatchWebData(thsZDFSB.ToString());
                dynamic obj = JsonHelper.DeserializeJsonToObject<object>(jsonStr);
                List<ENEStock.Service.Models.CurrentStockData> stockList = JsonHelper.DeserializeJsonToList<ENEStock.Service.Models.CurrentStockData>(JsonHelper.SerializeObject(obj["data"]));
                StringBuilder stockCodeSB = new StringBuilder();
                stockList.ForEach(m => 
                {
                    stockCodeSB.Append(m.stockcode + ",");
                });
                stockCodeSB.Remove(stockCodeSB.Length - 1, 1);
                dataList.AddRange(stockList);
            }
            return dataList;
        }

        public List<StockDayData> GetStockDayList(DateTime beginDate, DateTime endDate, string stockCodeStr)
        {
            string jsonStr = WebCatchHelper.GetTLDataByApi(beginDate, endDate, stockCodeStr);
            dynamic obj = JsonHelper.DeserializeJsonToObject<object>(jsonStr);
            List<StockDayData> stockDayDataList = JsonHelper.DeserializeJsonToList<StockDayData>(JsonHelper.SerializeObject(obj["data"]));
            return stockDayDataList;
        }

    }
}
