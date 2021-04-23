// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function($) {

	"use strict";

	var fullHeight = function() {
        $('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});
	};
	fullHeight();



	$('#sidebar').toggleClass('active1');
	$('#sidebarCollapse').css('margin-left', '1em');
	$('#navbarbutton').on('click', function () {
		$('#navigatorsbar').toggleClass('actionbar');
	});
    $('body').css('zoom', 0.6)

})(jQuery);
