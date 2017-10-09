using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IRatingService
    {
        IQueryable<Rating> GetAll();

        void AddRating(Guid bookId, string userId, int stars);

        void UpdateRating(Guid bookId, string userId, int stars);

        Rating GetByBookIdAndUserId(Guid bookId, string userId);
    }
}
