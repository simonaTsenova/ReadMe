using AutoMapper;
using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadMe.Web.Models.Profile
{
    public class UserDetailsViewModel : IMapFrom<User>, ICustomMapping
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
       
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Nationality { get; set; }

        [Range(0, 150)]
        public int Age { get; set; }

        public string FavouriteQuote { get; set; }

        public string PhotoUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDetailsViewModel>()
                .ForMember(userProfileViewModel => userProfileViewModel.Id,
                    cfg => cfg.MapFrom(user => user.Id))
                .ForMember(userProfileViewModel => userProfileViewModel.Email,
                    cfg => cfg.MapFrom(user => user.Email))
                .ForMember(userProfileViewModel => userProfileViewModel.UserName,
                    cfg => cfg.MapFrom(user => user.UserName))
                .ForMember(userProfileViewModel => userProfileViewModel.FirstName,
                    cfg => cfg.MapFrom(user => user.FirstName))
                .ForMember(userProfileViewModel => userProfileViewModel.LastName,
                    cfg => cfg.MapFrom(user => user.LastName))
                .ForMember(userProfileViewModel => userProfileViewModel.FullName,
                    cfg => cfg.MapFrom(user => user.FirstName + " " + user.LastName))
                .ForMember(userProfileViewModel => userProfileViewModel.Nationality,
                    cfg => cfg.MapFrom(user => user.Nationality))
                .ForMember(userProfileViewModel => userProfileViewModel.Age,
                    cfg => cfg.MapFrom(user => user.Age))
                .ForMember(userProfileViewModel => userProfileViewModel.FavouriteQuote,
                    cfg => cfg.MapFrom(user => user.FavouriteQuote))
                .ForMember(userProfileViewModel => userProfileViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(user => user.PhotoUrl));
        }
    }
}