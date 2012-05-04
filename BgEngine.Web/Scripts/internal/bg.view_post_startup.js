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
	$.view_post_startup = function (loading_message, postid, getposturl, commentcount, scriptspath, culture, usermail, leavecomment, isauthenticated) {
		tinyMCE.init({
			mode: "textareas",
			theme: "advanced",
			skin: "cirkuit",
			theme_advanced_toolbar_location: "top",
			theme_advanced_buttons1: "bold,italic,underline,blockquote,link,unlink",
			theme_advanced_buttons2: "",
			width: "100%"
		});

		$("#comment-container form").form();

		$("#comment-submit").live("click", function () {
			if (isauthenticated == "True" || (isauthenticated != "True" && $("#comment-container .anonymous-comment-fields").valid())) {
				var message = tinyMCE.get("comment-textarea").getContent();
				if (message != "") {
					$("#comment-container").block({ css: {
						border: 'none',
						padding: '15px',
						backgroundColor: '#000',
						'-webkit-border-radius': '10px',
						'-moz-border-radius': '10px',
						opacity: .5,
						color: '#fff'
					},
						message: loading_message
					});
					$.post("/Comment/AddComment/", (isauthenticated == "True"
						? { Message: message, PostId: postid }
						: { Message: message, PostId: postid, "AnonymousUser.Username": $("#comment-container #Username").val(), "AnonymousUser.Email": $("#comment-container #Email").val(), "AnonymousUser.Web": $("#comment-container #Web").val() }),
						   function (data) {
							   if (data.result == "ok") {
								   $("#comments").load(getposturl,
														function (text, status, request) {
															var numberofcomments = commentcount + 1;
															$("#comment.count").text(numberofcomments);
															$(".bg-button-reply").button({ icons: { primary: "ui-icon-pencil" }, text: false });
															$("#comment-container").unblock();
															$("#comment-container #Username").val("");
															$("#comment-container #Email").val("");
															$("#comment-container #Web").val("");
															reconnectTooltips();
														});
							   }
							   else {
							       $("#comment-container").unblock();
								   var validator = $("#comment-container form").validate();
								   errors = { };
								   for (var i = 0; i < data.errors.length; i++) {
									   errors[data.errors[i].Key] = data.errors[i].Value;
								   }
								   validator.showErrors(errors);
							   }
							   tinyMCE.get("comment-textarea").setContent("");
						   }
						   , "json");
				}
			}
		});

		$("#related-comment-submit").live("click", function () {
			if (isauthenticated == "True" || (isauthenticated != "True" && $("#comments .anonymous-comment-fields").valid())) {
				var message = tinyMCE.get("dynamic-textarea").getContent();
				if (message != "") {
					$("#newcomment").block({ css: {
						border: 'none',
						padding: '15px',
						backgroundColor: '#000',
						'-webkit-border-radius': '10px',
						'-moz-border-radius': '10px',
						opacity: .5,
						color: '#fff'
					},
						message: loading_message
					});
					$.post("/Comment/AddRelatedComment/", (isauthenticated == "True"
						 ? { Message: message, PostId: postid, parent: $("#related-comment-submit").attr("title") }
						 : { Message: message, PostId: postid, parent: $("#related-comment-submit").attr("title"), "AnonymousUser.Username": $("#comments #Username").val(), "AnonymousUser.Email": $("#comments #Email").val(), "AnonymousUser.Web": $("#comments #Web").val() }),
						   function (data) {
							   if (data.result == "ok") {
								   tinyMCE.execCommand('mceFocus', false, 'dynamic-textarea');
								   tinyMCE.execCommand('mceRemoveControl', false, 'dynamic-textarea');
								   $(".reply .ui-icon").removeClass("ui-icon-cancel").addClass("ui-icon-pencil");
								   $("#comments").load(getposturl,
														function (text, status, request) {
															var numberofcomments = commentcount + 1;
															$("#comment.count").text(numberofcomments);
															$(".bg-button-reply").button({ icons: { primary: "ui-icon-pencil" }, text: false });
															$("#newcomment").unblock();
															$("#comments #Username").val("");
															$("#comments #Email").val("");
															$("#comments #Web").val("");
															reconnectTooltips();
														});
							   }
							   else {
							       $("#newcomment").unblock();
								   var validator = $("#comments form").validate();
								   errors = { };
								   for (var i = 0; i < data.errors.length; i++) {
									   errors[data.errors[i].Key] = data.errors[i].Value;
								   }
								   validator.showErrors(errors);
							   }
							   tinyMCE.get("dynamic-textarea").setContent("");
						   }
						   , "json");
				}
			}
		});
		SyntaxHighlighter.autoloader(
			'js jscript javascript  ' + scriptspath + 'syntaxhlBrushes/shBrushJScript.js',
			'c# c-sharp csharp      ' + scriptspath + 'syntaxhlBrushes/shBrushCSharp.js',
			'css                    ' + scriptspath + 'syntaxhlBrushes/shBrushCss.js',
			'java                   ' + scriptspath + 'syntaxhlBrushes/shBrushJava.js',
			'xml xhtml xslt html    ' + scriptspath + 'syntaxhlBrushes/shBrushXml.js',
			'sql                    ' + scriptspath + 'syntaxhlBrushes/shBrushSql.js'
		);
		SyntaxHighlighter.all();

		$(".reply").live("hover", function () {
			//var element = $(this).closest("div").prev("div").toggleClass("ui-widget-content ui-helper-corner-all");
			var element = $(this).closest("li").toggleClass("ui-widget-content ui-helper-corner-all");
			var a = 1;
		});
		$(".reply").live("click", function () {
			var self = this;
			if ($("#newcomment").length == 0) {
				if (isauthenticated == "True") {
					createComment(self, " ", usermail, leavecomment);
				}
				else {

					$.get("/Comment/AnonymousComment", function (data) {
						createComment(self, data, usermail, leavecomment);
						$("#comments form").form();
					});
				}
			}
			else {
				$(".reply").not(this).show("slow");
				$(".ui-icon", this).removeClass("ui-icon-cancel").addClass("ui-icon-pencil");
				tinyMCE.execCommand('mceFocus', false, 'dynamic-textarea');
				tinyMCE.execCommand('mceRemoveControl', false, 'dynamic-textarea');
				$("#newcomment").remove();
			}
			return false;
		});

		var infobox = $('body').infobox({
			dataUrl: 'http://feeds.delicious.com/v2/json/popular/'
		});

		$('span[data-tag]').tagger({
			activated: function (event, data) {
				infobox.infobox('displayTagLinks', event, data.name);
			},
			deactivated: function () {
				infobox.infobox('hideTagLinks');
			}
		});
	};

	function createComment(object, htmldata, usermail, leavecomment) {
		$(".reply").not(object).hide("slow");
		$(".ui-icon", object).removeClass("ui-icon-pencil").addClass("ui-icon-cancel");
		$(object).closest("li.thread").after('<li id="newcomment" class="ui-state-highlight ui-corner-all ui-helper-margin">' +
													'<div class="comment-area ui-helper-width-100pc">' +
													   '<div class="bg-widget-image-left">' +
														   usermail +
													   '</div>' +
													   '<div class="bg-widget-content-right">' +
														   htmldata +
														   '<textarea id="dynamic-textarea" rows="5" class="ui-helper-width-100pc ui-state-default ui-corner-all"></textarea>' +
														   '<div class="bg-input-search ui-helper-margin-top-bottom">' +
															   '<input id="related-comment-submit" title="' + $(object).attr("id") + '" type="button" value="' + leavecomment + '"  />' +
														   '</div>' +
													   '</div>' +
													'</div>' +
												'<div class="ui-helper-reset-float"></div>' +
											'</li>');
		$('html,body').animate({ scrollTop: $("#newcomment").closest("li").position().top }, { duration: 'slow', easing: 'swing' });
		$.validator.unobtrusive.parse($("#comments .anonymous-comment-fields"));
		$("#related-comment-submit").button();
		tinyMCE.execCommand('mceAddControl', false, 'dynamic-textarea');
	}

	function reconnectTooltips() {
		$(".tooltip-default").tooltip();
		$(".tooltip, .tooltip-ajax").tooltip({
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
				if ($(this).hasClass("tooltip-ajax")) {
					$.get(href, response);
					return Globalize.localize("loading", culture);
				}
				else {
					if (/^#/.test(href)) {
						return $(href).html();
					}
				}
				return this.title;
			},
			close: function () {
				$(document).unbind("mousemove");
			}
		});
		$(".tooltip-ajax").click(function () { return false; });
	}
})(jQuery);