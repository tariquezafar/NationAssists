function BindBroker(id,e) {
    if ($(id).val() != "") {

        var UserTypeId = $(id).val();
        var UserType = $(id).val();
        $("#ServiceId").html("'<option value='0'>--Select--</option>'");
     
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

                    $('#' + e).html(""); // clear before appending new list
                    var jData = JSON.parse(data);
                    if (jData != null && jData.length > 0) {
                        $('#' + e).append($('<option></option>').val(0).html("--Select--"));
                        $.each(jData, function (i, service) {
                            $('#' + e).append(
                                $('<option></option>').val(service.ServiceProviderId).html(service.FirstName));
                        });

                        if ($("#hdnSourceId").val() != "0") {
                            $('#' + e).val($("#hdnSourceId").val());
                            GetAllServices();  
                        }
                    }
                }
            });
      
    }
}

function GetAllServices() {

    if ($("#BrokerId").val() != "") {
        var BrokerId = $("#BrokerId").val();
        var pUrl = "/Admin/Brokers/BindServices?BrokerId=" + BrokerId;
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
                $("#ServiceId").html(""); // clear before appending new list
                if (jData != null && jData.length > 0) {
                    $("#ServiceId").append($('<option></option>').val(0).html("--Select Services--"));
                    $.each(jData, function (i, service) {
                        $("#ServiceId").append(
                            $('<option></option>').val(service.ServiceId).html(service.ServiceName + "(" + service.ServiceCode+")" ));
                    });

                    if ($("#hdnPackageId").val() != "0") {
                        $("#ServiceId").val($("#hdnPackageId").val());
                    }
                }
            }
        });
    }

}

function BindMembership() {
    if ($("#BrokerTypeId").val() != "" && $("#BrokerId").val() != "0" && $("#ServiceId").val() != "0") {
        var SelectedService = $("#ServiceId option:selected").text();
        var ServiceCode = SelectedService.substring(SelectedService.lastIndexOf('(') + 1, SelectedService.lastIndexOf(')'));
        if (ServiceCode == "HA") {
            $('.RAS').hide();
            $('.HAS').show();

        }
        else {
            $('.RAS').show();
            $('.HAS').hide();
        }
    }
}

