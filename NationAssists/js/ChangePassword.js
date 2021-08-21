function ChangePassword() {
    if ($("#txtCurrentPassword").val() == "") {

        alert("Please enter your current Password");
        $("#txtCurrentPassword").focus();
    }
    else if ($("#txtNewPassword").val() == "") {
        alert("Please enter New  Password");
        $("#txtNewPassword").focus();
    }
    else if ($("#txtNewPassword").val() != "" && $("#txtNewPassword").val() != $("#txtConfirmNewPassword").val()) {
        alert("Password and confirmd must be same");
        $("#txtConfirmNewPassword").focus();
    }
    else {
        var objCP = {

            UserId: $("#hdnUserId").val(),
            UserType: $("#hdnUserType").val(),
            Password: $("#txtNewPassword").val(),
            OldPassword: $("#txtCurrentPassword").val(),
        };
        var pUrl = "/Home/UpdateUserPassword/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objCP),
            success: function (data) {
                if (data.ErrorMessage != null && data.ErrorMessage != ""  ) {

                    alert(data.ErrorMessage);
                    
                }
                else {
                    alert("Password Change successfully.");
                    if ($("#hdnUserType").val() == "EMP") {
                        window.location.href = "../Dashboard";
                    }
                    else {
                        window.location.href = "../Home";
                    }
                }
            }

        });
    }

    
}

