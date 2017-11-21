using System.Threading.Tasks;

namespace Vega.Persistent.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}