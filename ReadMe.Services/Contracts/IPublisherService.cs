using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IPublisherService
    {
        void AddPublisher(string name, string owner, string phone, string city, 
            string address, string country, string website);

        Publisher GetPublisherByName(string name);

        IQueryable<Publisher> GetAllAndDeleted();

        void UpdatePublisher(Guid id, string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl);

        void DeletePublisher(Guid id);

        void RestorePublisher(Guid id);

        IQueryable<Publisher> GetPublisherById(Guid id);
    }
}
