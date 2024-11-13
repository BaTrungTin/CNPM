using KoiDeliveryOrdering.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Repositories.Interfaces
{
    public interface IServicePackageRepository
    {
        Task<List<Servicepackage>> GetAllServices();
        Task<Servicepackage?> GetServiceById(int id);
        Task AddService(Servicepackage service);
        Task UpdateService(Servicepackage service);
        Task DeleteService(int id);
    }
}
