using System.Threading.Tasks;

namespace tasks.domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}