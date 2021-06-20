var SelectedServiceIds = [];
var BrokerFileList = [];

function SaveBroker() {

    var frmBroker = new FormData();
    if (ValidateForm()) {

        var strServices = "";
        $('input[name="chkService"][type="checkbox"]:checked').each(function (i, e) {
            strServices = strServices + $(e).val() + ","
        });

        var BrokerServiceCommissionPayableList = [];

        if (SelectedServiceIds.length > 0) {
            
            $.each(SelectedServiceIds, function (x, y) {
                BrokerServiceCommissionPayableList.push({
                    ServiceId: y.ServiceId,
                    Commission_Paybable: $("#txtCommissionPaybable_" + y.ServiceId).val(),
                    Commission_StartDate: $("#txtCommissionStartDate_" + y.ServiceId).val(),
                    Commission_EndDate: $("#txtCommissionEndDate_" + y.ServiceId).val()
                });

            });
            
        }


        frmBroker.append("BrokerId", $("#hdnBrokerID").val())
        frmBroker.append("Broker_Type", $("#BrokerTypeId").val());
        frmBroker.append("Broker_Name", $("#txtBrokerName").val());
        frmBroker.append("Office_Location_Address", $("#txtOfficeLocationAddress").val());
        frmBroker.append("Office_Postal_Address", $("#txtOfficePostalAddress").val());
        frmBroker.append("Contact_Person_Name", $("#txtContactPersonName").val());
        frmBroker.append("Contact_Person_Contact_No", $("#txtContactPersonNo").val());
        frmBroker.append("Escalation_Person_Name", $("#txtEscalationPersonName").val());
        frmBroker.append("Escalation_Person_Contact_No", $("#txtEscalationPersonContactNo").val());
        frmBroker.append("EmailId", $("#txtEmailId").val());
        frmBroker.append("Password", $("#txtPassword").val());
        frmBroker.append("MobileNo", $("#txtMobileNo").val());
        frmBroker.append("Agreement_Start_Date", $("#txtAggreementStartDate").val());
        frmBroker.append("Estimated_Business_In_A_Year", $("#txtEstimatedBusinessInAYear").val());
        frmBroker.append("Payment_Terms_Credit_Terms", $("#txtPaymentAndCreditTerms").val());
        frmBroker.append("Remarks", $("#txtRemarks").val());
        frmBroker.append("Agreement_End_Date", $("#txtAggreementEndDate").val());
        frmBroker.append("SelectedServiceOpted", strServices.substring(0, strServices.length - 1));
        frmBroker.append("Price_Option", $('input[type="radio"][name="PricingOption"]:checked').val());
        frmBroker.append("IsActive", $("#chkIsActive").prop("checked"));
        frmBroker.append("CRNumber", $("#txtCRNumber").val());
        frmBroker.append("CRExpiryDate", $("#txtCRExpiryDate").val());
        frmBroker.append("VATRegistrationNumber", $("#txtVATRegistrationNumber").val());
        frmBroker.append("Landline", $("#txtLandlineNo").val());
        frmBroker.append("EscalationLandlineNo", $("#txtEscalationLandlineNo").val());
        frmBroker.append("Escalation_Person_EmailId", $("#txtEscalationEmailId").val());
        frmBroker.append("DeclarationPeriod", $("#txtDeclarationPeriod").val());
        frmBroker.append("BranchLocation", $("#txtBranchLocation").val());
        frmBroker.append("BrokerCommissionList", JSON.stringify(BrokerServiceCommissionPayableList));
        if (BrokerFileList != null && BrokerFileList.length) {
            $.each(BrokerFileList, function (i, e) {
                frmBroker.append(e.FileName, e.File);
            });
        }
        
        var pUrl = "/Admin/Brokers/SaveBroker/";
        $.ajax({
            type: "POST",
            url: pUrl,
            data: frmBroker,
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                debugger;
                var Jdata = JSON.parse(data);
                if (Jdata.Result) {
                    alert("Source is successfully " + ($("#hdnBrokerID").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/Brokers/ManageBroker/";
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
    if ($('#BrokerTypeId').val() == "") {
        IsValid = false;
        strErrMsg += "Please select Broker Type. \n";
    }
    if ($('#txtBrokerName').val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Broker Name. \n";
    }
    if ($('#txtMobileNo').val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Mobile No. \n";
    }
    if ($("#txtLandlineNo").val() != "" && $("#txtLandlineNo").val().length < 8) {

        IsValid = false;
        strErrMsg += "Landline No must contain minimum 8 digit. \n";
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

    if ($("#txtEmailId").val() == "") {

        IsValid = false;
        strErrMsg += "Please enter EmailID. \n";
    }

    if ($("#txtAggreementStartDate").val() == "") {

        IsValid = false;
        strErrMsg += "Please Agreement Start Date. \n";
    }

    if ($("#txtAggreementEndDate").val() == "") {

        IsValid = false;
        strErrMsg += "Please Agreement End Date. \n";
    }


    if ($("#txtAggreementEndDate").val() != "" && $("#txtAggreementStartDate").val() != "" && Date.parse($("#txtAggreementEndDate").val()) <= Date.parse($("#txtAggreementStartDate").val())) {

        IsValid = false;
        strErrMsg += "Agreement End Date should be greater than start date.. \n";
    }

    if ($('input[name="chkService"][type="checkbox"]:checked').length == 0) {
        IsValid = false;
        strErrMsg += "Please select service opted. \n";
    }

    if ($('input[name="chkService"][type="checkbox"]:checked').length > 0) {
        SelectedServiceIds = [];

        $('input[name="chkService"][type="checkbox"]:checked').each(function (i, e) {
            if (SelectedServiceIds.filter(x => x.ServiceId == $(e).attr("service-id")).length == 0) {
                SelectedServiceIds.push({ ServiceId: $(e).attr("service-id"), ServiceName: $(e).attr("service-name") });
            }
        });
        $.each(SelectedServiceIds, function (x, y) {

            if ($("#txtCommissionPaybable_" + y.ServiceId).val() == "0" || $("#txtCommissionPaybable_" + y.ServiceId).val() == "") {

                IsValid = false;
                strErrMsg += "Please enter Commission Payable for " + y.ServiceName + " .\n";
            }

            if ($("#txtCommissionStartDate_" + y.ServiceId).val() == "") {

                IsValid = false;
                strErrMsg += "Please enter Commission Start Date for " + y.ServiceName + " .\n";
            }


            if ($("#txtCommissionEndDate_" + y.ServiceId).val() == "") {

                IsValid = false;
                strErrMsg += "Please enter Commission Start Date for " + y.ServiceName + " .\n";
            }
        });

    }
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function EditBroker(e) {


    var BrokerId = $(e).attr("data-id");
    var pUrl = "/Admin/Brokers/GetBrokerDetail?BrokerId=" + BrokerId;
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
            $("#txtContactPersonName").val(Jdata.Contact_Person_Name);
            $("#BrokerTypeId").val(Jdata.Broker_Type);
            $("#txtContactPersonNo").val(Jdata.Contact_Person_Contact_No);
            $("#txtEmailId").val(Jdata.EmailId);
            $("#txtEscalationPersonContactNo").val(Jdata.Escalation_Person_Contact_No);
            $("#txtEscalationPersonName").val(Jdata.Escalation_Person_Name);
            $("#txtBrokerName").val(Jdata.Broker_Name);
            $("#txtMobileNo").val(Jdata.MobileNo);
            $("#txtEscalationEmailId").val(Jdata.Escalation_Person_EmailId);
            $("#txtEstimatedBusinessInAYear").val(Jdata.Estimated_Business_In_A_Year);
            $("#txtPaymentAndCreditTerms").val(Jdata.Payment_Terms_Credit_Terms);
            $("#txtOfficeLocationAddress").val(Jdata.Office_Location_Address);
            $("#txtOfficePostalAddress").val(Jdata.Office_Postal_Address);
            $("#txtPhoneNo").val(Jdata.PhoneNo);
            var AgreementFromDate = new Date(parseFloat(Jdata.Agreement_Start_Date.substring(Jdata.Agreement_Start_Date.indexOf('(') + 1, Jdata.Agreement_Start_Date.indexOf(')'))));
            $("#txtAggreementStartDate").val(formatDate(AgreementFromDate));
            //$("#txtAggreementStartDate").val(new Date(new Date().getTime() - new Date().getTimezoneOffset() * 60000).toISOString());

            var AgreementEndDate = new Date(parseFloat(Jdata.Agreement_End_Date.substring(Jdata.Agreement_End_Date.indexOf('(') + 1, Jdata.Agreement_End_Date.indexOf(')'))));
            $("#txtAggreementEndDate").val(formatDate(AgreementEndDate));

            $("#hdnBrokerID").val(Jdata.BrokerId);
            $("#txtRemarks").val(Jdata.Remarks);
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
            $("input[type=radio][name=PricingOption][value=" + Jdata.Price_Option + "]").prop("checked", true);
            $("#chkIsActive").prop("checked", Jdata.IsActive);
            
            $("#txtCRNumber").val(Jdata.CRNumber);
            if (Jdata.CRExpiryDate != null && Jdata.CRExpiryDate != "") {

                var CRExpiryDate = new Date(parseFloat(Jdata.CRExpiryDate.substring(Jdata.CRExpiryDate.indexOf('(') + 1, Jdata.CRExpiryDate.indexOf(')'))));
                $("#txtCRExpiryDate").val(formatDate( CRExpiryDate));
            }
            
            $("#txtVATRegistrationNumber").val(Jdata.VATRegistrationNumber);
            $("#txtLandlineNo").val(Jdata.Landline);
            $("#txtEscalationLandlineNo").val(Jdata.EscalationLandlineNo);
            $("#txtDeclarationPeriod").val(Jdata.DeclarationPeriod);
            $("#txtBranchLocation").val(Jdata.BranchLocation);
            var lstBrokerServiceCommissionPayable = [];
            lstBrokerServiceCommissionPayable = Jdata.lstBrokerServiceCommissionPayable;
            if (lstBrokerServiceCommissionPayable.length > 0) {
                $.each(lstBrokerServiceCommissionPayable, function (i, e) {
                    $("#txtCommissionPaybable_" + e.ServiceId).val(e.Commission_Paybable);
                    var Commission_StartDate = new Date(parseFloat(e.Commission_StartDate.substring(e.Commission_StartDate.indexOf('(') + 1, e.Commission_StartDate.indexOf(')'))));
                    $("#txtCommissionStartDate_" + e.ServiceId).val(formatDate(Commission_StartDate));

                    var Commission_EndDate = new Date(parseFloat(e.Commission_EndDate.substring(e.Commission_EndDate.indexOf('(') + 1, e.Commission_EndDate.indexOf(')'))));
                    $("#txtCommissionEndDate_" + e.ServiceId).val(formatDate(Commission_EndDate));
                    
                });
            }
        },
        error: function (data) {
        }
    });

}

$("#fileInput").on("change", function () {
    var fileInput = document.getElementById('fileInput');
    //Iterating through each files selected in fileInput  
    for (i = 0; i < fileInput.files.length; i++) {

        var sfilename = fileInput.files[i].name;
        let srandomid = Math.random().toString(36).substring(7);

        BrokerFileList.push({
           FileName: sfilename, File: fileInput.files[i]
        });

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

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
   


    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
    
}

function DeleteBrokers(e) {
    if (confirm("Are you sure you want to delete  this Source ?")) {
        var BrokerId = $(e).attr("data-id");
        var pUrl = "/Admin/Brokers/DeleteBroker?BrokerId=" + BrokerId;
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
                    alert("Source is deleted successfully");
                    window.location.href = "/admin/Brokers/ManageBroker/";
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



