using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
  public  class DashboardService
    {
        public MethodOutput<DashboardModel> GetDashBoardData()
        {
            OpDashboard objdas = new OpDashboard();
            return objdas.GetDashBoardData();
        }
    }
}
