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
    $.create_post_startup = function (imageurl, no_image_path) {
        $("#select-image").click(function (e) {
            $.get(imageurl,
                function(data, status) {
                    $.fancybox({
                        'content': data,
                        'scrolling' : 'no',
                        'autoDimensions' : false,
                        'width': 640,
                        'height': 480
                    });
                }                
            );
            return false;
        });
        $("#cancel-image").click(function (e) {
            $("#image-selector img").attr("src", no_image_path);
            $("#ImageId").val("");            
            return false;
        });
        $(".image").live("click", function() {
            $.fancybox.close();
            $("#image-selector img").attr("src",$(this).attr("src"));
            $("#ImageId").val($(this).attr("id"));
        });

        $("form").form();
    };
})(jQuery);