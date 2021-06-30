$(".regNextStep").click(function () {

    if ($("#txtEmailId").val() == "") {
        alert("Please enter  Email Id");
        $("#txtEmailId").focus();
    }
    else if ($("#txtEmailId").val() != "" && isValidEmail($("#txtEmailId").val()) == false)
    {
        alert("Please enter a valid Email Id");
        $("#txtEmailId").focus();
    }
    else if ($("#txtPassword").val() == "") {
        alert("Please enter  Password");
        $("#txtPassword").focus();
    }
    else if ($("#txtPassword").val() != "" && ($("#txtPassword").val() != $("#txtConfirmPassword").val())) {
        alert("Password and confirmd must be same");
        $("#txtConfirmPassword").focus();
    }
    else {
        $('.ci').removeClass('active');
        $('.gi').addClass('active');

        $('#dvCredentails').hide(0);
        $('#generalInfo').show(0);
    }

});
//biNextStep

$(".biNextStep").click(function () {

    if ($("#txtFirstName").val() == "") {
        alert("Please enter First Name");
        $("#txtEmailId").focus();
    }
    else if ($("#ddlGender").val() == "0") {
        alert("Please select Gender");
        $("#ddlGender").focus();
    }
    else if ($("#txtNationalId").val() == "") {
        alert("Please enter National Id");
        $("#txtNationalId").focus();
    }
    else if ($("#txtMobileNo").val() == "") {
        alert("Please enter Mobile No");
        $("#txtMobileNo").focus();
    }
    else if ($("#txtMobileNo").val() != "" && $("#txtMobileNo").val().length<8) {
        alert("Mobile No contain minimum of 8 digits.");
        $("#txtMobileNo").focus();
    }
    else {
        $('.gi').removeClass('active');
        $('.bi').addClass('active');

        $('#generalInfo').hide(0);
        $('#businessInfo').show(0);
    }
});

$(".mtNextStep").click(function () {
    if ($("#ddlAccountType").val() == "0") {

        alert("Please select Account Type.");
        $("#ddlAccountType").focus();
    }
    else {
        if ($("#ddlAccountType").val() == "GA") {

            SubmitRegistration('', 0,'');

        }
        else {
            $('.bi').removeClass('active');
            $('.mt').addClass('active');

            $('#dvMemberType').show(0);
            $('#businessInfo').hide(0);
        }
    }

});

$(".mdNextStep").click(function () {


    if ($("#ddlSourceType").val() != "0") {
        $('.mt').removeClass('active');
        $('.bi').hide(0);
        $('.md').show(0);
        $('.md').addClass('active');

        $('#dvNAMemberDetail').show(0);
        $('#dvMemberType').hide(0);
    }
    else {
        alert("Please select Service Provider.");
        $("#ddlSourceType").focus();
    }
     

});

$('.bkci').click(function () {
    $('.ci').addClass('active');
    $('.gi').removeClass('active');

    $('#dvCredentails').show(0);
    $('#generalInfo').hide(0);
})

$('.bkgi').click(function () {

    $('.gi').addClass('active');
    $('.bi').removeClass('active');

    $('#businessInfo').hide(0);
    $('#generalInfo').show(0);
})
$('.bkbi').click(function () {

    $('.bi').addClass('active');
    $('.mt').removeClass('active');

    $('#dvMemberType').hide(0);
    $('#businessInfo').show(0);
})

$('.bkmt').click(function () {

    $('.mt').addClass('active');
    $('.bi').show(0);
    $('.md').hide(0);
    

    $('#dvNAMemberDetail').hide(0);
    $('#dvMemberType').show(0);
})



$("#ddlAccountType").change(function () {
    var ddf = "";
    if ($(this).val() != "0") {

        if ($(this).val() == "GA") {
            $('.mt').hide(0);
            
            $('.mtNextStep').html('Finish');

        } else {
            $('.mt').show(0);
            $('.mtNextStep').html('Next');
        }
    }
})

$("#ddlSourceType").change(function () {

    if ($(this).val() != "0") {

      
        var UserType = $(this).val();
        $("#ddlSource").html("'<option>--Select--</option>'");

        var pUrl = "../Admin/Brokers/BindBroker?UserType=" + UserType;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#ddlSource").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#ddlSource").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#ddlSource").append(
                            $('<option></option>').val(service.ServiceProviderId).html(service.FirstName));
                    });

                }
            }
        });

    }
})




function SubmitRegistration(AccountSubType, BrokerId, Customer_Reference_Code) {
    var objUsers = {
        CustomerId: 0,
        FirstName: $("#txtFirstName").val(),
        LastName: $("#txtLastName").val(),
        EmailId: $("#txtEmailId").val(),
        MobileNo: $("#txtMobileNo").val(),
        NationalId: $("#txtNationalId").val(),
        Gender: $("#ddlGender").val(),
        BrokerId: BrokerId,
        AccountType: $("#ddlAccountType").val(),
        AccountSubType: AccountSubType,
        Password: $("#txtPassword").val(),
        Customer_Reference_Code:Customer_Reference_Code
    };
    var pUrl = "/Register/SaveCustomer/";
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        url: pUrl,
        data: JSON.stringify(objUsers),
        success: function (data) {
            debugger;

            if (data.Result) {
                alert("Registration is completed. Please verify your email.");
                window.location.href = "/Home";
            }
            else {
                if (data.DuplicateEmail) {
                    alert("Email already exists.");
                }
                else if (data.DuplicateMobile) {
                    alert("Mobile No already exists.");
                }
                else {
                    alert("Opps! some error occured.");
                }
            }
        },
        error: function (data) {
        }
    });
}


$("#btnFinish").click(function () {
    if ($('#ddlSource').val() != "" || $('#ddlSource').val() != "0") {
        var Reference = $("#ddlSource option:selected").text();
        var ReferenceCode=    Reference.substring(Reference.indexOf('(') + 1, Reference.indexOf(')'))
        var pUrl = "/Register/SearchCPRNumber?CPRNumber=" + $("#txtNationalId").val() + "&SourceId=" + $('#ddlSource').val();
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            success: function (data) {

                if (data.ErrorMessage == null || data.ErrorMessage == "") {
                    if (data.IsValidCPR && data.IsCPRMatchesWithSource) {
                        SubmitRegistration($("#ddlSourceType").val(), $('#ddlSource').val(), ReferenceCode);

                    }
                    else {
                        if (data.IsValidCPR && !data.IsCPRMatchesWithSource) {

                            alert("CPR Number doesnot matched with Source.");
                        }
                        else {
                            alert("Invalid CPR Number.");
                        }

                    }
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
        alert("Please select source");
    }
})


