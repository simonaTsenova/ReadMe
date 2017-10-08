using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ReadMe.Web.Models.Books
{
    public class BookShortViewModel : IMapFrom<Book>, ICustomMapping
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public string PhotoUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Published { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookShortViewModel>()
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