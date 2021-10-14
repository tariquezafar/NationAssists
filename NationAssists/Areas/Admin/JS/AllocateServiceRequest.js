function ShowServiceRequestDetail(e) {
    var hhh = "";
    $("#dvDetail").hide();
    $("#hdnServiceRequestId").val(e.ServiceRequestId);
    $("#txtServiceName").val(e.ServiceName);
    $("#txtServiceType").val(e.SubCategoryName);
    $("#txtVehicleRegistrationNumber").val(e.VehicleRegistrationNumber);
    $("#txtChessisNo").val(e.ChessisNo);
    $("#txtServiceLocation").val(e.ServiceLocation);
    $("#txtCountryName").val(e.CountryName);
    $("#txtGovernotesName").val(e.GovernoratesName);
    $("#txtPlaceName").val(e.Place_Name);
    $("#txtBlockCode").val(e.Block_Code);
    $("#txtDateOfAccident").val(e.DateOfAccident);
    $("#txtNameOfWorkshop").val(e.NameOfWorkShop);
    $("#txtBuildingNo").val(e.BuildingNo);
    $("input[type=radio][name=StepiniCondtion][value=" + e.StepiniCondtion + "]").prop("checked", true);
    $("#txtCollectRepairVehicleAddress").val(e.CollectRepairVehicleAddress);
    $("#txtCustContactNo").val(e.ContactMobileNo);
    $("#txtReleventDetails").val(e.ReleventDetails);
    $("#dvServiceRequestDetail").show();
    $("#hdnServiceId").val(e.ServiceId);
    $("#hdnSubCategoryId").val(e.ServiceSubCategoryId);
    $("#hdnServiceAllocationId").val(e.ServiceAllocationId);
    $("#txtRemarks").val(e.Remarks);
    $("#txtCustName").val(e.Customer_Name);
    $("#txtCustomerEmailId").val(e.EmailId);


    
    var SelectedService = e.ServiceName;
    var ServiceCode = SelectedService.substring(SelectedService.indexOf('(') + 1, SelectedService.indexOf(')'));
    if (ServiceCode == "HA") {
        $('.RAP').hide();
        $('.HAP').show();

    }
    else {
        $('.RAP').show();
        $('.HAP').hide();
    }
    var scrollPos = $("#dvServiceRequestDetail").offset().top;
    $(window).scrollTop(scrollPos);

    if (e.ServiceRequestId != null && e.ServiceId != null && e.ServiceSubCategoryId != null && e.CountryID != null && e.GovernotesId != null && e.PlaceId != null && e.LocationPinCode != null) {
        
        var pUrl = "/Admin/ServiceRequest/GetAllServiceProviderForServiceAllocation?BlockId=" + e.LocationPinCode + "&PlaceId=" + e.PlaceId + "&GovernotesId=" + e.GovernotesId + "&CountryId=" + e.CountryID + "&SubCategoryId=" + e.ServiceSubCategoryId + "&BrokerId=" + e.BrokerId + "&ServiceId=" + e.ServiceId;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

               
                var jData = JSON.parse(data);
    

                if (jData != null && jData.length > 0) {
                    var table = "<table> <thead> <tr> <th></th> <th>SNo.</th> <th>Service Provider Code</th> <th>Name</th>  <th>Contact Person Name</th> <th>Mobile No </th> <th>Email id</th> <th>Price</th>   </tr> </thead>";

                        table += "<tbody>";
                    jData.forEach(function (e, i) {
                            var tbody = "<tr>";
                        tbody += "<td> <input type='radio' name='chkProvider' value=" + e.ServiceProviderId + " price=" + e.ServicePrice + " PriceId=" + e.ServicePriceId+" />  </td>";
                            tbody += "<td>" + (i + 1) + "</td>";
                        tbody += "<td>" + e.ServiceProviderCode + "  </td>";
                        tbody += "<td>" + e.ServiceProviderName + " </td>";
                        tbody += "<td>" + e.ContactPersonName + "</td>";
                        tbody += "<td>" + e.MobileNo + " </td>";
                        tbody += "<td>" + e.EmailId + " </td>";
                        tbody += "<td>" + e.ServicePrice + "</td>";
                        
                           
                            tbody += "</tr>";

                            table += tbody;

                        });

                    $('#tblProviderList').html(table);
                    }
                    else {
                    $('#tblProviderList').html('');
                    }


               
            }
        });
    }


    $("#tblServiceRequest").hide();
}

function SaveServiceAllocation() {

    if (ValidateServiceAllocation()) {

        var objServiceAllocationReq = {
            ServiceAllocationId:  $("#hdnServiceAllocationId").val(),
            ServiceProviderId: $("input[name='chkProvider']:checked").val(),
            ServiceRequestId: $("#hdnServiceRequestId").val(),
            ServiceId: $("#hdnServiceId").val(),  
            ServiceSubCategoryId: $("#hdnSubCategoryId").val(),  
            ServiceAllocationStatus: 1,
            ServicePrice: $("input[name='chkProvider']:checked").attr("price"),//PriceId
            ServicePriceId: $("input[name='chkProvider']:checked").attr("PriceId"),
            AllocationRemarks: $("#txtAllocationRemarks").val()


        };

        var pUrl = "/Admin/ServiceRequest/SaveServiceAllocation/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objServiceAllocationReq),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Service Request is successfully allocated");
                    window.location.href = "/admin/ServiceRequest/AllocateServiceRequest/";
                }
                else {
                    alert("Opps! some error occured.");
                }

            },
            error: function (data) {
            }
        });
    }
    
}

function ValidateServiceAllocation() {

    var IsValid = true;
    var strErrMsg = "";
    if ($("input[name='chkProvider']").length == 0) {
        IsValid = false;
        
        alert("Please map Service Provider.");
    }
    if ($("input[name='chkProvider']").length > 0 && $("input[name='chkProvider']:checked").length == 0) {
        IsValid = false;
        alert("Please select any Service Provider to allocate service.");
    }
    if ($("input[name='chkProvider']:checked").length > 0 && $("#txtAllocationRemarks").val() == "") {
        IsValid = false;
        alert("Please enter Service Allocation Remarks");
    }
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
       
    }

   
    
}

function SearchAllocateServiceRequest() {

    var TicketNo = $("#txtTicketNo").val();
    var CustomerName = $("#txtCustomerName").val();
    var ContactNo = $("#txtContactNo").val();
    var EmailId = $("#txtEmailId").val();
  
    var pUrl = "/Admin/ServiceRequest/ShowServiceRequestList?TicketNo=" + TicketNo + "&CustomerName=" + CustomerName + "&ContactNo=" + ContactNo + "&EmailId=" + EmailId;
    $.ajax({
        type: "Get",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {

            if (!IsJsonString(data)) {
                $("#tblServiceRequest").html(data);
            }
        }
    });
}