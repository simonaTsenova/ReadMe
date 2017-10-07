$('#search-pattern').keyup(function () {
    if ($('#search-pattern').val().length > 0) {
        $('#search-form').submit();
    } else {
        $('#search-results').empty();
    }
})

$("input[type='radio']").click(function () {
    if ($('#search-pattern').val().length > 0) {
        $("#search-form").submit();
    } else {
        $('#search-results').empty();
    }
})