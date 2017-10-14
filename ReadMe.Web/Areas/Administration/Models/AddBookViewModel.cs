using AutoMapper;
using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class AddBookViewModel : BookViewModel, IMapFrom<Book>, ICustomMapping
    {
        public AddBookViewModel()
        {
        }

        public AddBookViewModel(ICollection<Genre> genres)
        {
            this.Genres = genres;
        }

        [Required]
        [Remote("CheckTitleExists", "Books")]
        [StringLength(60, ErrorMessage = "Title must be between 4 and 60 characters long", MinimumLength = 4)]
        public override string Title { get => base.Title; set => base.Title = value; }

        public new void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, AddBookViewModel>()
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