using System.Linq;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Data.Contracts;
using System;

namespace ReadMe.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IEfRepository<Review> reviewRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReviewService(IEfRepository<Review> reviewRepository, IUnitOfWork unitOfWork)
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
    }
}
