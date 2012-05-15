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
    $.widget("ui.form", {
        _create: function () {
            var object = this;
            var form = this.element;
            var inputs = form.find("input , select ,textarea");

            form.find("fieldset").addClass("ui-widget-content ui-corner-all");
            form.find("legend").addClass("bg-panel ui-widget-header ui-corner-all");
            form.addClass("ui-widget");

            $.each(inputs, function () {
                if (!$(this).hasClass("slider-control")) {
                    $(this).addClass('ui-state-default ui-corner-all');
                    if ($(this).is(":checkbox")) {
                        $(this).wrap("<label />");
                    }
                    if ($(this).is(":checkbox"))
                        object.checkboxes(this);
                    else if ($(this).is("input[type='text']") || $(this).is("textarea") || $(this).is("input[type='password']"))
                        object.textelements(this);
                    else if ($(this).is(":radio"))
                        object.radio(this);

                    if ($(this).hasClass("date")) {
                        $(this).datepicker();
                    }

                    if ($(this).is("select")) {
                        $(this).selectmenu({
                            width: "25em"
                        });
                    }
                }
                else {
                    $(this).css("border", "none").css("font-size","2.2em").css("width","2em").css("padding","5px").css("text-align","right");
                }
            });

            $(".hover").hover(function () {
                $(this).addClass("ui-state-hover");
            }, function () {
                $(this).removeClass("ui-state-hover");
            });

        },

        textelements: function (element) {

            $(element).bind({
                focusin: function () {
                    $(this).toggleClass('ui-state-focus');
                },
                focusout: function () {
                    $(this).toggleClass('ui-state-focus');
                }
            });

        },

        checkboxes: function (element) {
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
        },

        radio: function (element) {
            $(element).parent("label").after("<span />");
            var parent = $(element).parent("label").next();
            $(element).addClass("ui-helper-hidden");
            parent.addClass("ui-icon ui-icon-radio-off");
            parent.wrap("<span class='ui-state-default ui-corner-all' style='display:inline-block;width:16px;height:16px;margin-right:5px;'/>");

            parent.parent().addClass('hover');


            parent.parent("span").click(function (event) {
                $(this).toggleClass("ui-state-active");
                parent.toggleClass("ui-icon-radio-off ui-icon-bullet");
                $(element).click();

            });
        }
    });
})(jQuery);



