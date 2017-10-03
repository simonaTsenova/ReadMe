using Microsoft.AspNet.Identity.EntityFramework;
using ReadMe.Models.Contracts;
using System;
using System.Collections.Generic;

namespace ReadMe.Models
{
    public class User : IdentityUser, IDeletable
    {
        public User()
        {
            this.UserBooks = new HashSet<UserBook>();
        }

        public User(string email, string username, string firstName, string lastName)
            : this()
        {
            this.Email = email;
            this.UserName = username;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public string FavouriteQuote { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
