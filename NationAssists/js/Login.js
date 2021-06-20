$("#btnLogin").click(function () {
    if ($('#txtEmailId').val() == "") {
        alert("Please enter Email Address.");
        $('#txtEmailId').focus();
    }
    else if ($('#txtEmailId').val() != "" && !isValidEmail($('#txtEmailId').val())) {
        alert("Please enter a valid Email Address.");
        $('#txtEmailId').focus();
    }
    else if ($('#txtPassword').val() == "") {
        alert("Please enter Password.");
        $('#txtPassword').focus();
    }
    else {

        if (window.location.href.indexOf('?') != -1) {
            var QueryStrings = window.location.href.substring(window.location.href.indexOf('?') + 1);
            var LoginType = QueryStrings.split('=')[1];
            var objLogin = {

                LoginType: LoginType,
                EmailId: $("#txtEmailId").val(),
                Password: $("#txtPassword").val(),
                RememberMe: $("#chkRememberMe").prop("checked")
            };
            var pUrl = "/Login/ValidateLogin/";
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: pUrl,
                data: JSON.stringify(objLogin),
                success: function (data) {

                    if (data.ErrorMessage==null || data.ErrorMessage =="") {
                        if (data.IsValid) {
                            if (objLogin.LoginType == "Customer") {
                                window.location.href = "/Home";
                            }
                            else {
                                window.location.href = "/admin/Dashboard";
                            }

                        }
                        else {

                           alter("Either EmailId or password is incorrect.")

                        }
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

});

$("#btnLogout").click(function () {
    var pUrl = "/Login/Logout/";
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        url: pUrl,
        success: function (data
        ) {
            if (data) {
                window.location.href = "/Home";
            }
        },
        error: function (data) {
        }
    });

});


function Logout() {
    var pUrl = "/Login/Logout/";
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        url: pUrl,
        success: function (data
        ) {
            if (data) {
                window.location.href = "/Home";
            }
        },
        error: function (data) {
        }
    });
}