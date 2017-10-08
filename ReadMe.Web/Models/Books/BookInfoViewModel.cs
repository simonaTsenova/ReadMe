using AutoMapper;
using ReadMe.Models;
using ReadMe.Models.Enumerations;
using ReadMe.Web.Infrastructure;
using System;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Books
{
    public class BookInfoViewModel : IMapFrom<Book>, ICustomMapping
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

        public ReadStatus CurrentStatus { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookInfoViewModel>()
                .ForMember(bookInfoViewModel => bookInfoViewModel.Id,
                    cfg => cfg.MapFrom(book => book.Id))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Title,
                    cfg => cfg.MapFrom(book => book.Title))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Published,
                    cfg => cfg.MapFrom(book => book.Published))
                .ForMember(bookInfoViewModel => bookInfoViewModel.ISBN,
                    cfg => cfg.MapFrom(book => book.ISBN))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Author,
                    cfg => cfg.MapFrom(book => book.Author))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Summary,
                    cfg => cfg.MapFrom(book => book.Summary))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Rating,
                    cfg => cfg.MapFrom(book => book.Rating))
                .ForMember(bookInfoViewModel => bookInfoViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(book => book.PhotoUrl))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Language,
                    cfg => cfg.MapFrom(book => book.Language))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Publisher,
                    cfg => cfg.MapFrom(book => book.Publisher))
                .ForMember(bookInfoViewModel => bookInfoViewModel.Genres,
                    cfg => cfg.MapFrom(book => book.Genres))
                .ForMember(bookInfoViewModel => bookInfoViewModel.UserBooks,
                    cfg => cfg.MapFrom(book => book.UserBooks));
        }
    }
}