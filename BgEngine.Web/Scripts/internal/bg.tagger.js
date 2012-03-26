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

    var timer,
        hideAfter = 1000; // ms
    // could've been an option

    $.widget('bg.tagger', {

        widgetEventPrefix: 'tag',

        options: {
            activated: null,
            deactivated: null
        },

        _create: function () {
            var that = this,
                name = this.name,
                tag = this.element.text();

            this.element
            .addClass('bg-tagged')
            .bind('mouseenter.' + name, function (event) {
                clearTimeout(timer);
                that._trigger('activated', event, {name: tag});
            })
            .bind('mouseleave.' + name, function () {
                timer = setTimeout(function () {
                    that._trigger('deactivated');
                }, hideAfter);
            });
        },

        destroy: function () {
            $.Widget.prototype.destroy.apply(this, arguments);
            this.element.removeClass('bg-tagged');
        }

    });

} (jQuery));
