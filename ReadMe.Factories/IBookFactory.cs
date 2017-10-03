using ReadMe.Models;
using System;
using System.Collections.Generic;

namespace ReadMe.Factories
{
    public interface IBookFactory
    {
        Book CreateBook(string title, DateTime published, string isbn,
                    Author author, string summary, string language,
                    Publisher publisher, ICollection<Genre> genres);
    }
}
