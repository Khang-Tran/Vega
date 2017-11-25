using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Persistent.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}