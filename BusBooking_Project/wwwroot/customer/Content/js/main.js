jQuery(document).ready(function($){
	//open navigation clicking the menu icon
    $("#br-cover-content").css('height', 'auto');
    $('.cd-nav-trigger').on('click', function (event) {
        $("#sb-site").parent().addClass('br-modal cd-section cd-selected');
        $("#br-cover-content").css('height','100vh');
		event.preventDefault();
		toggleNav(true);
    });
    $('.cd-nav-trigger-mobile').on('click', function (event) {
        $("#sb-site").parent().addClass('br-modal cd-section cd-selected');
        $("#br-cover-content").css('height', '100vh');
        event.preventDefault();
        toggleNav(true);
    });
	//close the navigation
	$('.cd-close-nav, .cd-overlay').on('click', function(event){
	    $("#sb-site").parent().removeClass('br-modal cd-section cd-selected');
	    event.preventDefault();
	    toggleNav(false);
	    setTimeout(function () {
	        $("#br-cover-content").css('height', 'auto');
	    }, 1000);
	});

	function toggleNav(bool) {
		$('.cd-nav-container, .cd-overlay').toggleClass('is-visible', bool);
		$('#br-cover-content').toggleClass('scale-down', bool);
	}
	
});
