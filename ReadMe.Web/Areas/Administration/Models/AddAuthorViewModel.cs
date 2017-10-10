using AutoMapper;
using ReadMe.Models;
using ReadMe.Web.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReadMe.Web.Areas.Administration.Models
{
    public class AddAuthorViewModel : AuthorViewModel, IMapFrom<Author>, ICustomMapping
    {
        [Required]
        [RegularExpression("[a-zA-Z]{3,15} [a-zA-Z]{3,20}", ErrorMessage = "Author name must be in format - [firstname] [lastname]")]
        [Remote("CheckAuthorExists", "Authors")]
        public override string FullName { get => base.FullName; set => base.FullName = value; }

        public new void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Author, AddAuthorViewModel>()
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