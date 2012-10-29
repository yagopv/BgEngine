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
    $(document).ready(function () {
        $(".post-slider").slider({
            value: $("#Pager_PostPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_PostPerPage").val(ui.value);
            }
        });
        $(".comment-slider").slider({
            value: $("#Pager_CommentsPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_CommentsPerPage").val(ui.value);
            }
        });
        $(".categories-slider").slider({
            value: $("#Pager_CategoriesPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_CategoriesPerPage").val(ui.value);
            }
        });
        $(".tags-slider").slider({
            value: $("#Pager_TagsPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_TagsPerPage").val(ui.value);
            }
        });
        $(".users-slider").slider({
            value: $("#Pager_UsersPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_UsersPerPage").val(ui.value);
            }
        });
        $(".roles-slider").slider({
            value: $("#Pager_RolesPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_RolesPerPage").val(ui.value);
            }
        });
        $(".images-slider").slider({
            value: $("#Pager_SearchImagesPerPage").val(),
            min: 5,
            max: 100,
            step: 5,
            slide: function (event, ui) {
                $("#Pager_SearchImagesPerPage").val(ui.value);
            }
        });
        $(".videos-slider").slider({
            value: $("#Pager_SearchVideosPerPage").val(),
            min: 4,
            max: 120,
            step: 4,
            slide: function (event, ui) {
                $("#Pager_SearchVideosPerPage").val(ui.value);
            }
        });
        $(".homepost-slider").slider({
            value: $("#Pager_HomeIndexPostsPerPage").val(),
            min: 4,
            max: 120,
            step: 2,
            slide: function (event, ui) {
                $("#Pager_HomeIndexPostsPerPage").val(ui.value);
            }
        });
        $("form").form();
        $(".slider-control").removeClass("ui-state-default ui-corner-all");
    });
});

function OptionsUpdated() {
    $("#updated-message").show("1000", interval);
}

function interval() {
    setTimeout(function () {
        $("#updated-message").hide("1000");
    }, 5000);
}