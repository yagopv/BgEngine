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

    var offsetX = 20,
        offsetY = 20,
        mouseOverBox = false,
        leftSideAdjustment = -270;

    $.widget('bg.infobox', {

        options: {
            dataUrl: '',
            maxItems: 10
        },

        _create: function () {
            var that = this,
                name = this.name;
            this.infoboxElement = $('<div class="bg-infobox ui-widget ui-widget-content ui-corner-all ui-tooltip" />');
            this.infoboxElement.appendTo('body')
            .bind('mouseenter.' + name, function () {
                mouseOverBox = true;
            })
            .bind('mouseleave.' + name, function () {
                mouseOverBox = false;
                that.hideTagLinks();
            });
        },

        hideTagLinks: function () {
            if (!mouseOverBox) {
                this.infoboxElement.hide();
            }
        },

        displayTagLinks: function (event, tagName) {
            var i,
                html,
                that = this,
                options = this.options,
                elem = this.infoboxElement,
                top = event.originalEvent.pageY + offsetY,
                left = event.originalEvent.pageX + offsetX,
                url = options.dataUrl + tagName + '?count=' + options.maxItems,
                displayResult = function () {
                    elem.html(html);
                    elem.css({ top: top, left: left });
                    elem.show();
                };

            if (event.pageX > window.screenWidth / 2) {
                left = event.pageX + leftSideAdjustment;
            }

            $.ajax({
                url: url,
                dataType: 'jsonp',
                success: function (data) {
                    if (data != null) {
                        html = '<h1>Popular Links for ' + tagName + '</h1><ul>';
                        for (i = 0; i < data.length - 1; i += 1) {
                            html += '<li><a href="' +
                                    data[i].u +
                                    '" target="_blank">' +
                                    data[i].d + '</a></li>';
                        }
                        html += '</ul>';
                    } else {
                        html = '<h1>Data Error</h1><p>The AJAX call returned null. This happens when using non-Internet Explorer browsers and is the expected behavior when viewing file based HTML files.</p><p>Please use Internet Explorer 9 and click the "Allow Blocked Content" button to run this QuickStart.</p>';
                    }
                    displayResult();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    html = '<h1>AJAX Error</h1>' +
                           '<p>The AJAX call returned the following error: ' +
                           jqXHR.statusText + '.</p>';
                    displayResult();
                }
            });
        },

        destroy: function () {
            $.Widget.prototype.destroy.call(this);
            this.infoboxElement.remove();
        }

    });
} (jQuery));
