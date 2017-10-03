using ReadMe.Models;

namespace ReadMe.Factories
{
    public interface IPublisherFactory
    {
        Publisher CreatePublisher(string name, string owner, string phone, string city, string address, string country, string website);
    }
}
