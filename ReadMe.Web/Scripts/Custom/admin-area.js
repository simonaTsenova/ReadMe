$("button.edit-btn").click(function () {
    console.log(this);
    $(this).toggleClass("invisible");
    $(this).siblings("form").toggleClass("invisible");
});


function toggle() {
    $("#edit-form").toggleClass("invisible");
}
