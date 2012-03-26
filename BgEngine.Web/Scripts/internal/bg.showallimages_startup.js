/*==============================================================================
* This file is part of BgEngine.
*
* BgEngine is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* BgEngine is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with BgEngine. If not, see <http://www.gnu.org/licenses/>.
*==============================================================================
* Copyright (c) 2011 Yago Pérez Vázquez
* Version: 1.0
*==============================================================================*/

(function ($) {
    $.showallimages_startup = function (imageurl, autocompleteurl, loadingmessage, imagewatermark) {
        $(".bg-button-search").button({ icons: { primary: "ui-icon-zoomin" }, text: false });
        $("#searchbutton").click(function () {
            var url = imageurl + "?searchstring=" + encodeURI($("#searchimage").val()) + " #images>*";
            $("#images").load(url, connectPager);
            return false;
        });
        var needsDelay = false;
        $("#searchimage").keyup(function (event) {
            if ((!needsDelay) && (!event.shiftKey) && (!event.altKey) && (event.keyCode != '16') && (event.keyCode != '18')
				&& (event.keyCode != '37') && (event.keyCode != '38') && (event.keyCode != '39') && (event.keyCode != '40')) {
                needsDelay = true;
                setTimeout(function () { needsDelay = false; }, "1000");
                var url = imageurl + "?searchstring=" + encodeURI($(this).val()) + " #images>*";
                $("#images").load(url, connectPager);
            }
        });
        $("#searchimage").focusin(function () { $(this).addClass("ui-state-hover") });
        $("#searchimage").focusout(function () { $(this).removeClass("ui-state-hover") });
        $("#searchimage").watermark(imagewatermark, { className: "watermark", useNative: false });
        $("#searchimage").autocomplete({
            source: autocompleteurl,
            select: function (event, ui) {
                var url = imageurl + "?searchstring=" + encodeURI(ui.item.label) + " #images>*";
                $("#images").load(url, connectPager);
            }
        });

        $("#pager a[href*=Image]").live("click", function () {
            var url = $(this).attr("href");
            $("#images").load(url + " #images>*", connectPager);
            return false;
        });
        $(".image").scaleImage({ fade: 2000, scale: "fill", center: true });
        $(".elements").ajaxStart(function () {
            $("#images").block({ css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            },
                message: loadingmessage
            });
        });
        $(".elements").ajaxComplete(function () {
            $("#images").unblock();
        });
        connectPager();
    };


    function connectPager() {
        $("#pager").buttonset();
        $("#pager a").each(function () {
            var myHref = $(this).attr("href");
            if (myHref != null) {
                $(this).addClass("ui-state-active");
                $(this).removeClass("ui-state-disabled");
                $(this).mouseover(function () {
                    $(this).removeClass("ui-state-active");
                }).mouseout(function () {
                    $(this).addClass("ui-state-active");
                });
                $(this).click(function () {
                    $("#pager a").removeClass("ui-state-active");
                    $(this).addClass("ui-state-active");
                });
            }
            else {
                $(this).addClass("ui-state-disabled");
                $(this).removeClass("ui-state-active");
            }
        });
    }
})(jQuery);