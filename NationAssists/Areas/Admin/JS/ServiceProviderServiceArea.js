

var ServiceAreaList = [];
var objServiceArea = {};
function GetAllServices() {

    if ($("#ServiceProviderId").val() != "") {
        var ServiceProviderId = $("#ServiceProviderId").val();
        var pUrl = "/Admin/Service/BindServices?ServiceProviderId=" + ServiceProviderId;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                var jData = JSON.parse(data);
                $("#ServiceId").html(""); // clear before appending new list
                if (jData != null && jData.length > 0) {
                    $("#ServiceId").append($('<option></option>').val(0).html("--Select Services--"));
                    $.each(jData, function (i, service) {
                        $("#ServiceId").append(
                            $('<option></option>').val(service.ServiceId).html(service.ServiceName));
                    });

                    BindServiceAreaBySearch($("#ServiceProviderId").val(),"0","0");
                }
            }
        });
    }

}

function BindServiceType() {
    if ($("#ServiceId").val() != "" && $("#ServiceId").val() != "0") {
        var pUrl = "../../../RaiseServiceRequest/BindServiceType?ServiceId=" + $("#ServiceId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#ServiceSubCategoryId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#ServiceSubCategoryId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#ServiceSubCategoryId").append(
                            $('<option></option>').val(service.ServiceSubCategoryId).html(service.Name));
                    });

                 
                        BindServiceAreaBySearch($("#ServiceProviderId").val(), $("#ServiceId").val(), "0");
                       

                   
                }
            }
        });
    }
}

function BindServiceAreaBySearch(ProviderId, ServiceId, SubCategoryId) {

    var pUrl = "/Admin/Service/GetAllServiceArea?ServiceProviderId=" + ProviderId + "&ServiceId=" + ServiceId + "&SubCategoryId="+SubCategoryId;
    $.ajax({
        type: "Get",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {

           
            var jData = JSON.parse(data);
            if (jData != null && jData.ServiceAreas!=null ) {
                ServiceAreaList = [];
                $.each(jData.ServiceAreas, function (i, service) {
                    ServiceAreaList.push({
                        ServiceProviderServiceAddressId: service.ServiceProviderServiceAreaId,
                        ServiceProviderId: $("#ServiceProviderId").val(),
                        ServiceProviderName: $("#ServiceProviderId option:selected").text(),
                        ServiceId: service.ServiceId,
                        ServiceName: service.ServiceName,
                        ServiceSubCategoryId: service.ServiceSubCategoryId,
                        ServiceSubCategoryName: service.SubCategoryName,
                        CountryId: service.CountryId,
                        CountryName: service.CountryName,
                        GovernotesId: service.GovernotesId,
                        GovernoratesName: service.GovernotesName,
                        PlaceId: service.PlaceId,
                        PlaceName: service.PlaceName,
                        BlockId: service.BlockId,
                        PinCode: service.PinCode
                    });
                });

                BindServiceArea();

            }
            else {
                ServiceAreaList = [];
                BindServiceArea();
            }
        }
    });
}

function BindGovernotes() {
    if ($("#CountryId").val() != "" && $("#CountryId").val() != "0") {
        var pUrl = "../../../RaiseServiceRequest/BindGovernotes?CountryId=" + $("#CountryId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#GovernotesId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#GovernotesId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#GovernotesId").append(
                            $('<option></option>').val(service.GovernorateId).html(service.GovernoratesName));
                    });

                    $("#PlaceId").html($('<option></option>').val(0).html("--Select--"));
                    $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));

                    

                }
            }
        });
    }
}

function BindPlaces() {
    if ($("#GovernotesId").val() != "" && $("#GovernotesId").val() != "0") {
        var pUrl = "../../../RaiseServiceRequest/BindPlaces?GovernotesId=" + $("#GovernotesId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#PlaceId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#PlaceId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#PlaceId").append(
                            $('<option></option>').val(service.PlaceId).html(service.Place_Name));
                    });

                    $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));

               
                }
            }
        });
    }
}

function BindBlock() {
    if ($("#PlaceId").val() != "" && $("#PlaceId").val() != "0") {
        var pUrl = "../../../RaiseServiceRequest/BindBlocks?PlaceId=" + $("#PlaceId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#BlockCode").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#BlockCode").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#BlockCode").append(
                            $('<option></option>').val(service.BlockId).html(service.Block_Code));
                    });

                    


                }
            }
        });
    }
}

