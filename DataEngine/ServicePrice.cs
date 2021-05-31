using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServicePrice
    {
        public Int64 ServicePriceId { get; set; }

        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        public decimal Prices { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
