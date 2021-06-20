var ServiceProviderFileList = [];

function SaveServiceProvider() {

    if (ValidateForm()) {
        var frmServiceProvider = new FormData();

        var strServices = "";
        $('input[name="chkService"][type="checkbox"]:checked').each(function (i,e) {
            strServices = strServices + $(e).val() + ","
        });
        
        frmServiceProvider.append("ServiceProviderId", $("#hdnServiceProviderID").val())
        frmServiceProvider.append("FirstName", $("#txtFirstName").val());
        frmServiceProvider.append("MiddleName", $("#txtMiddleName").val());
        frmServiceProvider.append("LastName", $("#txtLastName").val());
        frmServiceProvider.append("MobileNo", $("#txtMobileNo").val());
        frmServiceProvider.append("PhoneNo", $("#txtPhoneNo").val());
        frmServiceProvider.append("ContactPersonName", $("#txtContactPersonName").val());
        frmServiceProvider.append("ContactPersonNo", $("#txtContactPersonNo").val());
        frmServiceProvider.append("EscalationPersonName", $("#txtEscalationPersonName").val());
        frmServiceProvider.append("EscalationPersonContactNo", $("#txtEscalationPersonContactNo").val());
        frmServiceProvider.append("EmailId", $("#txtEmailId").val());
        frmServiceProvider.append("Password", $("#txtPassword").val());
        frmServiceProvider.append("CRNumber", $("#txtCRNumber").val());
        frmServiceProvider.append("Escalation_Person_EmailId", $("#txtEscalationEmailId").val());
        frmServiceProvider.append("EscalationLandlineNo", $("#txtEscalationLandlineNo").val());
        frmServiceProvider.append("OfficeLocationAddress", $("#txtOfficeAddress").val());
        frmServiceProvider.append("ServiceProviderAgreementFromDate", $("#txtAggreementStartDate").val());
        frmServiceProvider.append("ServiceProviderAgreementToDate", $("#txtAggreementEndDate").val());
        frmServiceProvider.append("SelectedServiceOpted", strServices.substring(0, strServices.length - 1));
        frmServiceProvider.append("PriceOption", $('input[type="radio"][name="PricingOption"]:checked').val());
       // frmPrinter.append("Image1", document.getElementById("fileUpload1").value != "" ? document.getElementById("fileUpload1").files[0] : $("#hdnImage1").val());
        if (ServiceProviderFileList != null && ServiceProviderFileList.length) {
            $.each(ServiceProviderFileList, function (i, e) {
                frmServiceProvider.append(e.FileName, e.File);
            });
        }

        var pUrl = "/Admin/Service/SaveServiceProvider/";
        $.ajax({
            type: "POST",
            url: pUrl,
            data: frmServiceProvider,
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                debugger;
                var Jdata = JSON.parse(data);
                if (Jdata.Result) {
                    alert("Service Provider is successfully " + ($("#hdnServiceProviderID").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/Service/ManageServiceProvider/";
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
    if ($('#txtFirstName').val() == "") {
        IsValid = false;
        strErrMsg += "Please enter First Name. \n";
    }
    if ($('#txtMobileNo').val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Mobile No. \n";
    }
    if ($("#txtMobileNo").val() != "" && $("#txtMobileNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Mobile No must contain minimum 8 digit. \n";
    }
    if ($("#txtPhoneNo").val() != "" && $("#txtPhoneNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Phone No must contain minimum 8 digit. \n";
    }

    if ($("#txtContactPersonNo").val() != "" && $("#txtContactPersonNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Contact Person No must contain minimum 8 digit. \n";
    }

    if ($("#txtEscalationPersonContactNo").val() != "" && $("#txtEscalationPersonContactNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Escalation Person No must contain minimum 8 digit. \n";
    }

    if ($("#txtEscalationLandlineNo").val() != "" && $("#txtEscalationLandlineNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Escalation Landline No must contain minimum 8 digit. \n";
    }
    if ($("#txtContactPersonName").val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Contact Person Name. \n";
    }

    if ($("#txtEmailId").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter EmailID. \n";
    }

    if ($("#txtCRNumber").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter CR Number. \n";
    }

    if ($("#txtCRNumber").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter CR Number. \n";
    }

    if ($("#txtAggreementStartDate").val() == "") {

        IsValid = false;
        strErrMsg += "Please Agreement Start Date. \n";
    }

    if ($("#txtAggreementEndDate").val() == "") {

        IsValid = false;
        strErrMsg += "Please Agreement End Date. \n";
    }


    if ($("#txtAggreementEndDate").val() != "" && $("#txtAggreementStartDate").val() != "" && Date.parse($("#txtAggreementEndDate").val()) <= Date.parse($("#txtAggreementStartDate").val()) ) {

        IsValid = false;
        strErrMsg += "Agreement End Date should be greater than start date.. \n";
    }

    if ($('input[name="chkService"][type="checkbox"]:checked').length == 0) {
        IsValid = false;
        strErrMsg += "Please select service opted. \n";
    }
   
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function EditServiceProvider(e) {

    $('body').data("IsLoaderRequired", true);
    $loading.show();
    setTimeout(function () {
        var ServiceProviderId = $(e).attr("data-id");
        var pUrl = "/Admin/Service/GetServiceProviderDetail?ServiceProviderId=" + ServiceProviderId;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: true,
            success: function (data) {
                debugger;
                var Jdata = JSON.parse(data);
                $("#txtContactPersonName").val(Jdata.ContactPersonName);
                $("#txtEscalationLandlineNo").val(Jdata.EscalationLandlineNo);
                $("#txtCRNumber").val(Jdata.CRNumber);
                $("#txtContactPersonNo").val(Jdata.ContactPersonContactDetail);
                $("#txtEmailId").val(Jdata.EmailId);
                $("#txtEscalationPersonContactNo").val(Jdata.EscalationContactDetail);
                $("#txtFirstName").val(Jdata.FirstName);
                $("#txtMobileNo").val(Jdata.MobileNo);
                $("#txtLastName").val(Jdata.LastName);
                $("#txtMiddleName").val(Jdata.MiddleName);
                $("#txtOfficeAddress").val(Jdata.OfficeLocationAddress);
                $("#txtPhoneNo").val(Jdata.PhoneNo);
                $("#txtEscalationEmailId").val(Jdata.Escalation_Person_EmailId);
                var AgreementFromDate = new Date(parseFloat(Jdata.ServiceProviderAgreementFromDate.substring(Jdata.ServiceProviderAgreementFromDate.indexOf('(') + 1, Jdata.ServiceProviderAgreementFromDate.indexOf(')'))));
                $("#txtAggreementStartDate").val(formatDateWithTime(AgreementFromDate));
                //$("#txtAggreementStartDate").val(new Date(new Date().getTime() - new Date().getTimezoneOffset() * 60000).toISOString());

                var AgreementEndDate = new Date(parseFloat(Jdata.ServiceProviderAgreementToDate.substring(Jdata.ServiceProviderAgreementToDate.indexOf('(') + 1, Jdata.ServiceProviderAgreementToDate.indexOf(')'))));
                $("#txtAggreementEndDate").val(formatDateWithTime(AgreementEndDate));

                $("#hdnServiceProviderID").val(Jdata.ServiceProviderId);
                //$('input[name="chkService"][type="checkbox"]').prop("checked", false);
                if (Jdata.SelectedServiceOpted != null) {

                    $('input[name="chkService"][type="checkbox"]').each(function (index) {
                        item = $(this);
                        if (Jdata.SelectedServiceOpted.indexOf(item.val()) != -1) {
                            item.attr('checked', true);
                        }
                    });
                }

                if ($("input[type=checkbox][checkboxType=service]").length > 0) {

                    $("input[type=checkbox][checkboxType=service]").each(function (i, e) {
                        if ($(e).parent().parent().find('ul').find('input[type=checkbox]').length == $(e).parent().parent().find('ul').find('input[type=checkbox]:checked').length) {
                            $(e).prop("checked", true);
                        }
                    });
                }


                $("#btnAdd").html("<strong>Update</strong>");
                $("input[type=radio][name=PricingOption][value=" + Jdata.PriceOption + "]").prop("checked", true);
                $("#progress").hide();

            },
            error: function (data) {

            }
        });
    }, 1000);
   

}

$("#fileInput").on("change", function () {
    var fileInput = document.getElementById('fileInput');
    //Iterating through each files selected in fileInput  
    for (i = 0; i < fileInput.files.length; i++) {

        var sfilename = fileInput.files[i].name;
        let srandomid = Math.random().toString(36).substring(7);

        ServiceProviderFileList.push({ FileName: sfilename, File: fileInput.files[i]});
        

        var markup = "<tr align='center' id='" + srandomid + "'><td style='height: 10px;'>" + sfilename + "</td><td style='height: 10px;'><a href='#' onclick='DeleteFile(\"" + srandomid + "\",\"" + sfilename +
            "\")'><img src='../../images/icon-delete.gif'  /></a></td></tr>"; // Binding the file name  
        $("#FilesList tbody").append(markup);

    }
    chkatchtbl();
    $('#fileInput').val('');
});  

function DeleteFile(Fileid, FileName) {
    frmBroker.delete(FileName)
    $("#" + Fileid).remove();
    chkatchtbl();
}
function chkatchtbl() {
    if ($('#FilesList tr').length > 1) {
        $("#FilesList").css("visibility", "visible");
    } else {
        $("#FilesList").css("visibility", "hidden");
    }
} 



function DeleteServiceProvider(e) {
    if (confirm("Are you sure you want to delete  Service Provider ?")) {
        var ServiceProviderId = $(e).attr("data-id");
        var pUrl = "/Admin/Service/DeleteServiceProvider?ServiceProviderId=" + ServiceProviderId;
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
                    alert("Service Provider is deleted successfully");
                    window.location.href = "/admin/Service/ManageServiceProvider/";
                }
                else {
                    if (Jdata.IsReferenceDataExist) {
                        alert("Reference Data exists.");
                    }
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



