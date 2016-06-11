using System;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using ENEStock.Web.Common;
using System.Collections.Generic;

namespace ENEStock.Web.Controllers
{
    public class DataController : Controller
    {
        public ENEStock.Service.CurrentStockDataService service = new ENEStock.Service.CurrentStockDataService();
        //
        // GET: /Data/
        protected static string cookieHeader;

        public ActionResult Index()
        {
            //var rny = service.GetEntiy();
            //service.GetTLStockDayList(new DateTime(2016, 5, 24), new DateTime(2016, 5, 27), "600873,002466,603799,300405,600365,300352,002509,002771,600185,002460,002703,300037,002709,601958,002180,002070,300073,300340,300420,002057,300118,002787,600297,603026,300451,300493,600419,000762,300222,002426,300294,002537,002340,600337,300483,300014,300359,300100,002108,000078,300166,300364,300469,002231,600671,600570,600233,300494,300231,300386");
            //string host = "https://api.wmcloud.com/data/v1";
            //string url = "/api/market/getMktEqud.json?field=ticker,secShortName,closePrice,tradeDate,isOpen,accumAdjFactor&beginDate=20160520&endDate=20160526&ticker=600873,002466,603799,300405,600365,300352,002509,002771,600185,002460,002703,300037,002709,601958,002180,002070,300073,300340,300420,002057,300118,002787,600297,603026,300451,300493,600419,000762,300222,002426,300294,002537,002340,600337,300483,300014,300359,300100,002108,000078,300166,300364,300469,002231,600671,600570,600233,300494,300231,300386";
            //url = host + url;
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //string token = "ebd8925c8029dd5512f0d18cbd32ff308205b8b32cea98e9c69fd33f675e7d7d";//此处更换token
            //request.Headers["Authorization"] = "Bearer " + token;
            //request.Headers["Accept-Encoding"] = "gzip";//数据压缩传输
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string encoding = response.ContentEncoding;
            //string responseBody = string.Empty;
            //HttpsDeal.decoderesult(ref responseBody, response);
            ////Console.Write(responseBody);
            //response.Close();
            //return Content(responseBody);

            return View();
        }

        public ActionResult GetTestData()
        {
            List<Model.stockprice> stockpriceList = service.CatchEastMoneyRankData().Where(m=> m.istouch5 == true && m.isstop == false && m.isright == false).ToList();


            return Content(ENEStock.Common.JsonHelper.SerializeObject(stockpriceList));
        }

        public ActionResult GetEntity()
        {
            var rny =  service.GetEntiy();

            return Content("5");
        }

        public string TreeMenu()
        {
            var node = new ENEStock.Common.Format.TreeNode();
            node.Checked = true;
            node.text = "实时ENE";
            node.id = 1;
            return "[" + ENEStock.Common.JsonHelper.SerializeObject(node)  + "]";
        }
    }

}
