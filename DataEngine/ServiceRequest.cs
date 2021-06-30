using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
   public class ServiceRequest
    {
     public Int64   ServiceRequestId                       {get;set;}
     public int   CustomerId                             {get;set;}
     public int   ServiceId                              {get;set;}
     public int   ServiceSubCategoryId                   {get;set;}
     public string   VehicleRegistrationNumber              {get;set;}
     public string   ChessisNo                              {get;set;}
     public string   ServiceLocation                        {get;set;}
     public int   CountryID                              {get;set;}
     public int   GovernotesId                           {get;set;}
     public int   PlaceId                                {get;set;}
     public string   LocationPinCode                        {get;set;}
     public DateTime   DateOfAccident                         {get;set;}
     public string   NameOfWorkShop                         {get;set;}
     public string   CarHandledWorkShopDate                 {get;set;}
     public DateTime   EstimatedRepairCompletedDate           {get;set;}
     public string   BuildingNo                             {get;set;}
     public int   ServiceRequestStatus                   {get;set;}
     public DateTime   ServiceRequestedDate                   {get;set;}
     public DateTime   ServiceCompletedDate                   {get;set;}
     public string   ServiceFeedBack                        {get;set;}
     public DateTime   CreateDate                             {get;set;}
     public DateTime   ModifiedDate                           {get;set;}
     public int   ModifiedBy                             {get;set;}
     public int   SequenceNo                             {get;set;}
     public string   TicketNo                               {get;set;}
     public int   CreatedBy                              {get;set;}
     public bool StepiniCondtion                            {get;set;}
      public string CollectRepairVehicleAddress             {get;set;}
     public string ContactMobileNo                          {get;set;}
      public string ReleventDetails                         {get;set;}
       public string Remarks { get; set; }
    }
}
