using AutoMapper;
using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Books
{
    public class BookDetailsViewBodel : IMapFrom<Book>, ICustomMapping
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime Published { get; set; }

        public string ISBN { get; set; }

        public Author Author { get; set; }

        public string Summary { get; set; }

        public double Rating { get; set; }

        public string PhotoUrl { get; set; }

        public string Language { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookDetailsViewBodel>()
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Id,
                    cfg => cfg.MapFrom(book => book.Id))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Title,
                    cfg => cfg.MapFrom(book => book.Title))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Published,
                    cfg => cfg.MapFrom(book => book.Published))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.ISBN,
                    cfg => cfg.MapFrom(book => book.ISBN))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Author,
                    cfg => cfg.MapFrom(book => book.Author))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Summary,
                    cfg => cfg.MapFrom(book => book.Summary))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Rating,
                    cfg => cfg.MapFrom(book => book.Rating))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(book => book.PhotoUrl))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Language,
                    cfg => cfg.MapFrom(book => book.Language))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Publisher,
                    cfg => cfg.MapFrom(book => book.Publisher))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.Genres,
                    cfg => cfg.MapFrom(book => book.Genres))
                .ForMember(bookDetailsViewModel => bookDetailsViewModel.UserBooks,
                    cfg => cfg.MapFrom(book => book.UserBooks));
        }
    }
}