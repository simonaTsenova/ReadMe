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

        public Publisher GetPublisherByName(string name)
        {
            var publisher = this.publisherRepository
                .All
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefault();

            return publisher;
        }
    }
}
