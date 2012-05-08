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
    $.widget("ui.grid", {
        _create: function () {
            var object = this;
            var table = this.element;
            table.addClass("ui-widget ui-widget-content ui-corner-all");
            table.find("th").addClass("ui-widget-header bg-panel");
            table.find("td").addClass("ui-widget-content");
            table.find("td:last").css("min-width","120px");
            $("tfoot td").contents(":empty").each(function () {
                var textnode = $(this);
                if (textnode[0].wholeText != " ") {
                    $(this).wrap("<a></a>");
                }
            });
            $("tfoot").buttonset();
            $("tfoot a").each(function () {
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
                        $("tfoot a").removeClass("ui-state-active");
                        $(this).addClass("ui-state-active");
                        return false;
                    });
                }
                else 
                {
                    $(this).addClass("ui-state-disabled");
                    $(this).removeClass("ui-state-active");
                }

            });
        }
    });
})(jQuery);




