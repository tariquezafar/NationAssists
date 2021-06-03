using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
   public class BrokerService
    {

        public MethodOutput<string> SaveBroker(Broker Brokers)
        {
            OpBroker obj = new OpBroker();
            return obj.SaveBroker(Brokers);
        }

        public MethodOutput<Broker> GetAllBrokers(int BrokerId, string BrokerType)
        {
            OpBroker obj = new OpBroker();
            return obj.GetAllBroker(BrokerId,BrokerType);
        }

        public MethodOutput<string> DeleteBroker(int BrokerId)
        {
            OpBroker obj = new OpBroker();
            return obj.DeleteBroker(BrokerId);
        }

    }
}
