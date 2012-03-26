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
});