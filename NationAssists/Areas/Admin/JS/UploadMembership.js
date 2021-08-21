function BindBroker() {
    if ($("#BrokerTypeId").val() != "") {

        var UserTypeId = $("#BrokerTypeId").val();
        var UserType = $("#BrokerTypeId").val();
        $("#ServiceId").html("'<option>--Select--</option>'");

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

                $("#BrokerId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#BrokerId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#BrokerId").append(
                            $('<option></option>').val(service.ServiceProviderId).html(service.FirstName));
                    });

                    
                }
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

    if ($("#membershipfile").val() == "") {

        IsValid = false;
        strErrMsg += "Please upload file. \n";
    }

    if ($("#membershipfile").val() != "" && $.inArray( $("#membershipfile").val().split('.').pop().toLowerCase(),["xls","xlsx"])==-1) {

        IsValid = false;
        strErrMsg += "Invalid file  extension. \n";
    }
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function UploadMembershipFile() {
    if (ValidateForm()) {
        var frmMemembership = new FormData();
        frmMemembership.append("SourceId", $('#BrokerId').val())
        frmMemembership.append("SourceName", $("#BrokerId option:selected").text());
        frmMemembership.append("MembershipFile", document.getElementById("membershipfile").files[0]);
        var pUrl = "/Brokers/UploadFile/";
        $.ajax({
            type: "POST",
            url: pUrl,
            data: frmMemembership,
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                debugger;
                var Jdata = JSON.parse(data);
                if (Jdata.Result) {
                    var ResponseList = Jdata.Response;
                    ResponseList.forEach(function (e, i) {
                        if (e.ErrorMessage != null && e.ErrorMessage != "") {
                            if (e.sheetName == "") {
                                alert(e.ErrorMessage);
                            }
                            else {
                                alert("Sheet " + e.sheetName + " has error " + e.ErrorMessage);
                            }
                        }
                        else {
                         
                            alert("Sheet " + e.sheetName + " Data has been successfully saved. ");
                        }
                    });
                }
                else {
                    console.log(Jdata.Error);
                    alert("Opps! some error occured in Membership upload. Please try after some time.");
                  
                }
                        
               

            },
            error: function (data) {
            }
        });
    }
}

