function removeReview() {
    let $this = $(this);
    let $review = $this.closest('.mbr-testimonial');
    $review.remove();
}