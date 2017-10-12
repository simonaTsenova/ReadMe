using ReadMe.Models;
using System.Collections.Generic;

namespace ReadMe.Web.Models.Search
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
        }

        public SearchViewModel(ICollection<Genre> genres)
        {
            this.Genres = genres;
        }

        public string SearchPattern { get; set; }

        public string SearchType { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}