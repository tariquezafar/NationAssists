function SaveUsers() {
    if (ValidateForm()) {
        var Reference = $("#UserReferenceId option:selected").text();
        var objUsers = {
            UserId: $("#hdnUserId").val(),
           PhoneNo: $("#txtPhoneNo").val(),
            EmailId: $("#txtEmailId").val(),
            Name: $("#txtUserName").val(),
            Password: $("#txtPassword").val(),
            RoleId: $("#RoleId").val(),
            UserTypeId: $("#UserTypeId").val(),
            IsActive: $("#chkIsActive").prop("checked"),
            MobileNo: $("#txtMobileNo").val(),
            BranchId: $("#BranchId").val(),
            UserReferenceId: $("#UserReferenceId").val(),
            CPRNumber: $("#txtCPRNumber").val(),
            CPRExpiryDate: $("#txtCPRExpiryDate").val()      ,
            PassportNumber: $("#txtPassportNumber").val()     ,
            PassportExpiryDate: $("#txtPassportExpiryDate").val()    ,
            VisaNumber: $("#txtVisaNumber").val()    ,
            ContactAddressHomeCountry: $("#txtContactAddressHomeCountry").val()    ,
            ContactAddressLocal: $("#txtContactAddressLocal").val()    ,
            MobileNumberLocal: $("#txtMobileNoLocal").val()    ,
            EmergencyContactPersonName: $("#txtEmergencyContactPersonName").val()    ,
            DateOfJoining: $("#txtDateOfJoining").val(),
            Remarks: $("#txtRemarks").val(),
            Reference_Code: $("#UserTypeId").val() != "" && $("#UserTypeId").val() == "1" ? "" : Reference.substring(Reference.indexOf('(') + 1, Reference.indexOf(')'))
        };
        var pUrl = "/Admin/User/SaveUsers/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objUsers),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("User  is successfully " + ($("#hdnUserId").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/User/AddUser/";
                }
                else {
                    if (data.DuplicateEmail) {
                        alert("Email already exists.");
                    }
                    else if (data.DuplicateMobile) {
                        alert("Mobile No already exists.");
                    }
                    else {
                        alert("Opps! some error occured.");
                    }
                }
            },
            error: function (data) {
            }
        });
    }
}

