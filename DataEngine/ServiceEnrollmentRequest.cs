using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceEnrollmentRequest
    {
        public string RequestedDate;

        public int ServiceEnrollmentRequestId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleManufacturer { get; set; }
        public string ModelType { get; set; }
        public int YearOfManufacture { get; set; }
        public int YearOfConstruction { get; set; }
        public int ServiceEnrollmentStatus { get; set; }
        public string Status { get; set; }
        public string ChessisNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByUser { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? GovernotesId { get; set; }
        public string GovernotesName { get; set; }
        public int? PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockCode { get; set; }
        public int NoOfLocation { get; set; }
        public string RiskAddress { get; set; }
        public string Remarks { get; set; }
        public string VehicleBody { get; set; }
        public string ApproverRemarks { get; set; }

        public string TypeOfService { get; set; }
        public string ServiceCode { get; set; }

        public string Branch { get; set; }

        public string PolicyType { get; set; }

        public int ApprovedBy { get; set; }

        public string CPRNumber { get; set; }

        public string CustomerEmail { get; set; }
    }

}


public class ServiceEnrollmentStatus
{
    public int ServiceEnrollmentStatusId { get; set; }

    public string Status_Name { get; set; }


}

