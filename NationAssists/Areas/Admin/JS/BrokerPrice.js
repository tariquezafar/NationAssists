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

function GetAllServices() {

    if ($("#BrokerId").val() != "") {
        var BrokerId = $("#BrokerId").val();
        var pUrl = "/Admin/Brokers/BindServiceOptedForBroker?BrokerId=" + BrokerId;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                $('.pb').hide();
                $('#tblPriceList').html('');
                var jData = JSON.parse(data);
                if (jData.BrokerId != null) {
                    //var AgreementFromDate = new Date(parseFloat(jData.Agreement_Start_Date.substring(jData.Agreement_Start_Date.indexOf('(') + 1, jData.Agreement_Start_Date.indexOf(')'))));
                    //$("#hdnAgreementStartDate").val(formatDate(AgreementFromDate));
                    ////$("#txtAggreementStartDate").val(new Date(new Date().getTime() - new Date().getTimezoneOffset() * 60000).toISOString());
                    //$("#txtAgreementStartDate").val($("#hdnAgreementStartDate").val());
                    //var AgreementEndDate = new Date(parseFloat(jData.Agreement_End_Date.substring(jData.Agreement_End_Date.indexOf('(') + 1, jData.Agreement_End_Date.indexOf(')'))));
                    //$("#hdnAgreementEndDate").val(formatDate(AgreementEndDate));
                    //$("#txtAgreementEndDate").val($("#hdnAgreementEndDate").val());
                    $("#hdnAgreementStartDate").val(jData.Agg_Start_Date);
                    $("#txtAgreementStartDate").val($("#hdnAgreementStartDate").val());
                    $("#hdnAgreementEndDate").val(jData.Agg_End_Date);
                    $("#txtAgreementEndDate").val($("#hdnAgreementEndDate").val());
                    $(".Ad").show();
                    $("#ServiceId").html(""); // clear before appending new list
                    if (jData.BrokerOptedServices != null && jData.BrokerOptedServices.length > 0) {
                        $("#ServiceId").append($('<option></option>').val(0).html("--Select Services--"));
                        $.each(jData.BrokerOptedServices, function (i, service) {
                            $("#ServiceId").append(
                                $('<option></option>').val(service.ServiceId).html(service.Name));
                        });
                    }
                }
            }
        });
    }

}

function BindBrokerPriceList() {
    if ($("#BrokerId").val() != "-1" && $("#ServiceId").val() != "0") {

        var pUrl = "/Admin/Brokers/ShowBrokerPriceList?BrokerId=" + $("#BrokerId").val() + "&ServiceId=" + $("#ServiceId").val();
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
                    $("input[type=radio][name=PricingOption][value=SB]").prop("checked", true);
                    $('.pb').hide()
                    $("#tblPriceList").html(data);
                }
                else {

                    var JData = JSON.parse(data);
                    if (JData.Error == "") {

                        var objServiceCategoryPrice = JData.ServiceProviderPrice;

                        $("input[type=radio][name=PricingOption][value=" + objServiceCategoryPrice.PriceOption + "]").prop("checked", true);
                        $('.pb').show()

                        if (JData.ServiceProviderPrice.BrokerPriceList != null && JData.ServiceProviderPrice.BrokerPriceList.length > 0) {

                            var table = "<table> <thead> <tr> <th>SNo.</th> <th>Price</th> <th> Start Date</th> <th>End Date</th> <th>Created Date </th> <th>Status</th> </tr> </thead>";

                            table += "<tbody>";
                            JData.ServiceProviderPrice.BrokerPriceList.forEach(function (e, i) {
                                var tbody = "<tr>";
                                var StarDate = formatDateWithAMPM(parseFloat(e.StartDate.substring(e.StartDate.indexOf('(') + 1, e.StartDate.indexOf(')'))));
                                var EndDate = formatDateWithAMPM(parseFloat(e.EndDate.substring(e.EndDate.indexOf('(') + 1, e.EndDate.indexOf(')'))));
                                var CreatedDate = formatDateWithAMPM(parseFloat(e.Created_Date.substring(e.Created_Date.indexOf('(') + 1, e.Created_Date.indexOf(')'))));
                                tbody += "<td>" + (i + 1) + "</td>";

                                tbody += "<td>" + e.Price + "</td>";
                                tbody += "<td>" + StarDate + "</td>";
                                tbody += "<td>" + EndDate + "</td>";
                                tbody += "<td>" + CreatedDate + "</td>";
                                tbody += "<td>" + (e.IsActive == true ? "Active" : "In Active") + "</td>";
                                tbody += "</tr>";

                                table += tbody;

                            });

                            $('#tblPriceList').html(table);

                        }
                        else {
                            $('#tblPriceList').html('');
                        }

                    }

                }

            }
        });
    }
}

