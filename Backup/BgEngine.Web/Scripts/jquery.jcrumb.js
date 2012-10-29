/*  jCrumb jquery plugin 
	version 1.0, Sep 24 2010
	Licensed under the BSD New License
	Developed by Jason Brown for any bugs or questions you can email me at loktar69@hotmail 
	info on the plugin is located on Somethinghitme.com and http://code.google.com/p/jcrumb/
	
	List of options 
	
	maxCrumbs : 4, 				// Number : Sets how many crumbs are displayed on the page
	maxMemory : 15, 			// Number : How many breadcrumbs will be stored for the user to follow
	defaultClass : 'jCrumb', 	// String: Controls how the breadcrumbs look
	jqUI : false,				// Bool : Attached jqui styles
	seperator : "&gt;" 			// False or HTML : Sets a seperator inbetween  the breadbcrumbs
	
	Default Usage :
	$('#element').jCrumb();
	
	-or-
	
	$('#element').jCrumb(options);
*/

(function ($) {
    $.fn.jCrumb = function (options) {
        var settings = {
            maxCrumbs: 4,
            maxMemory: 15,
            defaultClass: 'jCrumb',
            jqUI: false,
            seperator: "&gt;"
        },
			getCrumbs = function () {
			    var cookieName = "breadcrumbs=",
					cookies = document.cookie.split(';');

			    for (var i = 0; i < cookies.length; i++) {
			        var cookie = cookies[i].replace(/^\s+/, "");

			        if (cookie.indexOf(cookieName) == 0) {
			            return cookie.substring(cookieName.length, cookie.length);
			        }
			    }
			    return;
			},
			setCrumb = function (crumb) {
			    var crumbs = getCrumbs();

			    if (crumbs) {
			        var maxMemory = settings.maxMemory;
			        crumbCheck = crumbs.split('*');

			        if (crumbCheck[crumbCheck.length - 1] != crumb) {
			            crumbCheck.push(crumb);
			        }

			        if (crumbCheck.length > maxMemory) {
			            crumbCheck.splice(0, 1);
			        }

			        crumbs = crumbCheck.join('*');
			    } else {
			        crumbs = crumb;
			    }

			    document.cookie = 'breadcrumbs=' + crumbs + '; path=/';
			},
			removeCrumb = function (crumb, index) {
			    var crumbs = getCrumbs();
			    if (crumbs) {
			        crumbCheck = crumbs.split('*');
			        crumbCheck.splice(index, (crumbCheck.length - index));
			        crumbs = crumbCheck.join('*');
			        document.cookie = 'breadcrumbs=' + crumbs + '; path=/';
			    }

			    return;
			};
        return this.each(function () {

            if (options) {
                settings = $.extend(settings, options);
            }
            if (document.title.length > 30) {
                setCrumb(document.title.substring(0, 26) + "..." + '^' + window.location.href);
            }
            else {
                setCrumb(document.title + '^' + window.location.href);
            }            

            var crumbs = getCrumbs(),
				crumbVal = "",
				crumbList = $(document.createElement('ul')).addClass(settings.defaultClass).appendTo($(this)),
				crumbListItem = "";

            if (settings.jqUI) {
                crumbList.addClass('fg-buttonset fg-buttonset-single ui-helper-clearfix');
            }

            if (crumbs) {
                crumbs = crumbs.split('*');
                startIndex = 0;

                if (crumbs.length > settings.maxCrumbs) {
                    startIndex = crumbs.length - settings.maxCrumbs;
                }

                for (var i = startIndex; i < crumbs.length - 1; i++) {
                    crumbVal = crumbs[i].split('^');
                    crumbListItem = $(document.createElement('li'));

                    var crumbLink = $(document.createElement('a'));
                    crumbLink.attr({ 'href': crumbVal[1] }).text(crumbVal[0]).appendTo(crumbListItem.appendTo(crumbList));
                    crumbLink.click(function () { removeCrumb($(this).text() + '^' + $(this).attr('href'), $(this).data("index")); });
                    crumbLink.data("index", i);

                    if (settings.jqUI) {
                        crumbListItem.addClass('fg-button ui-state-default ui-priority-primary');
                        if (i === 0) {
                            crumbListItem.addClass('ui-corner-left');
                            crumbLink.addClass('ui-icon ui-icon-home');
                        }
                        if (startIndex !== 0 && i === startIndex) {
                            crumbListItem.addClass('ui-corner-left');
                            crumbLink.addClass('ui-icon ui-icon-carat-1-w');
                        }
                    } else if (settings.seperator) {
                        $(document.createElement('span')).html(settings.seperator).appendTo(crumbListItem);
                    }
                }

                crumbListItem = $(document.createElement('li'));
                crumbVal = crumbs[crumbs.length - 1].split('^');
                $(document.createElement('span')).text(crumbVal[0]).appendTo($(crumbListItem).appendTo($(crumbList)));

                if (settings.jqUI) {
                    crumbListItem.addClass('fg-button ui-state-default ui-priority-primary ui-corner-right ui-state-active');
                }

                $(".fg-button:not(.ui-state-disabled)").hover(
					function () {
					    $(this).addClass("ui-state-hover");
					},
					function () {
					    $(this).removeClass("ui-state-hover");
					}
				)
            }
        });
    };
})(jQuery);