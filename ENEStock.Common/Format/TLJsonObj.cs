using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENEStock.Common.Format
{
    public class TLJsonObj
    {
        public string retCode { set; get; }

        public string retMsg { set; get; }

        public object data { set; get; }
    }
}
