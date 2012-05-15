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

    var cult;

    $.view_posts_startup = function (culture, videocategory, videotag) {
        ajaxconnect();
        $("#pager a[href*=Post]").live("click", function () {
            var url = $(this).attr("href") + " #list";
            $("#maincontent").load(url, ajaxconnect);
            return false;
        });
        cult = culture;
        $.get("/Video/LatestVideos?category=" + videocategory + "&tag=" + videotag, function (data) {
            $("#maincontent").append(data);
            $(".video-container").hover(
                function () {
                    $(this).toggleClass("ui-state-highlight");
                    $(".video-container").not(this).removeClass("ui-state-highlight");
                },
                function () {
                    $(this).removeClass("ui-state-highlight");
                }
            );
            $(".tooltip").tooltip({
                items: "[href], [title]",
                open: function () {
                    var tooltip = $(".ui-tooltip");
                    $(document).mousemove(function (event) {
                        tooltip.position({
                            my: "left+25 center",
                            at: "right+25 center",
                            of: event
                        });
                    })
                    // trigger once to override element-relative positioning 
			    .mousemove();
                },
                content: function (response) {
                    var href = $(this).attr("href");
                    if (/^#/.test(href)) {
                        return $(href).html();
                    }
                    // using string.replace for line break substitution
                    return this.title.replace(new RegExp("\\n", "g"), "<br/>");
                },
                close: function () {
                    $(document).unbind("mousemove");
                }
            });
            $("#home-videos").fadeIn(1000);
        });
    };

    function ajaxconnect() {
        $("#pager").buttonset();
        $(".bg-button-readmore").button({ icons: { primary: "ui-icon-circle-plus" }, text: false });
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
        $(".mainpost").mouseover(function () { $(this).addClass("ui-state-highlight") })
                      .mouseout(function () { $(this).removeClass("ui-state-highlight") });
        $('html,body').animate({ scrollTop: $("body").position().top }, { duration: 'slow', easing: 'swing' });
        reconnectTooltips();
    }

    function reconnectTooltips() {
        $(".tooltip, .tooltip-default").tipTip();
        $(".tooltip-ajax").tipTip({
            content: function (data) {
                $.ajax({
                    url: $(this).attr("href"),
                    success: function (response) {
                        data.content.width("200px");
                        data.content.height("90px");
                        data.content.html(response); t
                    }
                });
                return Globalize.localize("loading", "@CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture())");
            },
            exit: function (data) {
                data.content.removeAttr("style");
            }
        });
        $(".tooltip-ajax").click(function () { return false; });
    }

})(jQuery);