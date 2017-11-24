using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.Extensions;

namespace Vega.Persistent.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _context;

        public VehicleRepository(VegaDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> GetVehicle(int id, bool isIncludeRelative=true)
        {
            if (isIncludeRelative)
            {
                 return await _context.VehicleSet.Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                    .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                    .SingleOrDefaultAsync(v => v.Id == id);
            }
            return await _context.VehicleSet.FindAsync(id);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery vehicleQuery)
        {
            var result = new QueryResult<Vehicle>();
            var query = _context.VehicleSet.Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make).AsQueryable();
            if (vehicleQuery.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == vehicleQuery.MakeId.Value);
            if (vehicleQuery.ModelId.HasValue)
                query = query.Where(v => v.ModelId == vehicleQuery.ModelId.Value);
            
            Expression<Func<Vehicle, object>> exp;

            var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                };

            query = query.ApplyOrdering(vehicleQuery, columnMap);
            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(vehicleQuery);

            result.Items = await query.ToListAsync();
            return result;
        }

      

        public void Add(Vehicle vehicle)
        {
            _context.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Remove(vehicle);
        }

    }
}
