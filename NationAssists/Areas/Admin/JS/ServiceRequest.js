function ShowServiceRequestDetail(e) {
    var hhh = "";
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

    $("#dvDetail").hide();
    //dvDetail
}




$("#ddlSourceType").change(function () {

    if ($(this).val() != "0") {


        var UserType = $(this).val();
        $("#ddlSource").html("'<option>--Select--</option>'");

        var pUrl = "/Admin/Brokers/BindBroker?UserType=" + UserType;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#ddlSource").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#ddlSource").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#ddlSource").append(
                            $('<option></option>').val(service.ServiceProviderId).html(service.FirstName));
                    });

                    $("#liSource").show();


                }
            }
        });

    }
})


function SearchServiceRequest() {

    if ($("#txtEndDate").val() != "" && $("#txtStartDate").val() != "" && Date.parse($("#txtEndDate").val()) <= Date.parse($("#txtStartDate").val())) {

        alert("End Date should be greater than start date");

    }
    else {
        $("#tblServiceRequest").html('');

        var objSearchSR = {
            ServiceRequestStatusId: $("#ServiceRequestStatusId").val() == "" ? "0" : $("#ServiceRequestStatusId").val(),
            TicketNo: $("#txtTicketNo").val(),
            AccountType: $("#ddlAccountType").val() == "0" ? "" : $("#ddlAccountType").val(),
            AccountSubType: $("#ddlSourceType").val() == "0" ? "" : $("#ddlSourceType").val(),
            BrokerId: $("#ddlSource").val(),
            StartDate: $("#txtStartDate").val(),
            EndDate: $("#txtEndDate").val()
        };

        var pUrl = "/Admin/ServiceRequest/SearchServiceRequest";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objSearchSR),
            success: function (data) {

                if (!IsJsonString(data)) {
                    $("#tblServiceRequest").html(data);
                }

            },
            error: function (ddsds) {
                $("#tblServiceRequest").html(ddsds.responseText);
            }
        });
    }


}