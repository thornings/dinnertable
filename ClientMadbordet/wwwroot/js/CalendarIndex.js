// calendar index

$(document).ready(function () {
    /* hovereffect on totals menu */
    $('.infoAnimationGroup').on("click", function () {
        var group = $(this);
        var groupSpecials = group.find('.totals-specials');

        if (groupSpecials.length > 0) {
            group.css('overflow-y', 'hidden');
            if ($(this).hasClass('col-1')) {
                $(this).toggleClass('col-12 col-1').promise().done(
                    setTimeout(function () {
                        groupSpecials.show();
                    }, 400))
            } else {
                groupSpecials.fadeOut(100);
                $(this).toggleClass('col-12 col-1');
            }
        }
    });
});