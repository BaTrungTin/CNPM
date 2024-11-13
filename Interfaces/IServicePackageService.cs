using KoiDeliveryOrdering.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Services.Interfaces
{
    public interface IServicePackageService
    {
        Task<List<Servicepackage>> GetAllServices();
        Task<Servicepackage> GetServiceById(int id);
        Task AddService(Servicepackage service);
        Task UpdateService(Servicepackage service);
        Task DeleteService(int id);
    }
}
