using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class BookViewModel : IMapFrom<Book>, ICustomMapping
    {
        public Guid Id { get; set; }

        [Required]
        [Remote("CheckTitleExists", "Books")]
        [StringLength(60, ErrorMessage = "Title must be between 4 and 60 characters long", MinimumLength = 4)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Published { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "ISBN must be between 10 and 13 characters long", MinimumLength = 10)]
        public string ISBN { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]{3,15} [a-zA-Z]{3,15}", ErrorMessage = "Author name must be in format - [firstname] [lastname]")]
        [Remote("CheckAuthorExists", "Books")]
        public string Author { get; set; }

        public string Summary { get; set; }

        public string PhotoUrl { get; set; }

        public string Language { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Publisher name must be between 2 and 40 characters long", MinimumLength = 2)]
        [Remote("CheckPublisherExists", "Books")]
        public string Publisher { get; set; }

        public Guid[] GenresIds { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(bookViewModel => bookViewModel.Id,
                    cfg => cfg.MapFrom(book => book.Id))
                .ForMember(bookViewModel => bookViewModel.Title,
                    cfg => cfg.MapFrom(book => book.Title))
                .ForMember(bookViewModel => bookViewModel.Published,
                    cfg => cfg.MapFrom(book => book.Published))
                .ForMember(bookViewModel => bookViewModel.ISBN,
                    cfg => cfg.MapFrom(book => book.ISBN))
                .ForMember(bookViewModel => bookViewModel.Author,
                    cfg => cfg.MapFrom(book => book.Author.FirstName + " " + book.Author.LastName))
                .ForMember(bookViewModel => bookViewModel.Summary,
                    cfg => cfg.MapFrom(book => book.Summary))
                .ForMember(bookViewModel => bookViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(book => book.PhotoUrl))
                .ForMember(bookViewModel => bookViewModel.Language,
                    cfg => cfg.MapFrom(book => book.Language))
                .ForMember(bookViewModel => bookViewModel.Publisher,
                    cfg => cfg.MapFrom(book => book.Publisher.Name))
                .ForMember(bookViewModel => bookViewModel.Genres,
                    cfg => cfg.MapFrom(book => book.Genres))
                .ForMember(bookInfoViewModel => bookInfoViewModel.IsDeleted,
                    cfg => cfg.MapFrom(book => book.IsDeleted));
        }
    }
}