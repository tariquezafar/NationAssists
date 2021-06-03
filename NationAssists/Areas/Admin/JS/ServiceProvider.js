
var frmBroker = new FormData();
function SaveServiceProvider() {

    if (ValidateForm()) {

        var strServices = "";
        $('input[name="chkService"][type="checkbox"]:checked').each(function (i,e) {
            strServices = strServices + $(e).val() + ","
        });
        
        frmBroker.append("ServiceProviderId", $("#hdnServiceProviderID").val())
        frmBroker.append("FirstName", $("#txtFirstName").val());
        frmBroker.append("MiddleName", $("#txtMiddleName").val());
        frmBroker.append("LastName", $("#txtLastName").val());
        frmBroker.append("MobileNo", $("#txtMobileNo").val());
        frmBroker.append("PhoneNo", $("#txtPhoneNo").val());
        frmBroker.append("ContactPersonName", $("#txtContactPersonName").val());
        frmBroker.append("ContactPersonNo", $("#txtContactPersonNo").val());
        frmBroker.append("EscalationPersonName", $("#txtEscalationPersonName").val());
        frmBroker.append("EscalationPersonContactNo", $("#txtEscalationPersonContactNo").val());
        frmBroker.append("EmailId", $("#txtEmailId").val());
        frmBroker.append("CRNumber", $("#txtCRNumber").val());
        frmBroker.append("OfficeLocationAddress", $("#txtOfficeAddress").val());
        frmBroker.append("ServiceProviderAgreementFromDate", $("#txtAggreementStartDate").val());
        frmBroker.append("ServiceProviderAgreementToDate", $("#txtAggreementEndDate").val());
        frmBroker.append("SelectedServiceOpted", strServices.substring(0, strServices.length - 1));
        frmBroker.append("PriceOption", $('input[type="radio"][name="PricingOption"]:checked').val());
       // frmPrinter.append("Image1", document.getElementById("fileUpload1").value != "" ? document.getElementById("fileUpload1").files[0] : $("#hdnImage1").val());


        var pUrl = "/Admin/Service/SaveServiceProvider/";
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


    var ServiceProviderId = $(e).attr("data-id");
    var pUrl = "/Admin/Service/GetServiceProviderDetail?ServiceProviderId=" + ServiceProviderId;
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
            $("#txtContactPersonName").val(Jdata.ContactPersonName);
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
            var AgreementFromDate = new Date(parseFloat(Jdata.ServiceProviderAgreementFromDate.substring(Jdata.ServiceProviderAgreementFromDate.indexOf('(') + 1, Jdata.ServiceProviderAgreementFromDate.indexOf(')'))));
            $("#txtAggreementStartDate").val(formatDate(AgreementFromDate));
            //$("#txtAggreementStartDate").val(new Date(new Date().getTime() - new Date().getTimezoneOffset() * 60000).toISOString());

            var AgreementEndDate = new Date(parseFloat(Jdata.ServiceProviderAgreementToDate.substring(Jdata.ServiceProviderAgreementToDate.indexOf('(')+1, Jdata.ServiceProviderAgreementToDate.indexOf(')'))));
            $("#txtAggreementEndDate").val(formatDate(AgreementEndDate));

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

                $("input[type=checkbox][checkboxType=service]").each(function (i,e) {
                    if ($(e).parent().parent().find('ul').find('input[type=checkbox]').length == $(e).parent().parent().find('ul').find('input[type=checkbox]:checked').length) {
                        $(e).prop("checked", true);
                    }
                });
            }

            
            $("#btnAdd").html("<strong>Update</strong>");
            $("input[type=radio][name=PricingOption][value=" + Jdata.PriceOption+"]").prop("checked",true);
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
    return ( datefiled + "T" + hour.toString() + ":" + minute.toString());
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



