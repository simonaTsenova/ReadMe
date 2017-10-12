using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IReviewService
    {
        IQueryable<Review> GetAll();

        Review GetById(Guid id);

        IQueryable<Review> GetByBookId(Guid id);

        IQueryable<Review> GetByUserId(string id);

        Review GetByUserIdAndBookId(string userId, Guid bookId);

        Review AddReview(string userId, Guid bookId, string content);

        void DeleteReview(Guid id);
    }
}
