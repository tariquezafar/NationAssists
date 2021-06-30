using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class CustomerService
    {
        public MethodOutput<string> SaveCustomer(Customer objCustomer)
        {
            OpCustomer obj = new OpCustomer();
            return obj.SaveCustomer(objCustomer);
        }

        public MethodOutput<string> UpdateCustomerEmailVerificationStatus(string CustomerCode)
        {
            OpCustomer obj = new OpCustomer();
            return obj.UpdateCustomerEmailVerificationStatus(CustomerCode);
        }
    }
}
