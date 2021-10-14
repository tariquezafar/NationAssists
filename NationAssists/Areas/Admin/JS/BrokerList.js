var SelectedServiceIds = [];
var BrokerFileList = [];


function EditBroker(e) {

    var scrollPos = $("#dvDetail").offset().top;
    $(window).scrollTop(scrollPos);
    $('body').data("IsLoaderRequired", true);
    $loading.show();
    var BrokerId = $(e).attr("data-id");
    var pUrl = "/Admin/Brokers/GetBrokerDetail?BrokerId=" + BrokerId;
    setTimeout(function () {
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
            //var AgreementFromDate = new Date(parseFloat(Jdata.Agreement_Start_Date.substring(Jdata.Agreement_Start_Date.indexOf('(') + 1, Jdata.Agreement_Start_Date.indexOf(')'))));
            //$("#txtAggreementStartDate").val(formatDate(AgreementFromDate));
            ////$("#txtAggreementStartDate").val(new Date(new Date().getTime() - new Date().getTimezoneOffset() * 60000).toISOString());

            //var AgreementEndDate = new Date(parseFloat(Jdata.Agreement_End_Date.substring(Jdata.Agreement_End_Date.indexOf('(') + 1, Jdata.Agreement_End_Date.indexOf(')'))));
            //$("#txtAggreementEndDate").val(formatDate(AgreementEndDate));

            $("#txtAggreementStartDate").val(Jdata.Agg_Start_Date);
            $("#txtAggreementEndDate").val(Jdata.Agg_End_Date);

            $("#hdnBrokerID").val(Jdata.BrokerId);
            $("#txtRemarks").val(Jdata.Remarks);
            //$('input[name="chkService"][type="checkbox"]').prop("checked", false);
            if (Jdata.SelectedServiceOpted != null) {
                var ArrSelectedServiceOpted = Jdata.SelectedServiceOpted.split(',');
                $('input[name="chkService"][type="checkbox"]').each(function (index) {
                    item = $(this);
                    if (ArrSelectedServiceOpted.includes(item.val())) {
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
            //if (Jdata.CRExpiryDate != null && Jdata.CRExpiryDate != "") {

            //    var CRExpiryDate = new Date(parseFloat(Jdata.CRExpiryDate.substring(Jdata.CRExpiryDate.indexOf('(') + 1, Jdata.CRExpiryDate.indexOf(')'))));
            //    $("#txtCRExpiryDate").val(formatDate( CRExpiryDate));
            //}
            $("#txtCRExpiryDate").val(Jdata.CPR_Expiry_Date);
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
                    //var Commission_StartDate = new Date(parseFloat(e.Commission_StartDate.substring(e.Commission_StartDate.indexOf('(') + 1, e.Commission_StartDate.indexOf(')'))));
                    //$("#txtCommissionStartDate_" + e.ServiceId).val(formatDate(Commission_StartDate));

                    //var Commission_EndDate = new Date(parseFloat(e.Commission_EndDate.substring(e.Commission_EndDate.indexOf('(') + 1, e.Commission_EndDate.indexOf(')'))));
                    //$("#txtCommissionEndDate_" + e.ServiceId).val(formatDate(Commission_EndDate));

                    $("#txtCommissionStartDate_" + e.ServiceId).val(e.Commission_Start_Date);
                    $("#txtCommissionEndDate_" + e.ServiceId).val(e.Commission_End_Date);
                });
            }
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

//function formatDate(date) {
//    var d = new Date(date),
//        month = '' + (d.getMonth() + 1),
//        day = '' + d.getDate(),
//        year = d.getFullYear();
   


//    if (month.length < 2)
//        month = '0' + month;
//    if (day.length < 2)
//        day = '0' + day;

//    return [year, month, day].join('-');
    
//}



function SearchSource() {
    $('body').data("IsLoaderRequired", true);
    $loading.show();
    var BrokerType = $("#BrokerTypeId").val();
    var SourceName = $("#txtBrokerName").val();
    var CRNumber = $("#txtCRNumber").val();
    var MobileNo = $("#txtMobileNo").val();
    var EmailId = $("#txtEmailId").val();
    setTimeout(function () {
    var pUrl = "/Admin/Brokers/SearchSource?BrokerType=" + BrokerType + "&SourceName=" + SourceName + "&CRNumber=" + CRNumber + "&MobileNo=" + MobileNo + "&EmailId=" + EmailId;
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
                $("#tblBrokers").html(data);
                $loading.hide();
            }
            else {
                console.log(data);
                $loading.hide();
            }
        }
    });
    }, 1000);



}



