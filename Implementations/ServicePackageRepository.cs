using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Repositories.Implementations
{
    public class ServicePackageRepository : IServicePackageRepository
    {
        private readonly KoiDeliveryOrderingContext _dbContext;

        public ServicePackageRepository(KoiDeliveryOrderingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Servicepackage>> GetAllServices()
        {
            return await _dbContext.Servicepackages.ToListAsync();
        }

        public async Task<Servicepackage?> GetServiceById(int id)
        {
            return await _dbContext.Servicepackages.FirstOrDefaultAsync(s => s.PackageId == id);
        }

        public async Task AddService(Servicepackage service)
        {
            _dbContext.Servicepackages.Add(service);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateService(Servicepackage service)
        {
            _dbContext.Servicepackages.Update(service);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteService(int id)
        {
            var service = await _dbContext.Servicepackages.FindAsync(id);
            if (service != null)
            {
                _dbContext.Servicepackages.Remove(service);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
