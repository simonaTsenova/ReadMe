﻿using Microsoft.AspNet.Identity.EntityFramework;
using ReadMe.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMe.Models
{
    public class User : IdentityUser, IPerson, IDeletable
    {
        public User()
        {
        }

        public User(string username, string email)
            : this()
        {
            this.UserName = username;
            this.Email = email;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public string FavouriteQuote { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
