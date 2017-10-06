using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using AutoMapper;

namespace ReadMe.Web.Models.Books
{
    public class BookViewModel : IMapFrom<Book>, ICustomMapping
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime Published { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(bookViewModel => bookViewModel.Id,
                    cfg => cfg.MapFrom(book => book.Id))
                .ForMember(bookViewModel => bookViewModel.Title,
                    cfg => cfg.MapFrom(book => book.Title))
                .ForMember(bookViewModel => bookViewModel.Published,
                    cfg => cfg.MapFrom(book => book.Published))
                .ForMember(bookViewModel => bookViewModel.Author,
                    cfg => cfg.MapFrom(book => book.Author))
                .ForMember(bookViewModel => bookViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(book => book.PhotoUrl));
        }
    }
}