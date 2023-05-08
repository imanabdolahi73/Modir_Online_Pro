'use strict';

(function ($) {

    var wind_ = $(window),
        body_ = $('body');

    feather.replace();

    $(document).on('click', '[data-toggle="fullscreen"]', function () {
        $(this).toggleClass('active-fullscreen');
        if (document.fullscreenEnabled) {
            if ($(this).hasClass("active-fullscreen")) {
                document.documentElement.requestFullscreen();
            } else {
                document.exitFullscreen();
            }
        } else {
            alert("مرورگر شما از حالت تمام صفحه پشتیبانی نمی کند.");
        }
        return false;
	});

	if (body_.hasClass('small-navigation')) {
		$('.navigation .navigation-menu-body > ul > li').each(function () {
			if ($(this).find('> a').next('ul').length) {
				// Dropdown add header title
				$(this).find('> a').next('ul').prepend('<li class="dropdown-divider">' + $(this).find('> a > span:not(.badge)').text() + '</li>')
			} else {
				// Add tooltip
				$(this).find('> a').attr('title', $(this).find('> a > span:not(.badge)').text());
				$(this).find('> a').tooltip({
					placement: "right"
				});
			}
		});
	}

    $(document).on('click', '.navigation-toggler > a', function () {
        if (body_.hasClass('small-navigation')) {
            body_.removeClass('small-navigation');
            $('.navigation').niceScroll({railalign: 'left'});
            $('.navigation .navigation-menu-body > ul > li').each(function () {
                if ($(this).find('> a').next('ul').length) {
                    // Dropdown add header title
                    $(this).find('.dropdown-divider').remove();
                } else {
                    // Add tooltip
                    $(this).find('> a').tooltip('dispose');
                }
            });
        } else if (body_.hasClass('hidden-navigation') || wind_.width() < 1200) {
            $.createOverlay();
            $('.navigation').addClass('open');
            $('.navigation').on('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend', function (e) {
                $('.navigation').niceScroll({railalign: 'left'}).resize();
            });
            $('[data-toggle="dropdown"]').dropdown('hide');
        } else {
            body_.addClass('small-navigation');
			$('.navigation').getNiceScroll().remove();
            $('.navigation .navigation-menu-body > ul > li').each(function () {
                if ($(this).find('> a').next('ul').length) {
                    // Dropdown add header title
                    $(this).find('> a').next('ul').prepend('<li class="dropdown-divider">' + $(this).find('> a > span:not(.badge)').text() + '</li>')
                } else {
                    // Add tooltip
                    $(this).find('> a').attr('title', $(this).find('> a > span:not(.badge)').text());
                    $(this).find('> a').tooltip({
                        placement: "right"
                    });
                }
            });
        }
        return false;
    });

    $(document).on('click', '.overlay', function () {
        $.removeOverlay();
        if (body_.hasClass('hidden-navigation')) {
            $('.navigation').niceScroll().remove();
        }
        $('.navigation').removeClass('open');
    });

    /*------------- create/remove overlay -------------*/
    $.createOverlay = function () {
        if ($('.overlay').length < 1) {
            body_.addClass('no-scroll').append('<div class="overlay"></div>');
            $('.overlay').addClass('show');
        }
    };

    $.removeOverlay = function () {
        body_.removeClass('no-scroll');
        $('.overlay').remove();
    };
    /*------------- create/remove overlay -------------*/

    $('[data-background-image]').each(function (e) {
        $(this).css("background", 'url(' + $(this).data('background-image') + ')');
    });

    /*------------- page loader -------------*/
    wind_.on('load', function () {
        $('.preloader').fadeOut(700, function () {
            setTimeout(function () {
                toastr.options = {
                    timeOut: 5000,
                    progressBar: true,
                    showMethod: "slideDown",
                    hideMethod: "slideUp",
                    showDuration: 200,
                    hideDuration: 200
                };
                toastr.info('خوش آمدید جان اسنو.');

                $('.theme-switcher').removeClass('open');
            }, 500);

            $('.theme-switcher').css('opacity', 1);
        });
    });
    /*------------- page loader -------------*/

    /*------------- side menu (sub menü arrow) -------------*/
    wind_.on('load', function () {
        setTimeout(function () {
            $('.navigation .navigation-menu-body ul li a').each(function () {
                var $this = $(this);
                if ($this.next('ul').length) {
                    $this.append('<i class="sub-menu-arrow ti-angle-up"></i>');
                }
            });
            $('.navigation .navigation-menu-body ul li.open>a>.sub-menu-arrow').removeClass('ti-plus').addClass('ti-minus').addClass('rotate-in');
        }, 200);
    });
    /*------------- side menu (sub menü arrow) -------------*/

    $(document).on('click', '.header-toggler a', function () {
        $('.header ul.navbar-nav').toggleClass('open');
        return false;
    });

    $(document).on('click', '*', function (e) {
        if (!$(e.target).is('.header ul.navbar-nav, .header ul.navbar-nav *, .header-toggler, .header-toggler *')) {
            $('.header ul.navbar-nav').removeClass('open');
        }
    });

    /*------------- form validation -------------*/
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
    /*------------- form validation -------------*/

    /*------------- responsive html table -------------*/
    var table_responsive_stack = $(".table-responsive-stack");
    table_responsive_stack
        .find("th")
        .each(function (i) {
            $(".table-responsive-stack td:nth-child(" + (i + 1) + ")").prepend(
                '<span class="table-responsive-stack-thead">' +
                $(this).text() +
                ":</span> "
            );
            $(".table-responsive-stack-thead").hide();
        });

    table_responsive_stack.each(function () {
        var thCount = $(this).find("th").length,
            rowGrow = 100 / thCount + "%";
        $(this).find("th, td").css("flex-basis", rowGrow);
    });

    function flexTable() {
        if (wind_.width() < 768) {
            $(".table-responsive-stack").each(function (i) {
                $(this)
                    .find(".table-responsive-stack-thead")
                    .show();
                $(this)
                    .find("thead")
                    .hide();
            });

            // window is less than 768px
        } else {
            $(".table-responsive-stack").each(function (i) {
                $(this)
                    .find(".table-responsive-stack-thead")
                    .hide();
                $(this)
                    .find("thead")
                    .show();
            });
        }
    }

    flexTable();

    window.onresize = function (event) {
        flexTable();
    };
    /*------------- responsive html table -------------*/

    /*------------- custom accordion -------------*/
    $(document).on('click', '.accordion.custom-accordion .accordion-row a.accordion-header', function () {
        var $this = $(this);
        $this.closest('.accordion.custom-accordion').find('.accordion-row').not($this.parent()).removeClass('open');
        $this.parent('.accordion-row').toggleClass('open');
        return false;
    });
    /*------------- custom accordion -------------*/

    /*------------- responsive table dropdown -------------*/
    var dropdownMenu,
        table_responsive = $('.table-responsive');

    table_responsive.on('show.bs.dropdown', function (e) {
        dropdownMenu = $(e.target).find('.dropdown-menu');
        body_.append(dropdownMenu.detach());
        var eOffset = $(e.target).offset();
        dropdownMenu.css({
            'display': 'block',
            'top': eOffset.top + $(e.target).outerHeight(),
            'left': eOffset.left,
            'width': '184px',
            'font-size': '14px'
        });
        dropdownMenu.addClass("mobPosDropdown");
    });

    table_responsive.on('hide.bs.dropdown', function (e) {
        $(e.target).append(dropdownMenu.detach());
        dropdownMenu.hide();
    });
    /*------------- responsive table dropdown -------------*/

    /*------------- chat -------------*/
    $(document).on('click', '.chat-app-wrapper .btn-chat-sidebar-open', function () {
        $('.chat-app-wrapper .chat-sidebar').addClass('chat-sidebar-opened');
        return false;
    });

    $(document).on('click', '*', function (e) {
        if (!$(e.target).is('.chat-app-wrapper .chat-sidebar, .chat-app-wrapper .chat-sidebar *, .chat-app-wrapper .btn-chat-sidebar-open, .chat-app-wrapper .btn-chat-sidebar-open *')) {
            $('.chat-app-wrapper .chat-sidebar').removeClass('chat-sidebar-opened');
        }
    });
    /*------------- chat -------------*/

    /*------------- aside menu toggle -------------*/
    $(document).on('click', '.navigation ul li a', function () {
        var $this = $(this);
        if ($this.next('ul').length) {
            var sub_menu_arrow = $this.find('.sub-menu-arrow');
            sub_menu_arrow.toggleClass('rotate-in');
            $this.next('ul').toggle(200);
            $this.parent('li').siblings().find('ul').not($this.parent('li').find('ul')).slideUp(200);
            $this.next('ul').find('li ul').slideUp(200);
            $this.next('ul').find('li>a').find('.sub-menu-arrow').removeClass('ti-minus').addClass('ti-plus');
            $this.next('ul').find('li>a').find('.sub-menu-arrow').removeClass('rotate-in');
            $this.parent('li').siblings().not($this.parent('li').find('ul')).find('>a').find('.sub-menu-arrow').removeClass('ti-minus').addClass('ti-plus');
            $this.parent('li').siblings().not($this.parent('li').find('ul')).find('>a').find('.sub-menu-arrow').removeClass('rotate-in');
            if (sub_menu_arrow.hasClass('rotate-in')) {
                setTimeout(function () {
                    sub_menu_arrow.removeClass('ti-plus').addClass('ti-minus');
                }, 200);
            } else {
                sub_menu_arrow.removeClass('ti-minus').addClass('ti-plus');
            }
            if (!body_.hasClass('horizontal-side-menu') && wind_.width() >= 1200) {
                setTimeout(function (e) {
					$('.navigation').getNiceScroll().resize();
                }, 300);
            }
            return false;
        }
    });

    $('body.icon-side-menu .navigation').hover(function (e) {
    }, function (e) {
        e.stopPropagation();
        $('.navigation ul').removeAttr('style');
        $('.navigation ul li').not('.open').find('>a>.sub-menu-arrow').removeClass('rotate-in').removeClass('ti-minus').addClass('ti-plus');
    });
    /*------------- aside menu toggle -------------*/

    /*------------- other -------------*/
    $(document).on('click', '.dropdown-menu', function (e) {
        e.stopPropagation();
    });

    $('#exampleModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget),
            recipient = button.data('whatever'),
            modal = $(this);

        modal.find('.modal-title').text('پیام جدید به ' + recipient);
        modal.find('.modal-body input').val(recipient);
    });

    $('[data-toggle="tooltip"]').tooltip({
        container: 'body'
    });

    $('[data-toggle="popover"]').popover();

    $('.carousel').carousel();

    if (wind_.width() >= 992) {
        $('.card-scroll').niceScroll({railalign: 'left'});
        $('.table-responsive').niceScroll({railalign: 'left'});

        $('.app-block .app-content .app-lists').niceScroll({railalign: 'left'});
        $('.app-block .app-sidebar .app-sidebar-menu').niceScroll({railalign: 'left'});
        $('.chat-block .chat-sidebar .chat-sidebar-content .tab-content .tab-pane').niceScroll({railalign: 'left'});
        var chat_messages = $('.chat-block .chat-content .messages');
        if (chat_messages.length) {
            chat_messages.niceScroll({
				railalign: 'left',
                horizrailenabled: false
            });
            chat_messages.getNiceScroll(0).doScrollTop(chat_messages.get(0).scrollHeight, -1);
        }
    }

    if (!body_.hasClass('small-navigation') && !body_.hasClass('hidden-navigation') && wind_.width() >= 992) {
        if (body_.hasClass('sticky-navigation')) {
            $('.navigation').niceScroll({railalign: 'left'});
        }
    }

    $('.dropdown-menu ul.list-group').niceScroll({railalign: 'left'});

    $(document).on('click', '.chat-block .chat-content .mobile-chat-close-btn a', function () {
        $('.chat-block .chat-content').removeClass('mobile-open');
        return false;
    });

    /* Theme Switcher */
    var path = window.location.pathname;
    var page = path.split("/").pop();

    var theme_switcher_html = '<div class="theme-switcher open"> \n\
        <div class="theme-switcher-button"> \n\
            <i class="fa fa-cog"></i> \n\
        </div> \n\
        <div class="theme-switcher-panel"> \n\
            <div class="card"> \n\
                <div class="card-body"> \n\
                    <h6 class="card-title">تغییر طرح</h6> \n\
                    <div class="form-group mb-2"> \n\
                        <div class="custom-control custom-switch"> \n\
                            <input type="checkbox" class="custom-control-input" id="shadow-layout"> \n\
                            <label class="custom-control-label" for="shadow-layout">طرح سایه</label> \n\
                        </div> \n\
                    </div> \n\
                    <div class="form-group mb-2"> \n\
                        <div class="custom-control custom-switch"> \n\
                            <input type="checkbox" class="custom-control-input" ' + (page === 'chat.html' || page === 'inbox.html' || page === 'app-todo.html' ? 'disabled' : '') + ' id="sticky-header"> \n\
                            <label class="custom-control-label" for="sticky-header">هدر چسبان</label> \n\
                        </div> \n\
                    </div> \n\
                    <div class="form-group mb-2"> \n\
                        <div class="custom-control custom-switch"> \n\
                            <input type="checkbox" class="custom-control-input" id="light-header"> \n\
                            <label class="custom-control-label" for="light-header">هدر روشن</label> \n\
                        </div> \n\
                    </div> \n\
                    <div class="form-group mb-2"> \n\
                        <div class="custom-control custom-switch"> \n\
                            <input type="checkbox" class="custom-control-input" ' + (page === 'chat.html' || page === 'inbox.html' || page === 'app-todo.html' ? 'disabled' : '') + ' id="sticky-footer"> \n\
                            <label class="custom-control-label" for="sticky-footer">فوتر چسبان</label> \n\
                        </div> \n\
                    </div> \n\
                </div> \n\
            </div> \n\
        </div> \n\
    </div>';

    $('body').append(theme_switcher_html);

    $(document).on('click', '.theme-switcher input[type="checkbox"]', function () {
        var id = $(this).attr('id');
        if (id === 'sticky-navigation') {
            if ($(this).prop('checked')) {
                $('.navigation').niceScroll({railalign: 'left'}).resize();
            } else {
                $('.navigation').niceScroll().remove();
            }
            if ($('body').hasClass('small-navigation')) {
                $('.navigation .navigation-menu-body > ul > li').each(function () {
                    if ($(this).find('> a').next('ul').length) {
                        // Dropdown add header title
                        $(this).find('.dropdown-divider').remove();
                    } else {
                        // Add tooltip
                        $(this).find('> a').tooltip('dispose');
                    }
                });
                $('body').removeClass('small-navigation');
                $('.theme-switcher input[type="checkbox"][id="small-navigation"]').prop('checked', false);
            }
            if ($('body').hasClass('hidden-navigation')) {CUSTOMİZABLE
                $('body').removeClass('hidden-navigation');
                $('.theme-switcher input[type="checkbox"][id="hidden-navigation"]').prop('checked', false);
            }
        }
        if (id === 'small-navigation') {
            if ($(this).prop('checked')) {
                $('.navigation .navigation-menu-body > ul > li').each(function () {
                    if ($(this).find('> a').next('ul').length) {
                        // Dropdown add header title
                        $(this).find('> a').next('ul').prepend('<li class="dropdown-divider">' + $(this).find('> a > span:not(.badge)').text() + '</li>')
                    } else {
                        // Add tooltip
                        $(this).find('> a').attr('title', $(this).find('> a > span:not(.badge)').text());
                        $(this).find('> a').tooltip({
                            placement: "right"
                        });
                    }
                });
            } else {
                $('.navigation .navigation-menu-body > ul > li').each(function () {
                    if ($(this).find('> a').next('ul').length) {
                        // Dropdown add header title
                        $(this).find('.dropdown-divider').remove();
                    } else {
                        // Add tooltip
                        $(this).find('> a').tooltip('dispose');
                    }
                });
            }
            if ($('body').hasClass('sticky-navigation')) {
                $('body').removeClass('sticky-navigation');
                $('.navigation').niceScroll().remove();
                $('.theme-switcher input[type="checkbox"][id="sticky-navigation"]').prop('checked', false);
            }
            if ($('body').hasClass('hidden-navigation')) {
                $('body').removeClass('hidden-navigation');
                $('.theme-switcher input[type="checkbox"][id="hidden-navigation"]').prop('checked', false);
            }
        }
        if (id === 'hidden-navigation') {
            setTimeout(function () {
                $('.navigation').niceScroll({railalign: 'left'}).resize();
                $('.app-block .app-content .app-lists').niceScroll({railalign: 'left'}).resize();
                $('.app-block .app-sidebar .app-sidebar-menu').niceScroll({railalign: 'left'}).resize();
                $('.chat-block .chat-sidebar .chat-sidebar-content .tab-content .tab-pane').niceScroll({railalign: 'left'}).resize();
            }, 200);
            if (!$(this).prop('checked')) {
                $.removeOverlay();
                $('.navigation').removeClass('open');
            }
            if (page != 'chat.html' && page != 'inbox.html' && page != 'app-todo.html') {
                if ($('body').hasClass('sticky-navigation')) {
                    $('body').removeClass('sticky-navigation');
                    $('.theme-switcher input[type="checkbox"][id="sticky-navigation"]').prop('checked', false);
                }
            }
            if ($('body').hasClass('small-navigation')) {
                $('.navigation .navigation-menu-body > ul > li').each(function () {
                    if ($(this).find('> a').next('ul').length) {
                        // Dropdown add header title
                        $(this).find('.dropdown-divider').remove();
                    } else {
                        // Add tooltip
                        $(this).find('> a').tooltip('dispose');
                    }
                });
                $('body').removeClass('small-navigation');
                $('.theme-switcher input[type="checkbox"][id="small-navigation"]').prop('checked', false);
            }
        }
        if (id === 'dark') {
            if ($('body').hasClass('semi-dark')) {
                $('body').removeClass('semi-dark');
                $('.theme-switcher input[type="checkbox"][id="semi-dark"]').prop('checked', false);
            }
        }
        if (id === 'semi-dark') {
            if ($('body').hasClass('dark')) {
                $('body').removeClass('dark');
                $('.theme-switcher input[type="checkbox"][id="dark"]').prop('checked', false);
            }
        }
        $('body').toggleClass(id);
    });

    $(document).on('click', '.theme-switcher .theme-switcher-button', function () {
        $('.theme-switcher').toggleClass('open');
	});

	$(window).on('scroll', function(){
		$('body.sticky-navigation').not('.small-navigation').not('.hidden-navigation').find('.navigation').niceScroll({railalign: 'left'}).resize();
	});

})(jQuery);