function SaveMembership() {

    if (ValidateForm()) {

        var objMembership = {
            MembershipId: $("#hdnMembershipId").val(),
            SourceId: $("#BrokerId").val(),
            PackageId: $("#ServiceId").val(),
            Branch: $("#txtBranch").val(),
            PolicyType: $("#txtPolicyType").val(),
            PolicyNo: $("#txtPolicyNo").val(),
            IssueDate: $("#txtIssueDate").val(),
            EffectiveDate: $("#txtEffectiveDate").val(),
            ExpiryDate: $("#txtExpiryDate").val(),
            InsuredName: $("#txtInsuredName").val(),
            CPRNumber: $("#txtCPRNumber").val(),
            MobileNo: $("#txtMobileNo").val(),
            EmailId: $("#txtEmailId").val(),
            VehicleRegistrationNo: $("#txtVehicleRegistrationNo").val(),
            ChassisNo: $("#txtChassisNo").val(),
            VehicleMake: $("#txtVehicleMake").val(),
            VehicleType: $("#txtVehicleType").val(),
            VehicleBody: $("#txtVehicleBody").val(),
            VehicleYear: $("#txtVehicleYear").val(),
            VehicleReplacement:( $('input[type="radio"][name="VehicleReplacement"]:checked').val()=="NO"? false :true),
            RiskAddress:  $("#txtRiskAddress").val(),
            NoOfLocation: $("#txtNoOfLocation").val(),
            TypeOfService:$("#txtTypeOfService").val(),
          

        }; 

       

        var pUrl = "/Admin/Brokers/SaveMemberShip/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objMembership),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Membership Saved successfully ");
                    window.location.href = "/admin/Brokers/ManageMembership/";
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
function ValidateForm() {

    var IsValid = true;
    var strErrMsg = "";
    if ($("#BrokerTypeId").val() == "") {
        IsValid = false;
        strErrMsg += "Please select source Type. \n";
    }
    if ($('#BrokerId').val() == "" || $('#BrokerId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Source. \n";
    }
    if ($('#ServiceId').val() == "" || $('#ServiceId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service. \n";
    }


    if ($("#txtBranch").val() == "") {
            IsValid = false;
            strErrMsg += "Please enter Branch/Location . \n";
    }
    if ($("#txtCPRNumber").val() == "") {
        IsValid = false;
        strErrMsg += "Please enter C.P.R Number . \n";
    }
    if ($("#txtPolicyNo").val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Policy No . \n";
    }
    if ($("#txtInsuredName").val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Insured Name . \n";
    }
    if ($("#txtIssueDate").val() == "") {
        IsValid = false;
        strErrMsg += "Please select issue data. \n";
    }
    if ($("#txtEffectiveDate").val() == "") {
            IsValid = false;
            strErrMsg += "Please select effective date. \n";
        }
    if ($("#txtExpiryDate").val() == "") {
            IsValid = false;
        strErrMsg += "Please select expiry date. \n";
        }
    if ($("#txtExpiryDate").val() != "" && $("#txtEffectiveDate").val() != "" && Date.parse($("#txtExpiryDate").val()) <= Date.parse($("#txtEffectiveDate").val())) {

        IsValid = false;
        strErrMsg += "Expiry Date should be greater than Effective date.. \n";
    }
    if ($("#txtMobileNo").val() != "" && $("#txtMobileNo").val().length < 8) {
        IsValid = false;
        strErrMsg += "Mobile No must contain 8 digit. \n";
    }
    if ($("#txtEmailId").val() != "" && !isValidEmail($("#txtEmailId").val())) {
        IsValid = false;
        strErrMsg += "Please enter a valid EmailId. \n";
    }
    if ($("#BrokerTypeId").val() != "" && $("#BrokerId").val() != "0" && $("#ServiceId").val() != "0") {
        var SelectedService = $("#ServiceId option:selected").text();
        var ServiceCode = SelectedService.substring(SelectedService.lastIndexOf('(') + 1, SelectedService.lastIndexOf(')'));
        if (ServiceCode == "HA") {

            if ($("#txtRiskAddress").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Risk Address . \n";
            }
            if ($("#txtNoOfLocation").val() == "" || $("#txtNoOfLocation").val() == "0") {
                IsValid = false;
                strErrMsg += "Please enter No of Location . \n";
            }
            if ($("#txtTypeOfService").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Type of Services. \n";
            }
        }
        else {

            if ($("#txtPolicyType").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Policy Type . \n";
            }
            if ($("#txtVehicleRegistrationNo").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter vehicle registration No. \n";
            }
            if ($("#txtChassisNo").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Chassis No. \n";
            }
            if ($("#txtVehicleYear").val() == "" || $("#txtVehicleYear").val() == "0") {
                IsValid = false;
                strErrMsg += "Please enter Vehicle year . \n";
            }
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

function EditMember(e) {


    var Membershipid = $(e).attr("data-id");
    var pUrl = "/Admin/Brokers/GetMembershipDetail?MembershipId=" + Membershipid;
    $.ajax({
        type: "Get",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {
            debugger;
            var Jdata = JSON.parse(data);
            $("#BrokerTypeId").val(Jdata.SourceType);
            BindBroker();
            $("#hdnMembershipId").val(Jdata.MembershipId);
            $("#hdnSourceId").val(Jdata.SourceId);
            $("#hdnPackageId").val(Jdata.PackageId);
            BindMembership();
            $("#txtBranch").val(Jdata.Branch);
            $("#txtPolicyType").val(Jdata.PolicyType);
            $("#txtPolicyNo").val(Jdata.PolicyNo);
            $("#txtIssueDate").val(Jdata.IssueDate);
            $("#txtEffectiveDate").val(Jdata.EffectiveDate);
            $("#txtExpiryDate").val(Jdata.ExpiryDate);
            $("#txtInsuredName").val(Jdata.InsuredName);
            $("#txtCPRNumber").val(Jdata.CPRNumber);
            $("#txtMobileNo").val(Jdata.MobileNo);
            $("#txtEmailId").val(Jdata.EmailId);
            $("#txtVehicleRegistrationNo").val(Jdata.VehicleRegistrationNo);
            $("#txtChassisNo").val(Jdata.ChassisNo);
            $("#txtVehicleMake").val(Jdata.VehicleMake);
            $("#txtVehicleType").val(Jdata.VehicleType);
            $("#txtVehicleBody").val(Jdata.VehicleBody);
            $("#txtVehicleYear").val(Jdata.VehicleYear);
            $("#txtRiskAddress").val(Jdata.RiskAddress);
            $("#txtNoOfLocation").val(Jdata.NoOfLocation);
            $("#txtTypeOfService").val(Jdata.TypeOfService);


            //var IssueDate = new Date(parseFloat(Jdata.IssueDate.substring(Jdata.IssueDate.indexOf('(') + 1, Jdata.IssueDate.indexOf(')'))));
            //$("#txtIssueDate").val(formatDate(IssueDate));
            

            //var EffectiveDate = new Date(parseFloat(Jdata.EffectiveDate.substring(Jdata.EffectiveDate.indexOf('(') + 1, Jdata.EffectiveDate.indexOf(')'))));
            //$("#txtEffectiveDate").val(formatDate(EffectiveDate));

            //var ExpiryDate = new Date(parseFloat(Jdata.ExpiryDate.substring(Jdata.EffectiveDate.indexOf('(') + 1, Jdata.ExpiryDate.indexOf(')'))));
            //$("#txtExpiryDate").val(formatDate(ExpiryDate));

            $("#txtIssueDate").val(Jdata.Issue_Date);
            $("#txtEffectiveDate").val(Jdata.Effective_Date);
            $("#txtExpiryDate").val(Jdata.Expiry_Date);
            //$('input[name="chkService"][type="checkbox"]').prop("checked", false);
        


            $("#btnAdd").html("Update");
            $("input[type=radio][name=VehicleReplacement][value=" + (Jdata.VehicleReplacement == true ?"YES":"NO") + "]").prop("checked", true);
            $('#dvDetail').hide();
            $('#dvMembershipDetail').show();
        },
        error: function (data) {
        }
    });

}
function SearchMembership() {
    var SourceType = $("#ddlBrokerTypeId").val();
    var SourceId = $("#ddlBrokerId").val() == "" ? 0 : parseInt($("#ddlBrokerId").val());
    var PackageId = 0;
    var CPRNumber = $("#CPRNumber").val();
    var PolicyType = $("#PolicyType").val();
    var PolicyNo = $("#PolicyNo").val();
    var InsuredName = $("#InsuredName").val();
    var MobileNo = $("#MobileNo").val();
    var EmailId = $("#EmailId").val();
    var VehicleRegistrationNo = $("#VehicleRegistrationNo").val();
    var ChassisNo = $("#ChassisNo").val();
    var Service = $("#Service").val();

    var pUrl = "/Admin/Brokers/SearchMemberShip?SourceType=" + SourceType + "&SourceId=" + SourceId + "&PackageId=" + PackageId + "&CPRNumber=" + CPRNumber + "&PolicyType=" + PolicyType + "&PolicyNo=" + PolicyNo + "&InsuredName=" + InsuredName + "&MobileNo=" + MobileNo + "&EmailId=" + EmailId + "&VehicleRegistrationNo=" + VehicleRegistrationNo + "&ChassisNo=" + ChassisNo + "&Service=" + Service;
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
                $("#tblMembership").html(data);
            }
        }
    });
    
}

function SendGenerateCertificate(e) {

    if (e.EmailId != null && e.EmailId != "") {
        var objMembership = {
            PackageId: e.PackageId,
            PackageName: e.PackageName,
            IssueDate: e.IssueDate,
            EffectiveDate: e.EffectiveDate,
            ExpiryDate: e.ExpiryDate,
            InsuredName: e.InsuredName,
            VehicleRegistrationNo: e.VehicleRegistrationNo,
            ChassisNo: e.ChassisNo,
            VehicleMake: e.VehicleMake,
            VehicleYear: e.VehicleYear,
            RiskAddress: e.RiskAddress,
            MembershipReferenceNo: e.MembershipReferenceNo,
            EmailId: e.EmailId
        };



        var pUrl = "/Admin/Brokers/SendGeneratedCertificate/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objMembership),
            success: function (data) {
                debugger;

                if (data.Error == null || data.Error=="") {
                    alert("Certificate send successfully");

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
        alert("Customer Email doesnot exists.");
    }
}


function AddMemmbership() {
    $("#dvDetail").hide();
    $("#dvMembershipDetail").show();
}






