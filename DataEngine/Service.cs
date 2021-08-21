using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class Service
    {
      public int       ServiceId    {get;set;}
      public string         ServiceCode  {get;set;}
      public string         ServiceName  {get;set;}
      public bool IsActive { get; set; }

        public decimal Price { get; set; }
        public string PackageCode { get; set; }
    }
}
