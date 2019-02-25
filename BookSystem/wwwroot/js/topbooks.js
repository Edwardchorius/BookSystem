$(function () {

    const $form = $('#top-books-form');

    $form.on('submit', function (e) {
        e.preventDefault();

        const data = $form.serialize();

        $.get($form.attr('action'), data, function (response) {
            $('#data').html(response);
        });
    });
});