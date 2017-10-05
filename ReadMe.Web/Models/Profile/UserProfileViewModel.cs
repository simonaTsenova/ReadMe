using AutoMapper;
using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Profile
{
    public class UserProfileViewModel : IMapFrom<User>, ICustomMapping
    {
        public UserProfileViewModel(string email, string username, string fullname, string nationality,
            int age, string favouriteQuote, string photoUrl, ICollection<UserBook> userBooks, bool isOwner)
        {
            this.Email = email;
            this.UserName = username;
            this.FullName = fullname;
            this.Nationality = nationality;
            this.Age = age;
            this.FavouriteQuote = favouriteQuote;
            this.PhotoUrl = photoUrl;
            this.UserBooks = userBooks;
            this.isOwner = isOwner;
        }

        public string Email { get; set; }
       
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public string FavouriteQuote { get; set; }

        public string PhotoUrl { get; set; }

        public bool isOwner { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserProfileViewModel>()
                .ForMember(userProfileViewModel => userProfileViewModel.Email,
                    cfg => cfg.MapFrom(user => user.Email))
                .ForMember(userProfileViewModel => userProfileViewModel.UserName,
                    cfg => cfg.MapFrom(user => user.UserName))
                .ForMember(userProfileViewModel => userProfileViewModel.FullName,
                    cfg => cfg.MapFrom(user => user.FirstName + " " + user.LastName))
                .ForMember(userProfileViewModel => userProfileViewModel.Nationality,
                    cfg => cfg.MapFrom(user => user.Nationality))
                .ForMember(userProfileViewModel => userProfileViewModel.Age,
                    cfg => cfg.MapFrom(user => user.Age))
                .ForMember(userProfileViewModel => userProfileViewModel.FavouriteQuote,
                    cfg => cfg.MapFrom(user => user.FavouriteQuote))
                .ForMember(userProfileViewModel => userProfileViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(user => user.PhotoUrl))
                .ForMember(userProfileViewModel => userProfileViewModel.UserBooks,
                    cfg => cfg.MapFrom(user => user.UserBooks));
        }
    }
}