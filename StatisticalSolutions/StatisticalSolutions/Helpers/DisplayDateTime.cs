using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace StatisticalSolutions.Helpers
{
    public class DisplayDateTime
    {
        private DateTime date;
        private string shortDate;
        private string longDate; 

        public DisplayDateTime(DateTime datetime)
        {
            date = datetime;
            shortDate = datetime.ToString("d");
            longDate = datetime.ToString("dddd, MMMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public string ShortDate
        {
            get
            {
                return shortDate;
            }

        }

        public string LongDate
        {
            get
            {
                return longDate;
            }

        }


        
    }
}