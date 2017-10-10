using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IAuthorService
    {
        void AddAuthor(string firstName, string lastName, string nationality, int age, string biography, string website);

        Author GetAuthorByName(string firstName, string lastName);

        IQueryable<Author> GetAllAndDeleted();

        void UpdateAuthor(Guid id, string firstName, string lastName, string nationality, int age, string biography, string website, string photoUrl);

        void DeleteAuthor(Guid id);

        void RestoreAuthor(Guid id);

        IQueryable<Author> GetAuthorById(Guid authorId);
    }
}
