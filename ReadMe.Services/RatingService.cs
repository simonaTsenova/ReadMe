﻿using System;
using ReadMe.Services.Contracts;
using ReadMe.Models;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using System.Linq;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services
{
    public class RatingService : IRatingService
    {
        private readonly IEfRepository<Rating> ratingRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRatingFactory ratingFactory;
        private readonly IDateTimeProvider provider;

        public RatingService(IEfRepository<Rating> ratingRepository, IUnitOfWork unitOfWork,
            IRatingFactory ratingFactory, IDateTimeProvider provider)
        {
            if (ratingRepository == null)
            {
                throw new ArgumentNullException("Rating repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (ratingFactory == null)
            {
                throw new ArgumentNullException("Rating factory cannot be null.");
            }

            if (provider == null)
            {
                throw new ArgumentNullException("DateTime provider cannot be null.");
            }

            this.ratingRepository = ratingRepository;
            this.unitOfWork = unitOfWork;
            this.ratingFactory = ratingFactory;
            this.provider = provider;
        }

        public void AddRating(Guid bookId, string userId, int stars)
        {
            var date = this.provider.GetCurrentTime();
            var rating = this.ratingFactory.CreateRating(bookId, userId, stars, date);
            this.ratingRepository.Add(rating);
            this.unitOfWork.Commit();
        }

        public IQueryable<Rating> GetAll()
        {
            return this.ratingRepository.All;
        }

        public Rating GetByBookIdAndUserId(Guid bookId, string userId)
        {
            var rating = this.ratingRepository.All
                .Where(r => r.BookId == bookId && r.UserId == userId)
                .FirstOrDefault();

            return rating;
        }

        public void UpdateRating(Guid bookId, string userId, int stars)
        {
            var rating = this.ratingRepository.All
                .Where(r => r.BookId == bookId && r.UserId == userId)
                .FirstOrDefault();

            if (rating != null)
            {
                rating.Stars = stars;

                this.ratingRepository.Update(rating);
                this.unitOfWork.Commit();
            }
        }

    }
}
