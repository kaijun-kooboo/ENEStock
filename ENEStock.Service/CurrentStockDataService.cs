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
    public class CurrentStockDataService : BaseService<Model.stockdetail>
    {
        /// <summary>
        /// 获取同花顺涨跌幅排名数据,每页50条数据,默认500条
        /// </summary>
        public List<ENEStock.Service.Models.CurrentStockData> GetCurrentTHSStockData(int dataCount = 500)
        {
            List<ENEStock.Service.Models.CurrentStockData> dataList = null;
            int page = (int)Math.Ceiling((double)dataCount / 50);
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

        /// <summary>
        /// 抓取通联数据
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="stockCodeStr"></param>
        /// <returns></returns>
        public List<StockDayData> GetTLStockDayList(DateTime beginDate, DateTime endDate, string stockCodeStr)
        {
            string jsonStr = WebCatchHelper.GetTLDataByApi(beginDate, endDate, stockCodeStr);
            dynamic obj = JsonHelper.DeserializeJsonToObject<object>(jsonStr);
            List<StockDayData> stockDayDataList = JsonHelper.DeserializeJsonToList<StockDayData>(JsonHelper.SerializeObject(obj["data"]));
            return stockDayDataList;
        }

        /// <summary>
        /// 抓取东方财富数据
        /// </summary>
        /// <returns></returns>
        public List<Model.stockprice> CatchEastMoneyRankData()
        {
            List<Model.stockprice> stockpriceList = null;
            string orginData = WebCatchHelper.GetEastMoneyData();
            string[] orginDataArr = orginData.Split(new string[2] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
            var eastMoneyArr = orginDataArr[1].Split(new char[2] { '"', '"' }, StringSplitOptions.RemoveEmptyEntries).Where(m => m != ",").ToList();

            if (eastMoneyArr != null && eastMoneyArr.Count > 0)
            {
                stockpriceList = new List<Model.stockprice>();
            }
            else
            {
                return null;
            }

            eastMoneyArr.ForEach(m =>
            {
                var itemArr = m.Split(',');
                Model.stockprice model = new Model.stockprice();
                model.id = System.Guid.NewGuid().ToString();
                model.stockcode = itemArr[1];
                model.codename = itemArr[2];
                model.lastprice = Convert.ToDouble(itemArr[3]);
                model.openprice = Convert.ToDouble(itemArr[4]);
                model.newprice = Convert.ToDouble(itemArr[5]);
                model.highestprice = Convert.ToDouble(itemArr[6]);
                model.lowestprice = Convert.ToDouble(itemArr[7]);
                model.turnover = Convert.ToInt32(itemArr[8]);
                model.tradingvolume = Convert.ToInt32(itemArr[9]);
                model.changeamount = Convert.ToDouble(itemArr[10]);
                model.changerate = Convert.ToString(itemArr[11]);
                model.amplitude = Convert.ToString(itemArr[13]);
                model.tradetime = Convert.ToDateTime(itemArr[28]);

                stockpriceList.Add(model);
            });

            Init(new DateTime(2016, 6, 1), new DateTime(2016, 6, 6), stockpriceList);

            //init ENE
            stockpriceList.ForEach(m =>
            {
                if (m.ma5 != null)
                {
                    m.eneupper = Convert.ToDouble(Math.Round((decimal)(1.11 * m.ma5), 2));
                    m.enelower = Convert.ToDouble(Math.Round((decimal)( 0.91 * m.ma5), 2));
                    m.enemiddle = (m.eneupper + m.enelower) / 2;
                    m.enemiddle = Convert.ToDouble(Math.Round((decimal)(m.enemiddle), 2));
                    if (m.enelower >= m.lowestprice)
                    {
                        m.istouch5 = true;
                    }
                    else
                    {
                        m.istouch5 = false;
                    }
                }
            });

            return stockpriceList;
        }

        public List<Model.stockprice> Init(DateTime beginDate, DateTime endDate, List<Model.stockprice> stockpriceList)
        {
            int pageIndex = 1;
            int pageSize = 50;
            int totalPage = (int)Math.Ceiling((double)stockpriceList.Count / (double)pageSize);
            for (int i = pageIndex; i <= totalPage; i++)
            {
                var tempData = stockpriceList.Skip((i - 1) * pageSize).Take(pageSize).ToList();
                StringBuilder sb = new StringBuilder();
                tempData.ForEach(m =>
                {
                    sb.Append(m.stockcode + ",");
                });

                sb.Remove(sb.Length - 1, 1);

                var tlStockDayList = GetTLStockDayList(beginDate, endDate, sb.ToString());

                tempData.ForEach(m =>
                {
                    var entity = stockpriceList.Where(n => n.stockcode == m.stockcode).FirstOrDefault();

                    if (tlStockDayList.Where(n => n.ticker == m.stockcode && n.isOpen == false).FirstOrDefault() != null)
                    {
                        entity.isstop = true;
                    }
                    else
                    {
                        entity.isstop = false;
                    }

                    if (tlStockDayList.Where(n => n.ticker == m.stockcode && n.accumAdjFactor != 1.0).FirstOrDefault() != null)
                    {
                        entity.isright = true;
                    }
                    else
                    {
                        entity.isright = false;
                    }

                    entity.ma5 = (tlStockDayList.Where(n => n.ticker == m.stockcode).Sum(x => x.closePrice) + m.newprice) / (double)5;

                    entity.ma5 =Convert.ToDouble(Math.Round((decimal)entity.ma5, 2));
                });
            }



            return stockpriceList;
        }


        public Model.stockdetail GetEntiy()
        {
            var entity = baseDAl.Where(m => m.id != "").FirstOrDefault();
            return entity;
        }
    }
}