function AddServiceArea() {
    if (Validateform()) {
        var ServiceProviderServiceAddressId = 0;
        if (ServiceAreaList.length != 0) {
            ServiceProviderServiceAddressId = Math.max.apply(Math, ServiceAreaList.map(function (o) { return o.ServiceProviderServiceAddressId; }))
        }
        ServiceAreaList.push({
            ServiceProviderServiceAddressId:(ServiceProviderServiceAddressId + 1),
            ServiceProviderId: $("#ServiceProviderId").val(),
            ServiceProviderName: $("#ServiceProviderId option:selected").text(),
            ServiceId: $("#ServiceId").val(),
            ServiceName: $("#ServiceId option:selected").text(),
            ServiceSubCategoryId: $("#ServiceSubCategoryId").val(),
            ServiceSubCategoryName: $("#ServiceSubCategoryId option:selected").text(),
            CountryId: $("#CountryId").val(),
            CountryName: $("#CountryId option:selected").text(),
            GovernotesId: $("#GovernotesId").val(),
            GovernoratesName: $("#GovernotesId").val()!="0" ? $("#GovernotesId option:selected").text():"",
            PlaceId: $("#PlaceId").val(),
            PlaceName: $("#PlaceId").val()!="0" ? $("#PlaceId option:selected").text():"",
            BlockId: $("#BlockCode").val(),
            PinCode: $("#BlockCode").val()!="0"? $("#BlockCode option:selected").text():""
        });

        objServiceArea = {
            ServiceProviderId: $("#ServiceProviderId").val(),
            ServiceId: $("#ServiceId").val(),
            ServiceSubCategoryId: $("#ServiceSubCategoryId").val(),
            ServiceAreas: ServiceAreaList
        };


        BindServiceArea();


        

        $("#CountryId").val("0");
        

        $("#GovernotesId").html("");
        $("#GovernotesId").append($('<option></option>').val(0).html("--Select--"));

        $("#PlaceId").html("");
        $("#PlaceId").append($('<option></option>').val(0).html("--Select--"));

        $("#BlockCode").html("");
        $("#BlockCode").append($('<option></option>').val(0).html("--Select--"));





    }
}

function BindServiceArea() {
    if (ServiceAreaList.length > 0) {
        var table = "<table> <thead> <tr> <th>SNo.</th> <th>Service </th> <th>Service Category</th> <th> Country</th> <th>Governotes</th> <th>Place </th> <th>Block</th>  <th>Action</th></tr> </thead>";

        table += "<tbody>";
        ServiceAreaList.forEach(function (e, i) {
            var tbody = "<tr>";
            
            tbody += "<td>" + (i+1)+ "</td>";
            tbody += "<td>" + e.ServiceName + "  </td>";
            tbody += "<td>" + e.ServiceSubCategoryName + " </td>";
            tbody += "<td>" + e.CountryName + "</td>";
            tbody += "<td>" + e.GovernoratesName + " </td>";
            tbody += "<td>" + e.PlaceName + " </td>";
            tbody += "<td>" + e.PinCode + "</td>";
            tbody += "<td>              <a onclick='DeleteServiceArea(" + JSON.stringify(e) + ");'><img src='/images/icon-delete.gif' title='Delete' alt='Delete' /></a></td>";
            tbody += "</tr>";

            table += tbody;

        });

        $('#tblPriceList').html(table);
    }
    else {
        $('#tblPriceList').html('');
    }
}

function Validateform() {
    var IsValid = true;
    var strErrMsg = "";
    if ($("#ServiceProviderId").val() == "" || $("#ServiceProviderId").val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service Provider. \n";
    }

    if ($("#ServiceId").val() == "" || $("#ServiceId").val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service . \n";
    }
    if ($("#ServiceSubCategoryId").val() == "" || $("#ServiceSubCategoryId").val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service Sub Category . \n";
    }
    if ($('#CountryId').val() == "" || $('#CountryId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Country. \n";
    }

    //if ($('#CountryId').val() != "" && $('#GovernotesId').val() == "0") {
    //    IsValid = false;
    //    strErrMsg += "Please select Governotes. \n";
    //}

    //if ($('#GovernotesId').val() != "0" && $('#PlaceId').val() == "0") {
    //    IsValid = false;
    //    strErrMsg += "Please select Place. \n";
    //}
    //if ($('#PlaceId').val() != "0" && $('#BlockCode').val() == "0") {
    //    IsValid = false;
    //    strErrMsg += "Please select Block. \n";
    //}
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function SaveServiceArea() {

    if (ServiceAreaList != null && ServiceAreaList.length > 0) {

       
        var pUrl = "/Admin/Service/SaveServiceAreaMapping/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objServiceArea),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Service Area Mapped successfully ");
                    window.location.href = "/admin/Service/ManageServiceProviderServiceArea/";
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
        alert("Please add Service Area");
    }
}

function BindMappedArea() {
    if ($("#ServiceSubCategoryId").val() != "" && $("#ServiceSubCategoryId").val() != "0") {
        BindServiceAreaBySearch($("#ServiceProviderId").val(), $("#ServiceId").val(), $("#ServiceSubCategoryId").val());
    }
}

function DeleteServiceArea(e) {
    if (e != null) {
        if (confirm("Are you sure you want to delete this record ??")) {
            var filteredServiceArea = ServiceAreaList.filter(function (el) {
                return el.ServiceProviderServiceAddressId != e.ServiceProviderServiceAddressId;
            });
            ServiceAreaList = filteredServiceArea;

            BindServiceArea();

        }
    }
}

//function EditServiceArea(e) {
//    if (e != null) {
//        $("#ServiceId").val(e.ServiceId);
//        $("#CountryId").val(e.CountryId);
//        $("#hdnSubCategoryId").val(e.ServiceSubCategoryId);
//        $("#hdnGovernotesId").val(e.GovernotesId);
//        $("#hdnPlaceId").val(e.PlaceId);
//        $("#hdnBlockId").val(e.BlockId);
//        BindServiceType();
//        BindGovernotes();
//    }
//}



