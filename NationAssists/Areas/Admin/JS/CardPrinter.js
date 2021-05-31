//var _CardPrinter = function () {
//    this.btnAdd = $("#btnAdd");
    
//};

//_CardPrinter.prototype.BindEvents = function () {
//    var h = this;
    



//    h.btnAdd.live("click", function () {

//        if (ValidateForm())
//        {
           
//        }
//    });
//    /* close the Form.*/
//    h.btnClose.live("click", function () {
//        Loading();
//        alertify.confirm("Are You Sure You Want to Close the Form ?", function (e) {
//            if (e) {
//                window.location.href = "/Index.aspx";
//            }
//        });
//        $('#progress').hide();
//    });
//    /* Reset the Form in Last Saved State.*/
//    h.btnReset.live("click", function () {
//        Loading();
//        alertify.confirm("Are You Sure You Want to Reset the Form ?", function (e) {
//            if (e) {
//                window.location.reload(true);
//            }
//        });
//        $('#progress').hide();
//    });

    


//};


//var _Handler;
//$(document).ready(function () {
//    _Handler = new _CardPrinter();
//    _Handler.BindEvents();
//});


 function AddPrinter() {
    
    if (ValidateForm()) {

        var frmPrinter = new FormData();
        frmPrinter.append("PrinterID", $("#hdnPrinterID").val())
        frmPrinter.append("Name",$("#TxtName").val());
        frmPrinter.append("BrandID", $("#BrandId").val());
        frmPrinter.append("CapabilityID", $("#PrinterCapabilityId").val());
        frmPrinter.append("Desciption", $("#txtDescription").val());
        frmPrinter.append("Price", $("#txtPrice").val());
        frmPrinter.append("Image1",document.getElementById("fileUpload1").value!=""?  document.getElementById("fileUpload1").files[0]:$("#hdnImage1").val());
        frmPrinter.append("Image2", document.getElementById("fileUpload2").value != "" ? document.getElementById("fileUpload2").files[0] : $("#hdnImage2").val());
        frmPrinter.append("Image3", document.getElementById("fileUpload3").value != "" ? document.getElementById("fileUpload3").files[0] : $("#hdnImage3").val());
        frmPrinter.append("Image4", document.getElementById("fileUpload4").value != "" ? document.getElementById("fileUpload4").files[0] : $("#hdnImage4").val());
        frmPrinter.append("IsActive", $("#chkIsActive").prop("checked"));


        var pUrl = "/Admin/CardPrinter/SaveCardPrinter/";
        $.ajax({
            type: "POST",
            url: pUrl,
            data: frmPrinter,
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                debugger;
                var Jdata = JSON.parse(data);
                if (Jdata.Result)
                {
                    alert("Printer is successfully " +( $("#hdnPrinterID").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/cardprinter/";
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


function DeletecardPrinter(e)
{
    if (confirm("Are you sure you want to delete card Printer?"))
    {
        var PrinterID = $(e).attr("data-id");
        var pUrl = "/Admin/CardPrinter/DeleteCardPrinter?PrinterID="+PrinterID;
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
                    alert("Printer is deleted successfully");
                    window.location.href = "/admin/cardprinter/";
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

function ValidateForm()
{
    var IsValid = true;
    var strErrMsg = "";
    if ($('#BrandId').val() == "")
    {
        IsValid = false;
        strErrMsg += "Please select Brand . \n";
    }
    if ($('#PrinterCapabilityId').val() == "")
    {
        IsValid = false;
        strErrMsg += "Please select Printer Capability. \n";
    }

    if ($("#TxtName").val() == "")
    {
        IsValid = false;
        strErrMsg += "Please enter Printer Name. \n";
    }

    if ($("#txtPrice").val() == "")
    {

        IsValid = false;
        strErrMsg += "Please enter Printer Price. \n";
    }


    if ($("#hdnPrinterID").val()=="0" && $("#fileUpload1").val() == "" && $("#fileUpload2").val() == "" && $("#fileUpload3").val() == "" && $("#fileUpload4").val() == "")
    {
        IsValid = false;
        strErrMsg += "Please upload atleast one Printer Image.";
    }

    if (IsValid)
    {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}