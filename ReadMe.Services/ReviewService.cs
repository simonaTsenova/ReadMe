using System.Linq;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Data.Contracts;
using System;
using ReadMe.Factories;

namespace ReadMe.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IEfRepository<Review> reviewRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IReviewFactory reviewFactory;

        public ReviewService(IEfRepository<Review> reviewRepository, IUnitOfWork unitOfWork, IReviewFactory reviewFactory)
        {
            if (reviewRepository == null)
            {
                throw new ArgumentNullException("Review repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.reviewRepository = reviewRepository;
            this.unitOfWork = unitOfWork;
            this.reviewFactory = reviewFactory;
        }

        public void AddReview(string userId, Guid bookId, string content)
        {
            var date = DateTime.Now;
            var review = this.reviewFactory.CreateReview(userId, bookId, content, date);
            this.reviewRepository.Add(review);
            this.unitOfWork.Commit();
        }

        public IQueryable<Review> GetAll()
        {
            var results = this.reviewRepository.All;

            return results;
        }

        public IQueryable<Review> GetByBookId(Guid id)
        {
            var results = this.reviewRepository.All
                .Where(r => r.BookId == id);

            return results;
        }

        public IQueryable<Review> GetByUserId(string id)
        {
            var results = this.reviewRepository.All
                .Where(r => r.UserId == id);

            return results;
        }

        public Review GetByUserIdAndBookId(string userId, Guid bookId)
        {
            var result = this.reviewRepository.All
                .Where(r => r.UserId == userId && r.BookId == bookId)
                .FirstOrDefault();

            return result;
        }
    }
}
