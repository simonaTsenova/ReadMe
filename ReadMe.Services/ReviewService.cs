using System.Linq;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Data.Contracts;
using System;
using ReadMe.Factories;
using ReadMe.Data;
using System.Data.SqlClient;

namespace ReadMe.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IEfRepository<Review> reviewRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IReviewFactory reviewFactory;
        private readonly ReadMeDbContext context;

        public ReviewService(IEfRepository<Review> reviewRepository, IUnitOfWork unitOfWork, IReviewFactory reviewFactory, ReadMeDbContext context)
        {
            if (reviewRepository == null)
            {
                throw new ArgumentNullException("Review repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (reviewFactory == null)
            {
                throw new ArgumentNullException("Review factory cannot be null.");
            }

            this.reviewRepository = reviewRepository;
            this.unitOfWork = unitOfWork;
            this.reviewFactory = reviewFactory;
            this.context = context;
        }

        public Review AddReview(string userId, Guid bookId, string content)
        {
            var date = DateTime.Now;
            var review = this.reviewFactory.CreateReview(userId, bookId, content, date);
            this.reviewRepository.Add(review);
            this.unitOfWork.Commit();

            return review;
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

        public void DeleteReview(Guid id)
        {
            var review = this.reviewRepository.All
                .Where(r => r.Id == id)
                .FirstOrDefault();

            if(review != null)
            {
                this.reviewRepository.Delete(review);
                this.unitOfWork.Commit();
            }
        }
    }
}
