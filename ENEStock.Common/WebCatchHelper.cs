using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ENEStock.Common
{
    public class WebCatchHelper
    {
        public static string CatchWebData(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "get";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseStr = string.Empty;
            using (StreamReader ms = new StreamReader(stream))
            {
                responseStr = ms.ReadToEnd();
            }

            return responseStr;
        }

        public static string GetTLDataByApi(DateTime beginDate, DateTime endDate, string stockCodeStr)
        {
            string host = "https://api.wmcloud.com/data/v1";
            // string url = "/api/market/getStockFactorsOneDay.json?field=ticker,tradeDate,pe,pb,MA5,MA10&secID=&ticker=603688&tradeDate=20160527";//000001日线数据
            //string url = "/api/market/getMktEqud.json?field=ticker,secShortName,closePrice,tradeDate,isOpen,accumAdjFactor&beginDate=20160520&endDate=20160526&ticker=600873,002466,603799,300405,600365,300352,002509,002771,600185,002460,002703,300037,002709,601958,002180,002070,300073,300340,300420,002057,300118,002787,600297,603026,300451,300493,600419,000762,300222,002426,300294,002537,002340,600337,300483,300014,300359,300100,002108,000078,300166,300364,300469,002231,600671,600570,600233,300494,300231,300386";
            StringBuilder urlSB = new StringBuilder();
            urlSB.AppendFormat("/api/market/getMktEqud.json?field=ticker,secShortName,closePrice,tradeDate,isOpen,accumAdjFactor&beginDate={0}&endDate={1}&ticker={2}", beginDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), stockCodeStr);
            string url = host + urlSB.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string token = "ebd8925c8029dd5512f0d18cbd32ff308205b8b32cea98e9c69fd33f675e7d7d";//此处更换token
            request.Headers["Authorization"] = "Bearer " + token;
            request.Headers["Accept-Encoding"] = "gzip";//数据压缩传输
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            string responseBody = string.Empty;
            decoderesult(ref responseBody, response);
            response.Close();
            return responseBody;
        }

        public static void decoderesult(ref string result, HttpWebResponse response)
        {
            using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
            {
                using (StreamReader StreamReaderreader = new StreamReader(stream))
                {
                    result = StreamReaderreader.ReadToEnd();
                }
            }
        }
    }
}
