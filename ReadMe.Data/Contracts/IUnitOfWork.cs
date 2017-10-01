using System.Threading.Tasks;

namespace ReadMe.Data.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();
    }
}
