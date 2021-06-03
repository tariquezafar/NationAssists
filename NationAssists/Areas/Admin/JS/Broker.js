
var frmBroker = new FormData();
function SaveBroker() {

    if (ValidateForm()) {

        var strServices = "";
        $('input[name="chkService"][type="checkbox"]:checked').each(function (i, e) {
            strServices = strServices + $(e).val() + ","
        });

        frmBroker.append("BrokerId", $("#hdnBrokerID").val())
        frmBroker.append("Broker_Type", $("#BrokerTypeId").val());
        frmBroker.append("Broker_Name", $("#txtBrokerName").val());
        frmBroker.append("Office_Location_Address", $("#txtOfficeLocationAddress").val());
        frmBroker.append("Office_Postal_Address", $("#txtOfficePostalAddress").val());
        frmBroker.append("Contact_Person_Name", $("#txtContactPersonName").val());
        frmBroker.append("Contact_Person_Contact_No", $("#txtContactPersonNo").val());
        frmBroker.append("Escalation_Person_Name", $("#txtEscalationPersonName").val());
        frmBroker.append("Escalation_Person_Contact_No", $("#txtEscalationPersonName").val());
        frmBroker.append("EscalationPersonContactNo", $("#txtEscalationPersonContactNo").val());
        frmBroker.append("EmailId", $("#txtEmailId").val());
        frmBroker.append("MobileNo", $("#txtMobileNo").val());
        frmBroker.append("Agreement_Start_Date", $("#txtAggreementStartDate").val());
        frmBroker.append("Commission_Paybable", $("#txtCommissionPaybable").val());
        frmBroker.append("Estimated_Business_In_A_Year", $("#txtEstimatedBusinessInAYear").val());
        frmBroker.append("Payment_Terms_Credit_Terms", $("#txtPaymentAndCreditTerms").val());
        frmBroker.append("Remarks", $("#txtRemarks").val());
        frmBroker.append("Agreement_End_Date", $("#txtAggreementEndDate").val());
        frmBroker.append("SelectedServiceOpted", strServices.substring(0, strServices.length - 1));
        frmBroker.append("PriceOption", $('input[type="radio"][name="PricingOption"]:checked').val());
        frmBroker.append("IsActive", $("#chkIsActive").prop("checked"));
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
                    alert("Broker is successfully " + ($("#hdnBrokerID").val() == "0" ? "added." : "updated."));
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
            $("#txtEscalationPersonContactNo").val(Jdata.Escalation_Person_Name);
            $("#txtEscalationPersonName").val(Jdata.Escalation_Person_Contact_No);
            $("#txtBrokerName").val(Jdata.Broker_Name);
            $("#txtMobileNo").val(Jdata.MobileNo);
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
            $("#txtCommissionPaybable").val(Jdata.Commission_Paybable);

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

        frmBroker.append(sfilename, fileInput.files[i]);

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
    hour = d.getHours();
    minute = d.getMinutes();


    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    var datefiled = [year, month, day].join('-');
    return (datefiled + "T" + hour.toString() + ":" + minute.toString());
}

function DeleteBrokers(e) {
    if (confirm("Are you sure you want to delete  this Broker ?")) {
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
                    alert("Broker is deleted successfully");
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



