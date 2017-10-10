using AutoMapper;
using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;

namespace ReadMe.Web.Models.Authors
{
    public class AuthorViewModel : IMapFrom<Author>, ICustomMapping
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public string Biography { get; set; }

        public string Website { get; set; }

        public string PhotoUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Author, AuthorViewModel>()
                .ForMember(authorViewModel => authorViewModel.Id,
                    cfg => cfg.MapFrom(author => author.Id))
                .ForMember(authorViewModel => authorViewModel.Nationality,
                    cfg => cfg.MapFrom(author => author.Nationality))
                .ForMember(authorViewModel => authorViewModel.Age,
                    cfg => cfg.MapFrom(author => author.Age))
                .ForMember(authorViewModel => authorViewModel.Biography,
                    cfg => cfg.MapFrom(author => author.Biography))
                .ForMember(authorViewModel => authorViewModel.FullName,
                    cfg => cfg.MapFrom(author => author.FirstName + " " + author.LastName))
                .ForMember(authorViewModel => authorViewModel.Website,
                    cfg => cfg.MapFrom(author => author.Website))
                .ForMember(authorViewModel => authorViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(author => author.PhotoUrl));
        }
    }
}