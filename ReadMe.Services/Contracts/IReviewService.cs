using ReadMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMe.Services.Contracts
{
    public interface IReviewService
    {
        IQueryable<Review> GetAll();

        IQueryable<Review> GetByBookId(Guid id);

        IQueryable<Review> GetByUserId(string id);

    }
}
