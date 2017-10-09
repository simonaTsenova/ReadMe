using ReadMe.Models;
using System;

namespace ReadMe.Factories
{
    public interface IRatingFactory
    {
        Rating CreateRating(Guid bookId, string userId, int stars, DateTime ratedOn);
    }
}
