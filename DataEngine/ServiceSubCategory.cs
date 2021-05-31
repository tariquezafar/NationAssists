using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceSubCategory
    {
        public int ServiceSubCategoryId { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public string ServiceName { get; set; }

        public string ServiceCode { get; set; }
    }
}
