﻿using ReadMe.Web.Models.Reviews;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Books
{
    public class BookDetailsViewModel
    {
        public BookDetailsViewModel()
        {
        }

        public BookDetailsViewModel(BookInfoViewModel bookInfoViewModel, ICollection<ReviewViewModel> reviewViewModels,
            ReviewViewModel formReviewViewModel)
        {
            this.BookInfoViewModel = bookInfoViewModel;
            this.ReviewViewModels = reviewViewModels;
            this.FormReviewViewModel = formReviewViewModel;
        }

        public BookInfoViewModel BookInfoViewModel { get; set; }

        public ICollection<ReviewViewModel> ReviewViewModels { get; set; }

        public ReviewViewModel FormReviewViewModel { get; set; }

        public UserBookViewModel UserBookViewModel { get; set; }
    }
}