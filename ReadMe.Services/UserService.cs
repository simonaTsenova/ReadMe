﻿using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using System;
using System.Data.Entity;
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

        public IQueryable<User> GetUserByUsername(string username)
        {
            var user = this.userRepository.All
                .Where(u => u.UserName == username)
                .Include(u => u.UserBooks);

            return user;
        }

        public User EditUser(string id, string email, string firstName, string lastName, string nationality, int age, string favouriteQuote)
        {
            var user = this.userRepository.GetById(id);
            
            if(user != null)
            {
                user.Email = email;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Nationality = nationality;
                user.Age = age;
                user.FavouriteQuote = favouriteQuote;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }

            return user;
        }

        public User GetUserById(string id)
        {
            var user = this.userRepository.GetById(id);

            return user;
        }

        public IQueryable<User> GetAll()
        {
            return this.userRepository.All;
        }

        public void DeleteUser(string userId)
        {
            var user = this.userRepository.GetById(userId);
            var dateDeleted = DateTime.Now;

            if(user != null)
            {
                user.IsDeleted = true;
                user.DeletedOn = DateTime.Now;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }
        }

        public void RestoreUser(string userId)
        {
            var user = this.userRepository.GetById(userId);

            if (user != null)
            {
                user.IsDeleted = false;
                user.DeletedOn = null;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<User> GetAllAndDeleted()
        {
            return this.userRepository.AllAndDeleted;
        }
    }
}
