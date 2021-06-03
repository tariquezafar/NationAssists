function SaveUsers() {
    if (ValidateForm()) {
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
            UserReferenceId: $("#UserReferenceId").val()
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
    if ($("#txtMobileNo").val() != "" && $("#txtMobileNo").val().length!=10) {

        IsValid = false;
        strErrMsg += "Mobile No must contain 10 digit. \n";
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
           
            $("#btnAdd").html("<strong>Update</strong>");
        },
        error: function (data) {
        }
    });

}


function BindUserTypeDetail() {
    if ($("#UserTypeId").val() != "") {
        var UserTypeId = $("#UserTypeId").val();
        var UserType = $("#UserTypeId").val() == "2" ? 'AG' : ($("#UserTypeId").val() == "3" ? 'IC' : ($("#UserTypeId").val() == "4" ? 'SP' : ''));

        if (UserTypeId != "1") {
            var pUrl = "/Admin/User/BindUserTypeDetail?UserType=" + UserType;
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
                        $("#lblUserDetail").html(UserType == "SP" ? "Service Provider" : (UserType=="AG"? "Agent" :"Insurance Company"));
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