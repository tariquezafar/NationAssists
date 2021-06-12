

function SaveServiceSubCategory() {

    if (ValidateForm()) {

        var objServiceSubCategory = {
            ServiceSubCategoryId: $("#hdnServiceSubCategoryID").val(),
            ServiceId: $("#ServiceId").val(),
            Name: $("#txtSubCategoryName").val(),
            IsActive: $("#chkIsActive").prop("checked")
        };
        


        var pUrl = "/Admin/Service/SaveServiceSubCategory/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data:  JSON.stringify(objServiceSubCategory),
            success: function (data) {
                debugger;
               
                if (data.Result) {
                    alert("Service SubCategory  is successfully " + ($("#hdnServiceSubCategoryID").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/Service/ManageServiceSubCategory/";
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
    if ($('#ServiceId').val() == "") {
        IsValid = false;
        strErrMsg += "Please select Service . \n";
    }
   
    if ($("#txtSubCategoryName").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter Service Sub Categroy Name ";
    }


    
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function EditServiceSubCategory(e) {
    
    $("#hdnServiceSubCategoryID").val($(e).attr("data-id"));
    $("#ServiceId").val($(e).attr("service-id"));
    $("#txtSubCategoryName").val($(e).attr("subcate-name"));
    $("#btnAdd").html("<strong>Update</strong>");
    $('#chkIsActive').prop('checked', $(e).attr("subCat_isActive") == "True" ? true : false);
    
}