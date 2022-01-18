function custom_dropdowns() {
    $('select').each(function (i, select) {
        if (!$(this).next().hasClass('dropdown-select')) {
            var icon = "";
            if ($(this).parent().hasClass("form-group") && $(this).parent().find(".input-icon").children().hasClass("fas")){
                icon = '<div class="input-icon baseIcon">' + $(this).parent().find(".input-icon").html() + '</div>' ;
                $(this).parent().attr("style", "border: 0 !important;");
                $(this).parent().find(".input-icon").attr("style", "display: none !important;");
            }
            $(this).after('<div id="dropdown-custom-' + i + '" class="dropdown-select wide ' + ($(this).attr('class') || '') + '" tabindex="0"><span class="current">' + icon +'</span><div class="list"><ul></ul></div></div>');
            var dropdown = $(this).next();
            var options = $(select).find('option');
            var selected = $(this).find('option:selected');
            dropdown.find('.current').html(icon + (selected.data('display-text') || selected.text()));
            options.each(function (j, o) {
                var attr = $(o).attr('hidden');
                if (typeof attr == 'undefined' || attr == false) {
                    var display = $(o).data('display-text') || '';
                    dropdown.find('ul').append('<li class="option ' + ($(o).is(':selected') ? 'selected' : '') + '" data-value="' + $(o).val() + '" data-display-text="' + display + '">' + $(o).text() + '</li>');
                }
            });
            var attr = $(this).attr('searching');
            if (typeof attr !== 'undefined' && attr !== false) {
                $(this).next().find('ul').before('<div class="dd-search"><input id="search-custom-' + i + '"autocomplete="off" placeholder="Recherche.." onkeyup="filter(\'search-custom-\',\'' + i + '\')" class="dd-searchbox" type="text"></div>');
            }
        }
    });

    $('li[class="option selected"]').append(' <i class="fas fa-check prefix"></i>');


}

// Event listeners

// Open/close
$(document).on('click', '.dropdown-select', function (event) {
    if ($(event.target).hasClass('dd-searchbox')) {
        return;
    }
    $('.dropdown-select').not($(this)).removeClass('open');
    $(this).toggleClass('open');
    if ($(this).hasClass('open')) {
        $(this).find('.option').attr('tabindex', 0);
        $(this).find('.selected').focus();

    } else {
        $(this).find('.option').removeAttr('tabindex');
        $(this).focus();
    }
});

// Close when clicking outside
$(document).on('click', function (event) {
    if ($(event.target).closest('.dropdown-select').length === 0) {
        $('.dropdown-select').removeClass('open');
        $('.dropdown-select .option').removeAttr('tabindex');
    }
    event.stopPropagation();
});

function filter(text_filter, i) {
    var valThis = $('#' + text_filter + i).val();
    $('#dropdown-custom-' + i + ' ul > li').each(function () {
        var text = $(this).text();
        (text.toLowerCase().indexOf(valThis.toLowerCase()) > -1) ? $(this).show() : $(this).hide();
    });
};
// Search

// Option click
$(document).on('click', '.dropdown-select .option', function (event) {
    $(this).closest('.list').find('.selected').removeClass('selected');
    $(this).closest('.list').find('.prefix').remove();
    $(this).addClass('selected');
    var text = $(this).data('display-text') || $(this).text();
    $(this).append("<i class=\"fas fa-check prefix\"></i>");
    var icon = "";
    if (typeof $(this).find("i").attr("class") !== 'undefined') {
        icon = '<i class=\"' + $(this).find("i").attr("class") + '\"></i>';
    }

    //Affiche in top
    if ($(this).closest('.dropdown-select').parent().hasClass("form-group") && $(this).closest('.dropdown-select').parent().find(".input-icon").children().hasClass("fas")) {
        if (icon == "") {
            icon = $(this).closest('.dropdown-select').parent().find(".input-icon").html();
            $(this).closest('.dropdown-select').find('.current').html('<div class="input-icon baseIcon">' + icon + '</div> ' + text)
        } else {
            $(this).closest('.dropdown-select').find('.current').html('<div class="input-icon baseIcon" style="display:none" >' + $(this).closest('.dropdown-select').parent().find(".input-icon").html() + '</div> <div class="input-icon iconPerso">' + icon + "</div>" + text);
        }
        
    } else {
        $(this).closest('.dropdown-select').find('.current').text(text);
    }
    $(this).closest('.dropdown-select').prev('select').val($(this).data('value')).trigger('change');
});

// Keyboard events
$(document).on('keydown', '.dropdown-select', function (event) {
    var focused_option = $($(this).find('.list .option:focus')[0] || $(this).find('.list .option.selected')[0]);
    // Space or Enter
    //if (event.keyCode == 32 || event.keyCode == 13) {
    if (event.keyCode == 13) {
        if ($(this).hasClass('open')) {
            focused_option.trigger('click');
        } else {
            $(this).trigger('click');
        }
        return false;
        // Down
    } else if (event.keyCode == 40) {
        if (!$(this).hasClass('open')) {
            $(this).trigger('click');
        } else {
            focused_option.next().focus();
        }
        return false;
        // Up
    } else if (event.keyCode == 38) {
        if (!$(this).hasClass('open')) {
            $(this).trigger('click');
        } else {
            var focused_option = $($(this).find('.list .option:focus')[0] || $(this).find('.list .option.selected')[0]);
            focused_option.prev().focus();
        }
        return false;
        // Esc
    } else if (event.keyCode == 27) {
        if ($(this).hasClass('open')) {
            $(this).trigger('click');
        }
        return false;
    }
});

$(document).ready(function () {
    custom_dropdowns();
});