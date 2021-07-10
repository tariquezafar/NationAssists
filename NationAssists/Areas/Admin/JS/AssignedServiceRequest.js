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

}

function SaveServiceAllocation() {

    if ($("#txtServiceCompletionRemarks").val()!="") {

        var objServiceAllocationReq = {
            ServiceProviderId: $("#hdnServiceProviderId").val(),
            ServiceRequestId: $("#hdnServiceRequestId").val(),
            ServiceId: $("#hdnServiceId").val(),
            ServiceSubCategoryId: $("#hdnSubCategoryId").val(),
            ServiceAllocationStatus: 4,
            Remarks: $("#txtServiceCompletionRemarks").val(),
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
                    alert("Service Request is completed successfully.");
                    window.location.href = "/admin/ServiceRequest/AssignedServiceRequest/";
                }
                else {
                    alert("Opps! some error occured.");
                }

            },
            error: function (data) {
            }
        });
    }
    else {
        alert("Please enter service completion Remarks.");
    }
}