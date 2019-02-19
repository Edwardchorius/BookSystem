
$(function () {
    $('button[type="submit"]').attr('disabled', true);

    $('textarea').on('keyup', function () {
        var textarea_value = $("#textarea").val();

        if (textarea_value != '' && textarea_value.length > 20) {
            $('button[type="submit"]').attr('disabled', false);
        } else {
            $('button[type="submit"]').attr('disabled', true);
        }
    })
});