function DeleteUsers(e) {
    if (confirm("Are you sure you want to delete User ?")) {
        var UserId = $(e).attr("data-id");
        var pUrl = "/Admin/User/DeleteUsers?UserId=" + UserId;
        $.ajax({
            type: "POST",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                debugger;
                var Jdata = JSON.parse(data);
                if (Jdata.Result) {
                    alert("User is deleted successfully");
                    window.location.href = "/admin/User/AddUser/";
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
        return false;
    }
}

function ValidateForm() {
    var IsValid = true;
    var strErrMsg = "";
    var UserType = $("#UserTypeId").val() == "2" ? 'AG' : ($("#UserTypeId").val() == "3" ? 'IC' : ($("#UserTypeId").val() == "4" ? 'SP' : ''));


    if ($("#UserTypeId").val() == "") {

        IsValid = false;
        strErrMsg += "Please select User Type. \n";
    }
   
    if ($("#txtUserName").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter  Name . \n";
    }
    if ($("#txtPassword").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter Password . \n";
    }
    if ($("#txtEmailId").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter EmailId . \n";
    }
    if ($("#txtMobileNo").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter Mobile No . \n";
    }
    if ($("#txtMobileNo").val() != "" && $("#txtMobileNo").val().length <8) {

        IsValid = false;
        strErrMsg += "Mobile No must contain minimum 8 digit. \n";
    }
    if ($("#txtMobileNoLocal").val() != "" && $("#txtMobileNoLocal").val().length <8) {

        IsValid = false;
        strErrMsg += "Mobile Number (Local) No must contain minimum 8 digit. \n";
    }

    if ($("#txtPhoneNo").val() != "" && $("#txtPhoneNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Phone No must contain minimum 8 digit. \n";
    }
    if ( $("#RoleId").val()=="") {

        IsValid = false;
        strErrMsg += "Please select Role. \n";
    }
    if ($("#UserTypeId").val()=="1" &&  $("#BranchId").val() == "") {

        IsValid = false;
        strErrMsg += "Please select Branch. \n";
    }

    if ($("#UserTypeId").val() != "" && $("#UserTypeId").val() == "1" && $("#BranchId").val() == "") {

        IsValid = false;
        strErrMsg += "Please select Branch. \n";
    }
  
    

    if ($("#UserTypeId").val() != "" && $("#UserTypeId").val() != "1" && $("#UserReferenceId").val() == "0") {

        var UserTypeName = UserType == "SP" ? "Service Provider" : (UserType == "AG" ? "Agent" : "Insurance Company");
        IsValid = false;
        strErrMsg += "Please select "+UserTypeName+". \n";
    }

    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function EditUser(e) {

   
    var UserId = $(e).attr("data-id");
    var pUrl = "/Admin/User/GetUserDetail?UserId=" + UserId;
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
            $("#hdnUserId").val(Jdata.UserId);
            $("#txtPhoneNo").val(Jdata.PhoneNo);
            $("#txtEmailId").val(Jdata.EmailId);
            $("#txtUserName").val(Jdata.Name);
            $("#txtPassword").val(Jdata.Password);
            $("#RoleId").val(Jdata.RoleId);
            $("#UserTypeId").val(Jdata.UserTypeId);
            $("#chkIsActive").prop("checked", Jdata.IsActive);
            $("#txtMobileNo").val(Jdata.MobileNo);
            if (Jdata.UserReferenceId != null && Jdata.UserReferenceId != 0) {
                BindUserTypeDetail();
               
                $("#UserReferenceId").val(Jdata.UserReferenceId);
            }
            else {
                $("#BranchId").val(Jdata.BranchId);
               
            }

            $("#txtCPRNumber").val(Jdata.CPRNumber);
            if (Jdata.CPRExpiryDate!=null && Jdata.CPRExpiryDate != "") {
                var CPRExpiryDate = new Date(parseFloat(Jdata.CPRExpiryDate.substring(Jdata.CPRExpiryDate.indexOf('(') + 1, Jdata.CPRExpiryDate.indexOf(')'))));
                $("#txtCPRExpiryDate").val(formatDate(CPRExpiryDate));
            }
            if (Jdata.PassportExpiryDate !=null &&  Jdata.PassportExpiryDate != "") {
                var PassportExpiryDate = new Date(parseFloat(Jdata.PassportExpiryDate.substring(Jdata.PassportExpiryDate.indexOf('(') + 1, Jdata.PassportExpiryDate.indexOf(')'))));
                $("#txtPassportExpiryDate").val(formatDate( PassportExpiryDate));
            }
            if (Jdata.DateOfJoining !=null && Jdata.DateOfJoining != "") {
                var DateOfJoining = new Date(parseFloat(Jdata.DateOfJoining.substring(Jdata.DateOfJoining.indexOf('(') + 1, Jdata.DateOfJoining.indexOf(')'))));
                $("#txtDateOfJoining").val(formatDate( DateOfJoining));
            }
            
            $("#txtPassportNumber").val(Jdata.PassportNumber);
          
            $("#txtVisaNumber").val(Jdata.VisaNumber);
            $("#txtContactAddressHomeCountry").val(Jdata.ContactAddressHomeCountry);
            $("#txtContactAddressLocal").val(Jdata.ContactAddressLocal);
            $("#txtMobileNoLocal").val(Jdata.MobileNumberLocal);
            $("#txtEmergencyContactPersonName").val(Jdata.EmergencyContactPersonName);
           
            $("#txtRemarks").val(Jdata.Remarks);
           
            $("#btnAdd").html("<strong>Update</strong>");
        },
        error: function (data) {
        }
    });

}


function BindUserTypeDetail() {
    if ($("#UserTypeId").val() != "") {
        var userType = $("#UserTypeId option:selected").text();
        var UserTypeCode = userType.substring(userType.indexOf('(') + 1, userType.indexOf(')'));
       // var UserTypeId = $("#UserTypeId").val();
       // var UserType = $("#UserTypeId").val() == "2" ? 'AG' : ($("#UserTypeId").val() == "3" ? 'IC' : ($("#UserTypeId").val() == "4" ? 'SP' : ''));

        if (UserTypeCode != "EMP") {
            var pUrl = "/Admin/User/BindUserTypeDetail?UserType=" + UserTypeCode;
            $.ajax({
                type: "Get",
                url: pUrl,
                data: {},
                dataType: 'html',
                contentType: false,
                processData: false,
                async: false,
                success: function (data) {

                    $("#UserReferenceId").html(""); // clear before appending new list
                    var jData = JSON.parse(data);
                    if (jData != null && jData.length > 0) {
                        $("#UserReferenceId").append($('<option></option>').val(0).html("--Select--"));
                        $.each(jData, function (i, service) {
                            $("#UserReferenceId").append(
                                $('<option></option>').val(service.ServiceProviderId).html(service.FirstName));
                        });
                        $('#liBranch').hide();
                        $("#liUserType").show();
                        $("#BranchId").val('');
                        $("#lblUserDetail").html("Source");
                    }
                }
            });
        }
        else {
            $('#liBranch').show();
            $("#liUserType").hide();
            $("#UserReferenceId").val('');
        }
    }
}

function SearchUser() {
    var UserId = 0;
    var UserTypeId = $("#UserTypeId").val() == "" ? 0 : parseInt($("#UserTypeId").val());
    var UserReferenceId = $("#UserReferenceId").val() == "" ? 0 : parseInt($("#UserReferenceId").val());
    var UserCode = $("#txtUserCode").val();
    var UserName = $("#txtUserName").val();

    var pUrl = "/Admin/User/BindUsers?UserId=" + UserId + "&UserTypeId=" + UserTypeId + "&UserReferenceId=" + UserReferenceId + "&UserCode=" + UserCode + "&UserName=" + UserName;
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
                $("#tblUsers").html(data);
            }
            else {
                console.log(data);
            }
        }
    });
}

