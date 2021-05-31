using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceProviderServiceOpted
    {
        public int ServiceProviderServiceOptedId { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string SubCategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string ServiceCode { get; set; }


    }
}
