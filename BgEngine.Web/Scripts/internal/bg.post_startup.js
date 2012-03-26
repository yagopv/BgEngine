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
    $.post_startup = function (index_url, autocomplete_url,message) {
		var needsDelay = false;
		$("#searchpost").keyup(function (event) {
				if ((!needsDelay) && (!event.shiftKey) && (!event.altKey) && (event.keyCode != '16' ) && (event.keyCode != '18' )
					&& (event.keyCode != '37' )&& (event.keyCode != '38' )&& (event.keyCode != '39' )&& (event.keyCode != '40' ))
				{                
					needsDelay = true;
					setTimeout(function () {needsDelay = false;},"1000");
					var url = index_url + "?searchstring=" + encodeURI($(this).val()) + " #grid-container";
					$("#grid-container").load(url);					
				}
		});
		$("#searchpost").focusin(function () {$(this).addClass("ui-state-hover") });
		$("#searchpost").focusout(function () {$(this).removeClass("ui-state-hover") });
		$("#searchpost").watermark(message,{className: "watermark", useNative: false});
		$("#searchpost").autocomplete({
			source: autocomplete_url,
			select: function(event, ui) {
				var url = index_url + "?searchstring=" + encodeURI(ui.item.label) + " #grid-container";
				$("#grid-container").load(url);
			}
		});       
		$("#searchbutton").click(function () { 
			var url = index_url + "?searchstring=" + encodeURI($("#searchpost").val()) + " #grid-container";
			$("#grid-container").load(url);					
			return false; 
		}); 
        $(".image").scaleImage({fade: 2000,scale: "fill", center: true});
    };
})(jQuery);