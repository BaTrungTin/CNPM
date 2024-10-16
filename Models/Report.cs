using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOI.Core.Models
{
    public class Report : FeedBack
    {
        public int Id { get; set; } // id báo cáo
        public DateTime DateTimeReport { get; set; } // thời gian báo cáo


    }
}
