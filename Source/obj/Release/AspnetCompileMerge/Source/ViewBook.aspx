<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBook.aspx.cs" Inherits="HaloBoSach.ViewBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/responsive.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery.js"></script>
    <script type="text/javascript" src="/ctrl/bookflip/extras/modernizr.2.5.3.min.js"></script>
    <script type="text/javascript" src="/ctrl/bookflip/lib/hash.js"></script>
    <style type="text/css">
        body
        {
            background: none transparent;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div id="bookContent" runat="server">
        </div>
    </form>
</body>
<script type="text/javascript">
    function loadApp() {

        $('#canvas').fadeIn(1000);

        var flipbook = $('.magazine');

        // Check if the CSS was already loaded

        if (flipbook.width() == 0 || flipbook.height() == 0) {
            setTimeout(loadApp, 10);
            return;
        }

        // Create the flipbook

        flipbook.turn({

            // Magazine width

            width: 922,

            // Magazine height

            height: 600,

            // Duration in millisecond

            duration: 1000,

            // Hardware acceleration

            acceleration: !isChrome(),

            // Enables gradients

            gradients: true,

            // Auto center this flipbook

            autoCenter: true,

            // Elevation from the edge of the flipbook when turning a page

            elevation: 50,

            // The number of pages


            // Events

            when: {
                turning: function (event, page, view) {

                    var book = $(this),
                    currentPage = book.turn('page'),
                    pages = book.turn('pages');

                    // Update the current URI

                    Hash.go('page/' + page).update();

                    // Show and hide navigation buttons

                    disableControls(page);


                    $('.thumbnails .page-' + currentPage).
                        parent().
                        removeClass('current');

                    $('.thumbnails .page-' + page).
                        parent().
                        addClass('current');



                },

                turned: function (event, page, view) {

                    disableControls(page);

                    $(this).turn('center');

                    if (page == 1) {
                        $(this).turn('peel', 'br');
                    }

                }
                /*
                ,

                missing: function (event, pages) {

                    // Add pages that aren't in the magazine

                    for (var i = 0; i < pages.length; i++)
                        addPage(pages[i], $(this));

                }
                */
            }

        });

        // Zoom.js

        $('.magazine-viewport').zoom({
            flipbook: $('.magazine'),

            max: function () {

                return largeMagazineWidth() / $('.magazine').width();

            },

            when: {

                swipeLeft: function () {

                    $(this).zoom('flipbook').turn('next');

                },

                swipeRight: function () {

                    $(this).zoom('flipbook').turn('previous');

                },

                resize: function (event, scale, page, pageElement) {

                    if (scale == 1)
                        loadSmallPage(page, pageElement);
                    else
                        loadLargePage(page, pageElement);

                },

                zoomIn: function () {

                    $('.thumbnails').hide();
                    $('.made').hide();
                    $('.magazine').removeClass('animated').addClass('zoom-in');
                    $('.zoom-icon').removeClass('zoom-icon-in').addClass('zoom-icon-out');

                    if (!window.escTip && !$.isTouch) {
                        escTip = true;

                        $('<div />', { 'class': 'exit-message' }).
                            html('<div>Press ESC to exit</div>').
                                appendTo($('body')).
                                delay(2000).
                                animate({ opacity: 0 }, 500, function () {
                                    $(this).remove();
                                });
                    }
                },

                zoomOut: function () {

                    $('.exit-message').hide();
                    $('.thumbnails').fadeIn();
                    $('.made').fadeIn();
                    $('.zoom-icon').removeClass('zoom-icon-out').addClass('zoom-icon-in');

                    setTimeout(function () {
                        $('.magazine').addClass('animated').removeClass('zoom-in');
                        resizeViewport();
                    }, 0);

                }
            }
        });

        // Zoom event

        if ($.isTouch)
            $('.magazine-viewport').bind('zoom.doubleTap', zoomTo);
        else
            $('.magazine-viewport').bind('zoom.tap', zoomTo);


        // Using arrow keys to turn the page

        $(document).keydown(function (e) {

            var previous = 37, next = 39, esc = 27;

            switch (e.keyCode) {
                case previous:

                    // left arrow
                    $('.magazine').turn('previous');
                    e.preventDefault();

                    break;
                case next:

                    //right arrow
                    $('.magazine').turn('next');
                    e.preventDefault();

                    break;
                case esc:

                    $('.magazine-viewport').zoom('zoomOut');
                    e.preventDefault();

                    break;
            }
        });

        // URIs - Format #/page/1 

        Hash.on('^page\/([0-9]*)$', {
            yep: function (path, parts) {
                var page = parts[1];

                if (page !== undefined) {
                    if ($('.magazine').turn('is'))
                        $('.magazine').turn('page', page);
                }

            },
            nop: function (path) {

                if ($('.magazine').turn('is'))
                    $('.magazine').turn('page', 1);
            }
        });


        $(window).resize(function () {
            resizeViewport();
        }).bind('orientationchange', function () {
            resizeViewport();
        });




        // Regions

        if ($.isTouch) {
            $('.magazine').bind('touchstart', regionClick);
        } else {
            $('.magazine').click(regionClick);
        }

        // Events for the next button

        //$('.next-button').bind($.mouseEvents.over, function () {

        //    $(this).addClass('next-button-hover');

        //}).bind($.mouseEvents.out, function () {

        //    $(this).removeClass('next-button-hover');

        //}).bind($.mouseEvents.down, function () {

        //    $(this).addClass('next-button-down');

        //}).bind($.mouseEvents.up, function () {

        //    $(this).removeClass('next-button-down');

        //}).click(function () {

        //    $('.magazine').turn('next');

        //});

        $('.next-button').addClass('next-button-hover').click(function () {$('.magazine').turn('next');});

        // Events for the next button

        //$('.previous-button').bind($.mouseEvents.over, function () {

        //    $(this).addClass('previous-button-hover');

        //}).bind($.mouseEvents.out, function () {

        //    $(this).removeClass('previous-button-hover');

        //}).bind($.mouseEvents.down, function () {

        //    $(this).addClass('previous-button-down');

        //}).bind($.mouseEvents.up, function () {

        //    $(this).removeClass('previous-button-down');

        //}).click(function () {

        //    $('.magazine').turn('previous');

        //});
        $('.previous-button').addClass('previous-button-hover').click(function () {$('.magazine').turn('previous');});

        resizeViewport();

        $('.magazine').addClass('animated');

    }


    $('#canvas').hide();


    // Load the HTML4 version if there's not CSS transform

    yepnope({
        test: Modernizr.csstransforms,
        yep: ['/ctrl/bookflip/lib/turn.min.js'],
        nope: ['/ctrl/bookflip/lib/turn.html4.min.js'],
        both: ['/ctrl/bookflip/lib/zoom.min.js', '/ctrl/bookflip/js/magazine.js', '/ctrl/bookflip/css/magazine.css'],
        complete: loadApp
    });
    $(document).ready(function () { parent.DisplayContent(); });
</script>
</html>
