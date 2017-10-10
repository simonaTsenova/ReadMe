using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMe.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IEfRepository<Publisher> publisherRepository;
        private readonly IPublisherFactory publisherFactory;
        private readonly IUnitOfWork unitOfWork;

        public PublisherService(IEfRepository<Publisher> publisherRepository, IPublisherFactory publisherFactory, IUnitOfWork unitOfWork)
        {
            if (publisherRepository == null)
            {
                throw new ArgumentNullException("Publisher repository cannot be null.");
            }

            if (publisherFactory == null)
            {
                throw new ArgumentNullException("Publisher factory cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.publisherRepository = publisherRepository;
            this.publisherFactory = publisherFactory;
            this.unitOfWork = unitOfWork;
        }

        public void AddPublisher(string name, string owner, string phone, string city, string address, string country, string website)
        {
            var publisher = this.publisherFactory.CreatePublisher(name, owner, phone, city,
                address, country, website);

            this.publisherRepository.Add(publisher);
            this.unitOfWork.Commit();
        }

        public void DeletePublisher(Guid id)
        {
            var publisher = this.publisherRepository.GetById(id);
            var dateDeleted = DateTime.Now;

            if(publisher != null)
            {
                publisher.IsDeleted = true;
                publisher.DeletedOn = dateDeleted;

                this.publisherRepository.Update(publisher);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<Publisher> GetAllAndDeleted()
        {
            return this.publisherRepository.AllAndDeleted;
        }

        public IQueryable<Publisher> GetPublisherById(Guid id)
        {
            var publisher = this.publisherRepository.All
                .Where(x => x.Id == id);

            return publisher;
        }

        public Publisher GetPublisherByName(string name)
        {
            var publisher = this.publisherRepository
                .All
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefault();

            return publisher;
        }

        public void RestorePublisher(Guid id)
        {
            var publisher = this.publisherRepository.GetById(id);

            if(publisher != null)
            {
                publisher.IsDeleted = false;
                publisher.DeletedOn = null;

                this.publisherRepository.Update(publisher);
                this.unitOfWork.Commit();
            }
        }

        public void UpdatePublisher(Guid id, string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
        {
            var publisher = this.publisherRepository.GetById(id);

            if(publisher != null)
            {
                publisher.Name = name;
                publisher.Owner = owner;
                publisher.PhoneNumber = phone;
                publisher.City = city;
                publisher.Address = address;
                publisher.Country = country;
                publisher.Website = website;
                publisher.LogoUrl = logoUrl;

                this.publisherRepository.Update(publisher);
                this.unitOfWork.Commit();
            }
        }
    }
}
