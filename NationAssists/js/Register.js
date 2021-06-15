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
    else if ($("#txtNationality").val() == "") {
        alert("Please enter Nationality");
        $("#txtNationality").focus();
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
    $('.bi').removeClass('active');
    $('.mt').addClass('active');

    $('#dvMemberType').show(0);
    $('#businessInfo').hide(0);

});

$(".mdNextStep").click(function () {
    $('.mt').removeClass('active');
    $('.bi').hide(0);
    $('.md').show(0);
    $('.md').addClass('active');

    $('#dvNAMemberDetail').show(0);
    $('#dvMemberType').hide(0);

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


function isValidEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}