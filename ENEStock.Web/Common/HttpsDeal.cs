using System.Net;
using System.IO;
using System.IO.Compression;

namespace ENEStock.Web.Common
{
    public class HttpsDeal
    {
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

