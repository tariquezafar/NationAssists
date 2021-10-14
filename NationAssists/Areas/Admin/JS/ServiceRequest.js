function ShowServiceRequestDetail(e) {
    var hhh = "";

    $("#hdnServiceRequestId").val(e.ServiceRequestId);
    BindServiceRemarksByRequestId(e.ServiceRequestId);
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
    $("#txtRemarks").val(e.Remarks);
    
    $("#txtCustomerName").val(e.Customer_Name);
    $("#txtCustomerEmailId").val(e.EmailId);
    $("#hdnTicketNo").val(e.TicketNo);
    $("#hdnSourceType").val(e.SourceType);
    $("#hdnSourceName").val(e.SourceName);
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

    $("#dvDetail").hide();
    if (e.ServiceRequestStatus == 2 || e.ServiceRequestStatus == 4) {
        BindServiceProviderByRequestId(e.ServiceRequestId);
        $("#txtServiceAllocationRemarks").val(e.ServiceAllocationRemarks);
    }
    else {
        $('#tblProviderList').html('');
    }


    $("#hdnServiceRequestID").val(e.ServiceRequestId);
    $("#hdnServiceAllocationID").val(e.ServiceAllocationId);
    if (e.ServiceRequestStatus == 4 || (parseInt($("#hdnUserID").val()) != parseInt(e.AssignedTo))) {
        $("#Action").hide();
        $("#BtnUpdate").hide();
        $('.ASG').hide();
        $('#ClosingRemarks').hide();
    }
    else {
        BIndAssignedToUser(e.AcceptedBy);
        $("#Action").show();
        if (e.ServiceAllocationStatus != null && (e.ServiceAllocationStatus == 1 || e.ServiceAllocationStatus == 3)) {
           
            $("input[name=rdAction][value=Closed]").attr("disabled", "disabled");
        }
        $("#BtnUpdate").show();
    }
    //dvDetail
}

function BIndAssignedToUser(UserID) {
    if (UserID != "" && UserID != "0") {
        var pUrl = "/Admin/ServiceRequest/BindAssignedToUser?UserId=" + UserID;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#AssignedTo").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#AssignedTo").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, user) {
                        $("#AssignedTo").append(
                            $('<option></option>').val(user.UserId).html(user.Name + '(' + user.UserCode+')'));
                    });

                   

                }
            }
        });
    }
}

function BindServiceProviderByRequestId(serviceRequestID) {
    var pUrl = "/Admin/ServiceRequest/GetAllServiceProviderByServiceRequest?ServiceRequestId=" + serviceRequestID;
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
                var table = "<table> <thead> <tr>  <th>SNo.</th> <th>Service Provider Code</th> <th>Name</th>   <th>Mobile No </th> <th>Email id</th><th>Contact Person Name</th> <th>Contact Person No</th>  <th>Esclation Person Name</th> <th>Esclation Person No</th> <th>Esclation Person Email Id</th>  </tr> </thead>";

                table += "<tbody>";
                jData.forEach(function (e, i) {
                    var tbody = "<tr>";
                   
                    tbody += "<td>" + (i + 1) + "</td>";
                    tbody += "<td>" + e.ServiceProviderCode + "  </td>";
                    tbody += "<td>" + (e.FirstName + " " + e.LastName)  + " </td>";
                    tbody += "<td>" + e.MobileNo + " </td>";
                    tbody += "<td>" + e.EmailId + " </td>";
                    tbody += "<td>" + e.ContactPersonName + "</td>";
                    tbody += "<td>" + e.ContactPersonNo + "</td>";
                    tbody += "<td>" + e.EscalationPersonName + "</td>";
                    tbody += "<td>" + e.EscalationPersonContactNo + "</td>";
                    tbody += "<td>" + e.Escalation_Person_EmailId + "</td>";

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

function BindServiceRemarksByRequestId(serviceRequestID) {
    var pUrl = "/Admin/ServiceRequest/GetAllServiceRemarks?ServiceRequestId=" + serviceRequestID;
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
                var table = "<table> <thead> <tr>  <th>SNo.</th> <th>Remarks</th> <th>Created Date</th>   <th>Created By </th> </tr> </thead>";

                table += "<tbody>";
                jData.forEach(function (e, i) {
                    var tbody = "<tr>";
                    var CreatedDate = formatDateWithAMPM(parseFloat(e.CreatedDate.substring(e.CreatedDate.indexOf('(') + 1, e.CreatedDate.indexOf(')'))));
                    tbody += "<td>" + (i + 1) + "</td>";
                    tbody += "<td>" + e.Remarks + "  </td>";
                    tbody += "<td>" + CreatedDate + " </td>";
                    tbody += "<td>" + e.CreatedByUser + " </td>";
                    

                    tbody += "</tr>";

                    table += tbody;

                });

                $('#tblServiceRemarks').html(table);
            }
            else {
                $('#tblServiceRemarks').html('');
            }



        }
    });
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
            StartDate: $("#txtStartDate").val() == "" ? null : $("#txtStartDate").val(),
            EndDate: $("#txtEndDate").val() == "" ? null : $("#txtEndDate").val(),
            Service: $("#Service").val(),
            ServiceAllocationStatusId: $("#ServiceAllocationStatusId").val() == "" ? "0" : $("#ServiceAllocationStatusId").val()
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

