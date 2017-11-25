using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistent.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext _context;

        public PhotoRepository(VegaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await _context.PhotoSet.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }
    }
}
