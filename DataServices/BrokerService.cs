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

        public MethodOutput<Broker> GetAllBrokers(int BrokerId, string BrokerType, string SourceName, string CRNumber, string MobileNo, string EmailId)
        {
            OpBroker obj = new OpBroker();
            return obj.GetAllBroker(BrokerId,BrokerType, SourceName,  CRNumber,  MobileNo,  EmailId);
        }

        public MethodOutput<string> DeleteBroker(int BrokerId)
        {
            OpBroker obj = new OpBroker();
            return obj.DeleteBroker(BrokerId);
        }
        public MethodOutput<string> SaveBrokerPrice(BrokerPrice objBrokerPrice)
        {
            OpBrokerPrice obj = new OpBrokerPrice();
            return obj.SaveBrokerPrice(objBrokerPrice);
        }

        public MethodOutput<BrokerPrice> GetBrokerPriceList(int BrokerId, int ServiceId)
        {
            OpBrokerPrice opBrokerPrice = new OpBrokerPrice();
            return opBrokerPrice.GetBrokerPriceList(BrokerId,ServiceId);
        }



    }
}
