using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
   public  class City
    {
        public int CityId { get; set; }

        public string StateId { get; set; }


        public string Name { get; set; }

        public string CityCode { get; set; }

        public string PinCode { get; set; }

        public bool IsActive { get; set; }
    }
}