function SaveBrokerPrice() {

    if (ValidateForm()) {

        var SubCategoryPriceList = [];
        if ($("input[type=radio][name=PricingOption]:checked").val() == "SB")
            $("#tblServicePriceList tr").each(function (i, e) {
                var ServiceSubCategoryPrice = {
                    SubCategoryId: $(e).find('input[id="SubCategoryId"]').val(),
                    Price: $(e).find('input[name="txtPrice"]').val(),
                    StartDate: $(e).find('input[name="txtStartDate"]').val(),
                    EndDate: $(e).find('input[name="txtEndDate"]').val()
                };

                SubCategoryPriceList.push(ServiceSubCategoryPrice);

            });
        else {
            SubCategoryPriceList.push({

                Price: $("#txtPrice").val(),
                StartDate: $("#txtStartDate").val(),
                EndDate: $("#txtEndDate").val()
            });
        }


        var objServiceSubCategoryPrice = {
            BrokerId: $("#BrokerId").val(),
            ServiceId: $("#ServiceId").val(),
            BrokerPriceList: SubCategoryPriceList
        };



        var pUrl = "/Admin/Brokers/SaveBrokerPrice/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objServiceSubCategoryPrice),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Price Save successfully ");
                    window.location.href = "/admin/Brokers/ManageBrokerPrice/";
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

    if ($("input[type=radio][name=PricingOption]:checked").val() == "SB" && $("#tblServicePriceList tr").length > 0) {
        $("#tblServicePriceList tr").each(function (i, e) {
            if ($(e).find('input[name="txtPrice"]').val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Price for Row No " + (i + 1) + ". \n";
            }
            if ($(e).find('input[name="txtStartDate"]').val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Start Date for Row No " + (i + 1) + ". \n";
            }

            if ($(e).find('input[name="txtEndDate"]').val() == "") {
                IsValid = false;
                strErrMsg += "Please enter End Date for Row No " + (i + 1) + ". \n";
            }
            if ($(e).find('input[name="txtStartDate"]').val() != "" && $("#hdnAgreementStartDate").val() != "" && $("#hdnAgreementEndDate").val() != "" && ((Date.parse($(e).find('input[name="txtStartDate"]').val()) <= Date.parse($("#hdnAgreementStartDate").val())) || (Date.parse($(e).find('input[name="txtStartDate"]').val()) > Date.parse($("#hdnAgreementEndDate").val())))) {
                IsValid = false;
                strErrMsg += "Start Date is less than Source Agreement start date  or greater than Source Agreement End date for Row No.  " + (i + 1) + ". \n";
            }

            if ($(e).find('input[name="txtEndDate"]').val() != "" && $("#hdnAgreementStartDate").val() != "" && $("#hdnAgreementEndDate").val() != "" && ((Date.parse($(e).find('input[name="txtEndDate"]').val()) <= Date.parse($("#hdnAgreementStartDate").val())) || (Date.parse($(e).find('input[name="txtEndDate"]').val()) > Date.parse($("#hdnAgreementEndDate").val())))) {
                IsValid = false;
                strErrMsg += "End Date is less than Source Agreement start date  or greater than Source Agreement End date  for Row No.  " + (i + 1) + ". \n";
            }
            if ($(e).find('input[name="txtEndDate"]').val() != "" && $(e).find('input[name="txtStartDate"]').val() != "" && Date.parse($(e).find('input[name="txtEndDate"]').val()) <= Date.parse($(e).find('input[name="txtStartDate"]').val())) {

                IsValid = false;
                strErrMsg += "End Date should be greater than start date for Row No " + (i + 1) + " \n";
            }
        });
    }

    if ($("input[type=radio][name=PricingOption]:checked").val() == "PB") {
        if ($("#txtPrice").val() == "") {
            IsValid = false;
            strErrMsg += "Please enter Price. \n";
        }
        if ($("#txtStartDate").val() == "") {
            IsValid = false;
            strErrMsg += "Please select start date. \n";
        }
        if ($("#txtEndDate").val() == "") {
            IsValid = false;
            strErrMsg += "Please select end date.";
        }
        if ($("#txtStartDate").val() != "" && $("#hdnAgreementStartDate").val() != "" && $("#hdnAgreementEndDate").val() != "" && ((Date.parse($("#txtStartDate").val()) <= Date.parse($("#hdnAgreementStartDate").val())) || (Date.parse($("#txtStartDate").val()) > Date.parse($("#hdnAgreementEndDate").val())))) {
            IsValid = false;
            strErrMsg += "Start Date is less than Source Agreement start date  or greater than Source Agreement End date . \n";
        }

        if ($("#txtEndDate").val() != "" && $("#hdnAgreementStartDate").val() != "" && $("#hdnAgreementEndDate").val() != "" && ((Date.parse($("#txtEndDate").val()) <= Date.parse($("#hdnAgreementStartDate").val())) || (Date.parse($("#txtEndDate").val()) > Date.parse($("#hdnAgreementEndDate").val())))) {
            IsValid = false;
            strErrMsg += "End Date is  less than Source Agreement start date  or greater than Source Agreement End date . \n";
        }
        if ($("#txtEndDate").val() != "" && $("#txtStartDate").val() != "" && Date.parse($("#txtAggreementEndDate").val()) <= Date.parse($("#txtStartDate").val())) {

            IsValid = false;
            strErrMsg += "End Date should be greater than start date.. \n";
        }

    }

    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}




