$("button.edit-btn").click(function () {
    console.log(this);
    toggle();
})


function toggle() {
    $(".edit-btn").toggleClass("invisible");
    $("#edit-form").toggleClass("invisible");
}
