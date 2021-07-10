function ShowServiceRequestDetail(e) {
    var hhh = "";
    $("#hdnServiceRequestId").val(e.ServiceRequestId);
    $("#hdnServiceProviderId").val(e.ServiceProviderId);//
    $("#hdnServiceAllocationId").val(e.ServiceAllocationId);
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
    $("#txtContactNo").val(e.ContactMobileNo);
    $("#txtReleventDetails").val(e.ReleventDetails);
    $("#dvServiceRequestDetail").show();
    $("#hdnServiceId").val(e.ServiceId);
    $("#hdnSubCategoryId").val(e.ServiceSubCategoryId);
    
    var SelectedService = e.ServiceName;
    var ServiceCode = SelectedService.substring(SelectedService.indexOf('(') + 1, SelectedService.indexOf(')'));
    if (ServiceCode == "HAP") {
        $('.RAP').hide();
        $('.HAP').show();

    }
    else {
        $('.RAP').show();
        $('.HAP').hide();
    }
    var scrollPos = $("#dvServiceRequestDetail").offset().top;
    $(window).scrollTop(scrollPos);

 


    $("#tblServiceRequest").hide();
}



function SaveServiceAllocation() {

    if ($("#ddlStatus").val() == "0") {

        alert("Please select Status.");

    }
    else if ($("#ddlStatus").val() == "AC" && $("#AssignedToUserId").val() == "") {
        alert("Please select User.");
    }
    else if ($("#ddlStatus").val() == "RJ" && $("#txtReasonForRejection").val() == "") {
        alert("Please enter Rejection Reason");
    }

    else {

        var objServiceAllocationReq = {
            ServiceProviderId: $("#hdnServiceProviderId").val(),
            ServiceRequestId: $("#hdnServiceRequestId").val(),
            ServiceId: $("#hdnServiceId").val(),  
            ServiceSubCategoryId: $("#hdnSubCategoryId").val(),  
            ServiceAllocationStatus: $("#ddlStatus").val() == "AC" ? 2 : 3,
            ReasonForRejection: $("#txtReasonForRejection").val(),
            AssignedToUser: $("#AssignedToUserId").val(),
            ServicePrice: 0,
            ServicePriceId: 0,
            ServiceAllocationId: $("#hdnServiceAllocationId").val(),

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
                    var StrMsg = $("#ddlStatus").val() == "AC" ? "Service Allocation request is accepted and successfully assigned" : "Service Allocation request is rejected.";
                    alert(StrMsg);
                    window.location.href = "/admin/ServiceRequest/RespondToServiceAllocation/";
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