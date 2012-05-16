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

$(function () {
	$(".sortable").sortable({
		connectWith: ".sortable"
	});
	$(".sortable").disableSelection();

	$(".primarypost,.secondarypost").hover(
			function () {
				$(this).toggleClass("ui-state-highlight");
				$(".primarypost,.secondarypost").not(this).removeClass("ui-state-highlight");
			},
			function () {
				$(this).removeClass("ui-state-highlight");
			}
	);

	$.get("/Video/LatestVideos/", function (data) {
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
        $(".tooltip, .tooltip-default").tipTip();
        $(".tooltip-ajax").tipTip({
            content: function (data) {
                $.ajax({
                    url: $(this).attr("href"),
                    success: function (response) {
                        data.content.html(response);
                    }
                });
                return Globalize.localize("loading", "@CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture())");
            }
        });
        $(".tooltip-ajax").click(function () { return false; });
		$("#home-videos").fadeIn(1000);
	});
});