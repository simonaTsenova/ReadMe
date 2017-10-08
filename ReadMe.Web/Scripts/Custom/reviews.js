function removeReview() {
    let $this = $(this);
    let $review = $this.closest('.mbr-testimonial');
    $review.remove();
}

function addReview() {
    console.log(this);
}