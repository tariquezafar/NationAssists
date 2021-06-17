using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
   public class  MethodOutput<T>
    {
        public int RowAffected { get; set; }

        public string ErrorMessage { get; set; }

        public List<T> DataList { get; set; }

        public T Data { get; set; }
    }
}
