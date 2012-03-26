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
    $.index_startup = function (text) {
        $("table").grid();
		$("th a").button({ icons: { primary: "ui-icon-triangle-2-n-s" }, text: true });
        $("#grid-container").ajaxStart(function () {
            $("#grid-container").block({ css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            },
                message: text
            });
        });
        $("#grid-container").ajaxComplete(function () {
            $("#grid-container").unblock();
            $("table").grid();
            ajaxconnect();
        });
        ajaxconnect();
    };

    function ajaxconnect() {
        $(".bg-button-grid-edit").button({ icons: { primary: "ui-icon-pencil" }, text: false });
        $(".bg-button-grid-delete").button({ icons: { primary: "ui-icon-circle-close" }, text: false });
        $(".bg-button-grid-zoom").button({ icons: { primary: "ui-icon-zoomin" }, text: false });
		$("th a").button({ icons: { primary: "ui-icon-triangle-2-n-s" }, text: true });
        $(".options").buttonset();
    }
})(jQuery);
