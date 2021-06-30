using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
   public class SearchCPROutput
    {
        public bool IsValidCPR { get; set; }

        public bool IsCPRMatchesWithSource { get; set; }
        public string ErrorMessage { get; set; }
    }
}
