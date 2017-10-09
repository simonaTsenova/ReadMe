using ReadMe.Models;

namespace ReadMe.Services.Contracts
{
    public interface IPublisherService
    {
        Publisher GetPublisherByName(string name);
    }
}
