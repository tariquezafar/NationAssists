var ServiceProviderFileList = [];



function EditServiceProvider(e) {

    var scrollPos = $("#dvDetail").offset().top;
    $(window).scrollTop(scrollPos);
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
                $("#dvSearchDetail").hide();
                $("#btnBack").show();  
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
                //var AgreementFromDate = new Date(parseFloat(Jdata.ServiceProviderAgreementFromDate.substring(Jdata.ServiceProviderAgreementFromDate.indexOf('(') + 1, Jdata.ServiceProviderAgreementFromDate.indexOf(')'))));
                //$("#txtAggreementStartDate").val(formatDateWithTime(AgreementFromDate));
                ////$("#txtAggreementStartDate").val(new Date(new Date().getTime() - new Date().getTimezoneOffset() * 60000).toISOString());

                //var AgreementEndDate = new Date(parseFloat(Jdata.ServiceProviderAgreementToDate.substring(Jdata.ServiceProviderAgreementToDate.indexOf('(') + 1, Jdata.ServiceProviderAgreementToDate.indexOf(')'))));
                //$("#txtAggreementEndDate").val(formatDateWithTime(AgreementEndDate));

                $("#txtAggreementStartDate").val(Jdata.Agg_Start_Date);
                $("#txtAggreementEndDate").val(Jdata.Agg_End_Date);

                $("#hdnServiceProviderID").val(Jdata.ServiceProviderId);
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
                $("input[type=radio][name=PricingOption][value=" + Jdata.PriceOption + "]").prop("checked", true);
                $("#progress").hide();
                $("#dvDetail").show();
                $("input").attr("disabled", "disabled");
                if (Jdata.ServiceProviderAreas.length > 0) {
                    var table = "<table> <thead> <tr> <th>SNo.</th> <th>Service </th> <th>Service Category</th> <th> Country</th> <th>Governotes</th> <th>Place </th> <th>Block</th>  </tr> </thead>";

                    table += "<tbody>";
                    Jdata.ServiceProviderAreas.forEach(function (e, i) {
                        var tbody = "<tr>";

                        tbody += "<td>" + (i + 1) + "</td>";
                        tbody += "<td>" + e.ServiceName + "  </td>";
                        tbody += "<td>" + e.SubCategoryName + " </td>";
                        tbody += "<td>" + e.CountryName + "</td>";
                        tbody += "<td>" + e.GovernotesName + " </td>";
                        tbody += "<td>" + e.PlaceName + " </td>";
                        tbody += "<td>" + e.PinCode + "</td>";
                       
                        tbody += "</tr>";

                        table += tbody;

                    });

                    $('#tblArea').html(table);
                }
                else {
                    $('#tblArea').html('');
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

function SearchServiceProvider() {
    $('body').data("IsLoaderRequired", true);
    $loading.show();
    var CompanyName = $("#txtCompanyName").val();
    var ServiceProviderId = 0;
    var MobileNo = $("#txtSPMobileNo").val();
    var PhoneNo = $("#txtSPPhoneNo").val();
    var EmailId = $("#txtSPEmailId").val()
    var CPRNumber = $("#txtSPCRNumber").val();
    var pUrl = "/Admin/Service/BindServiceProvider?ServiceProviderId=" + ServiceProviderId + "&CompanyName=" + CompanyName + "&MobileNo=" + MobileNo + "&PhoneNo=" + PhoneNo + "&EmailId=" + EmailId + "&CRNumber=" + CPRNumber;
    $.ajax({
        type: "Get",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: true,
        success: function (data) {
            if (!IsJsonString(data)) {
                $("#tblServiceProvider").html(data);
            }

            $loading.hide();
        },
        error: function (data) {

        }
    });
}



