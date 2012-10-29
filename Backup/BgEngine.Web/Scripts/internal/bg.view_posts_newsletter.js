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
	$("#newsletter_content input:checkbox").live("change", function (eventdata) {
		if ($(this).is(":checked")) {
			$(document.body).data($(this).attr("id"), $(this).val());
		}
		else {
			$(document.body).removeData($(this).attr("id"));
		}
	});

	var cult;

	var skip = true;

	$.view_posts_newsletter = function (culture, url) {
		ajaxconnect();
		$("#pager a[href*=Post]").live("click", function () {
			var url = $(this).attr("href") + " #list";
			$("#newsletter_content").load(url, ajaxconnect);
			return false;
		});
		$("form").submit(function () {
			var data = {};
			data["posts"] = [];
			var i = 0;
			$.each($(document.body).data(), function (key, value) {
				data["posts"][i] = key;
				i++;
			});
			data["name"] = $("#Name").val();
			$.ajax({
				type: "POST",
				url: url,
				dataType: "json",
				contentType: "application/json; charset=utf-8",
				data: JSON.stringify(data),
				success: function (data) {
					if (data.result == "ok") {
						location.href = "Index";
					}
					else {
						$("#error-message").append("<p>" + data.message + "</p>").addClass("ui-state-error").show("1000", interval); ;
					}
				},
				error: function (error) {
					$("#error-message").append("<p>" + error.responseText + "</p>").addClass("ui-state-error").show("1000", interval); ;
				}
			});
			return false;
		});
		cult = culture; 
		
		function interval() {
			setTimeout(function () {
			$("#error-message").hide("1000").empty();
			}, 5000);
		}
	};


	function ajaxconnect() {
		if (!skip) {
			$("#newsletter_content input:checkbox").each(function () {
				var element = $(this);
				$(element).wrap("<label />");
				$(element).parent("label").after("<span />");
				var parent = $(element).parent("label").next();
				$(element).addClass("ui-helper-hidden");
				parent.css({ width: 16, height: 16, display: "block" });
				parent.wrap("<span class='ui-state-default ui-corner-all' style='display:inline-block;width:16px;height:16px;margin-right:5px;'/>");
				parent.parent().addClass('hover');
				if (element.checked) {
					parent.addClass("ui-icon ui-icon-check");
				}
				parent.parent("span").click(function (event) {
					$(this).toggleClass("ui-state-active");
					parent.toggleClass("ui-icon ui-icon-check");
					$(element).click();
				});
			});
		}
		skip = false;
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

		$.each($(document.body).data(), function (key, value) {
			var element = $("input:checkbox#" + key + ":hidden");
			element.click();
			element.parent().next().toggleClass("ui-state-active");
			element.parent().next().children().toggleClass("ui-icon ui-icon-check");
		});

	}

})(jQuery);