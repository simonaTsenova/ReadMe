$('label').click(function () {
    console.log(this);
    let value = $(this).attr("rate-value");
    console.log(value);
    $('#rating').val(value);
    $('#rating-form').submit();
})