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
		$(this).next('.moreLinkPopup').slideToggle('300');
	});
	$(".moreLinkPopup").click(function (moreNav) {
		moreNav.stopPropagation();
	});
	$("body").click(function () {
		$('.moreLinkPopup').hide();
	});
	

	$(".popupLinks .arrow").click(function () {
		$('.popupLinks .inner').slideToggle(0);
		$(this).toggleClass('active');
	});
	
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

	$(".regNextStep").click(function () {
		$('.gi').removeClass('active');
		$('.bi').addClass('active');
		$('#generalInfo').hide(0);
		$('#businessInfo').show(0);
	});

	$(".dbMenu").click(function () {
		$('.dbCon').toggleClass('active');
	});

	$('.checkInpt').each(function () {
		$(this).wrap("<span class='checkWrapper'></span>");
		$(this).after("<i class='bg'></i>");
	});
	$('.radioInpt').each(function () {
		$(this).wrap("<span class='radioWrapper'></span>");
		$(this).after("<i class='bg'></i>");
	});

	$(".dbLeftMenu li .link").click(function () {
		$(".subLink").slideUp('300');
		if ($(this).parent("li").hasClass("open")) {
			$(".dbLeftMenu li").removeClass("open");
			$(this).parent('li').find(".subLink").slideUp('300');
		}
		else {
			$(".dbLeftMenu li").removeClass("open");
			$(this).parent("li").addClass("open");
			$(this).parent('li').find(".subLink").slideDown('300');
		}
	});

});