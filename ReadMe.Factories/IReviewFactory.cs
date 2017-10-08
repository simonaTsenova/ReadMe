using ReadMe.Models;
using System;

namespace ReadMe.Factories
{
    public interface IReviewFactory
    {
        Review CreateReview(string userId, Guid bookId, string content, DateTime postedOn);
    }
}
