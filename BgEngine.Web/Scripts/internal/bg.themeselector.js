(function ($) {

    $.fn.themeselector = function (user_options) {

        tsOptions = $.extend($.fn.themeselector.default_options, user_options);

        return this.each(function () {
            $(this).button();
            $(this).click(function (e) {
                $.get(tsOptions.url,
                function (data, status) {
                    $.fancybox({
                        'content': data,
                        'scrolling': 'no',
                        'autoDimensions': false,
                        'autoScale':true,
                        'width': tsOptions.width,
                        'height': tsOptions.height
                    });
                }
            );
                return false;
            });
        });
    };

    $.fn.themeselector.default_options = {
        url: " ",
        width: 300,
        height: 300
    };

})(jQuery);