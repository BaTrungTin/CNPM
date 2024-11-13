using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Implementations;
using KoiDeliveryOrdering.Repositories.Interfaces;
using KoiDeliveryOrdering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Services.Implementations
{
    public class ServicePackageService : IServicePackageService
    {
        private readonly IServicePackageRepository _servicePackageRepository;

        public ServicePackageService(IServicePackageRepository servicePackageRepository)
        {
            _servicePackageRepository = servicePackageRepository;
        }

        public async Task<List<Servicepackage>> GetAllServices()
        {
            return await _servicePackageRepository.GetAllServices();
        }

        public async Task<Servicepackage> GetServiceById(int id)
        {
            var service = await _servicePackageRepository.GetServiceById(id);
            if (service == null)
            {
                throw new Exception("Gói dịch vụ không tồn tại.");
            }
            return service;
        }

        public async Task AddService(Servicepackage service)
        {
            await _servicePackageRepository.AddService(service);
        }

        public async Task UpdateService(Servicepackage service)
        {
            await _servicePackageRepository.UpdateService(service);
        }

        public async Task DeleteService(int id)
        {
            await _servicePackageRepository.DeleteService(id);
        }
    }
}
