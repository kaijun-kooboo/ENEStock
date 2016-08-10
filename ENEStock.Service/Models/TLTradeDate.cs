using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENEStock.Service.Models
{
    public class TLTradeDateDto
    {
        public string exchangeCD { set; get; }

        public string calendarDate { set; get; }

        public DateTime CalendarDate
        {
            set 
            {
                CalendarDate = Convert.ToDateTime(calendarDate);
            }
            get
            {
                return Convert.ToDateTime(calendarDate);
            }
        }

        public bool isOpen { set; get; }

        public string prevTradeDate { set; get; }

        public DateTime PrevTradeDate
        {
            set
            {
                CalendarDate = Convert.ToDateTime(prevTradeDate);
            }
            get
            {
                return Convert.ToDateTime(prevTradeDate);
            }
        }

        public bool isWeekEnd { set; get; }

        public bool isMonthEnd { set; get; }

        public bool isQuarterEnd { set; get; }

        public bool isYearEnd { set; get; }
    }
}
