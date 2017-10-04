using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using System;
using System.Linq;

namespace ReadMe.Services
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IEfRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            if(userRepository == null)
            {
                throw new ArgumentNullException("User repository cannot be null.");
            }

            if(unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public User GetUserByUsername(string username)
        {
            var user = this.userRepository.All
                .FirstOrDefault(u => u.UserName == username);

            return user;
        }
    }
}