$("input[name='rdAction']").change(function () {

    if ($(this).val() == "Assigned") {
        $('.ASG').show();
        $('#ClosingRemarks').hide();
        $("#UpdateRemarks").hide();
    }
    else if ($(this).val() == "UpdateRemarks")
    {
        $('.ASG').hide();
        $('#ClosingRemarks').hide();
        $("#UpdateRemarks").show();
    }
    else {
        $('.ASG').hide();
        $("#UpdateRemarks").hide();
        $('#ClosingRemarks').show();
    }
})


function UpdateServiceRequest() {

    if (ValidateServiceAllocation()) {

        var objServiceAllocationReq = {
            Action: $("input[name='rdAction']:checked").val(),
            ServiceAllocationId: $("#hdnServiceAllocationID").val(),
            ServiceRequestId: $("#hdnServiceRequestID").val(),
            AssignedToUser: $("#AssignedTo").val(),
            AssignmentRemarks: $("#txtAssignmentRemarks").val(),
            Remarks: $("input[name='rdAction']:checked").val() == "Closed" ? $("#txtClosingRemarks").val() : $("#txtUpdatedRemarks").val(),
            CustomerName: $("#txtCustomerName").val(),
            CustomerEmail: $("#txtCustomerEmailId").val(),
            ServiceType: $("#txtServiceType").val(),
            SourceType: $("#hdnSourceType").val(),
            SourceName: $("#hdnSourceName").val(),
            TicketNo: $("#hdnTicketNo").val()

        };

        var pUrl = "/Admin/ServiceRequest/UpdateServiceRequest/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objServiceAllocationReq),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Service Request is successfully Updated");
                    window.location.href = "/admin/ServiceRequest/index";
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
    if ($("input[name='rdAction']:checked").val() == "Assigned") {

        if ($("#AssignedTo").val() == "" || $("#AssignedTo").val() == "0") {

            IsValid = false;

            strErrMsg += "Please select Assigned To. \n";
        }

        if ($("#txtAssignmentRemarks").val() == "") {
            IsValid = false;

            strErrMsg += "Please Enter Assignment Remarks. \n";
        }
    }
    else if ($("input[name='rdAction']:checked").val() == "UpdateRemarks") {
        if ($("#txtUpdatedRemarks").val() == "") {
            IsValid = false;

            strErrMsg += "Please Enter Updated Remarks. \n";
        }
    }
    else {
        if ($("#txtClosingRemarks").val() == "") {
            IsValid = false;

            strErrMsg += "Please Enter Closing Remarks. \n";
        }
    }
    
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);

    }



}