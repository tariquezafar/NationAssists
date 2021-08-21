using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class Membership
    {
        public Int64 MembershipId { get; set; }
        public int SourceId { get; set; }
        public int PackageId { get; set; }
        public string Branch { get; set; }
        public string PolicyType { get; set; }
        public string PolicyNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime ? EffectiveDate { get; set; }
        public DateTime ? ExpiryDate { get; set; }
        public string InsuredName { get; set; }
        public string CPRNumber { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string ChassisNo { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleType { get; set; }
        public string VehicleBody { get; set; }
        public int ? VehicleYear { get; set; }
        public bool VehicleReplacement { get; set; }
        public string RiskAddress { get; set; }
        public int ? NoOfLocation { get; set; }
        public string TypeOfService { get; set; }
        public int ? CreatedBy { get; set; }
        public DateTime ? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string PackageName { get; set; }
        public string SourceName { get; set; }

        public string CreatedByUser { get; set; }

        public string ModifiedByUser { get; set; }

        public string SourceType { get; set; }

        public string MembershipReferenceNo { get; set; }

    }

    public class ExcelData
    {
        public string SheetId { get; set; }
        public string SheetName { get; set; }

        public DataTable dt { get; set; }

        public string ErrorMessage { get; set; }

        public List<string> MandatoryColumns { get; set; }

        public List<string> Columns { get; set; }
    }

    public class ExcelValidationResponse
    {
        public int SourceId { get; set; }

        public string SourceName { get; set; }
        public string ErrorMessage { get; set; }

        public List<ExcelData> ExcelData { get; set; }


    }

    public class ExcelUploaderResponse
    {
        public string sheetName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
