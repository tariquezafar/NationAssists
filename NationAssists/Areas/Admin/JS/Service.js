

function SaveService() {

    if (ValidateForm()) {

        var objService = {
            ServiceId: $("#hdnServiceID").val(),
            ServiceCode: $("#txtServicecode").val(),
            ServiceName: $("#txtServicename").val(),
            PackageCode: $("#txtPackageCode").val(),
            IsActive: $("#chkIsActive").prop("checked")
        };



        var pUrl = "/Admin/Service/SaveServices/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objService),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Service is successfully " + ($("#hdnServiceID").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/Service/ManageService/";
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


function DeleteServiceSubCategory(e) {
    if (confirm("Are you sure you want to delete  Service SubCategory ?")) {
        var ServiceId = $(e).attr("data-id");
        var pUrl = "/Admin/Service/DeleteServiceSubCategory?ServiceId=" + ServiceId;
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
                    alert("Service Sub Category is deleted successfully");
                    window.location.href = "/admin/Service/AddServiceSubCategory/";
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
    if ($('#txtServicecode').val() == "") {
        IsValid = false;
        strErrMsg += "Please Enter Service Code. \n";
    }

    if ($("#txtServicename").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter Service  Name ";
    }



    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function EditService(e) {

    $("#hdnServiceID").val(e.ServiceId);
    $("#txtServicecode").val(e.ServiceCode);
    $("#txtServicename").val(e.ServiceName);
    $("#txtPackageCode").val(e.PackageCode);
    $("#btnAdd").html("<strong>Update</strong>");
    $('#chkIsActive').prop('checked', e.IsActive );

}