$(document).ready(function () {
    // Typed animation in the header of the website
    var typed = new Typed('#typed', {
        stringsElement: '#typed-strings',
        typeSpeed: 40,
        loop: true,
        backDelay: 1000,
    });

    // Controls for the sidebar menu
    var trigger = $('.hamburger'),
    overlay = $('.overlay'),
    isClosed = false;

    // On clicking the menu button
    trigger.click(function () {
        hamburger_cross();
    });

    function hamburger_cross() {
        if (isClosed == true) {
            overlay.hide();
            trigger.removeClass('is-open');
            trigger.addClass('is-closed');
            isClosed = false;
        } else {
            overlay.show();
            trigger.removeClass('is-closed');
            trigger.addClass('is-open');
            isClosed = true;
        }
    }

    $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
    });
});





