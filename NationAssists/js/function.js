$(document).ready(function () {

	$(".stepList li").click(function(event) {
		event.preventDefault();
        $(this).addClass("active");
        $(this).siblings().removeClass("active");
        var tab = $(this).attr("data-");
        $(".videoBox").not(tab).hide(0);
        $(tab).show(0);
	});


	$(".showMobMenu").click(function (navfun) {
		navfun.stopPropagation();
		$(this).toggleClass('active');
		$(this).next('.menu').toggleClass('active');
	});
	$(".menu").click(function (navfun) {
		navfun.stopPropagation();
	});
	$("body").click(function () {
		$('.showMobMenu').removeClass('active');
		$('.menu').removeClass('active');
	});

	$(".showMoreLink").click(function (moreNav) {
		moreNav.stopPropagation();
		$(this).next('.moreLinkPopup').show('300');
	//	$(this).next('.moreLinkPopup').slideToggle('300');
	});
	//$(".moreLinkPopup").click(function (moreNav) {
	//	moreNav.stopPropagation();
	//});
	$("body").click(function () {
		$('.moreLinkPopup').hide();
	});
	

	//$(".popupLinks .arrow").click(function () {
	//	$('.popupLinks .inner').slideToggle(0);
	//	$(this).toggleClass('active');
	//});
	
	$(".showSearch").click(function () {
		$('#searchBox').slideToggle(300);
	});

	$(".menu li").hover(
		function () {
			$(this).addClass('open');
		},
		function () {
			$(this).removeClass('open');
		}
	);


});