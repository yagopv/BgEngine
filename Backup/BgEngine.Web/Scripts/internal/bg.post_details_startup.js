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
    $.post_details_startup = function (scriptspath, getpostcommentsurl, showcommentsmessage, hidecommentsmessage) {

        /* For work with another brushes you can download it from http://alexgorbatchev.com/SyntaxHighlighter/manual/brushes/ */
        /* You can just download the file and add it to Scripts/syntaxhlBrushes. Then you have to add a line like the following ones */
        /* to point to the new brush */
        SyntaxHighlighter.autoloader(
            'js jscript javascript  ' + scriptspath + 'syntaxhlBrushes/shBrushJScript.js',
            'c# c-sharp csharp      ' + scriptspath + 'syntaxhlBrushes/shBrushCSharp.js',
            'css                    ' + scriptspath + 'syntaxhlBrushes/shBrushCss.js',
            'java                   ' + scriptspath + 'syntaxhlBrushes/shBrushJava.js',
            'xml xhtml xslt html    ' + scriptspath + 'syntaxhlBrushes/shBrushXml.js',
            'sql                    ' + scriptspath + 'syntaxhlBrushes/shBrushSql.js'
		);
        SyntaxHighlighter.all();

        var commentvisibility = false;
		$("#showcomments").click(function () {
            if ($("#commentsection").children().size() == 0) {
                $("#ajax-loader").toggle();
			    $.get(getpostcommentsurl, 
				    function (data, status) {
					    $("#commentsection").append(data);
                        $("#ajax-loader").toggle();
				    }
			    );
            }
			$("#commentsection").slideToggle("slow");
            $(".bg-button-comments").button({ icons: { primary: "ui-icon-comment" }, text: true });
            if (commentvisibility == true)
            {
                $(".bg-button-comments .ui-button-text").text(showcommentsmessage);
                commentvisibility = false;
            }
            else
            {
                $(".bg-button-comments .ui-button-text").text(hidecommentsmessage);
                commentvisibility = true;
            }
			return false;
		});
    };
})(jQuery);