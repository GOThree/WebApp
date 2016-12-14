using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Db;
using WebApp.API.Models.Input;
using WebApp.API.Models.Output;

namespace WebApp.API.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessConverter _businessConverter;

        public BusinessService(IBusinessRepository repository, IBusinessConverter converter)
        {
            _businessRepository = repository;
            _businessConverter = converter;
        }

        public async Task<List<BusinessResponse>> GetAllAsync()
        {
            List<Business> dbBusinesses = await _businessRepository.GetAllAsync();
            List<BusinessResponse> convertedBusinesses = _businessConverter.ConvertToResponse(dbBusinesses);
            return convertedBusinesses;
        }

        public async Task<BusinessResponse> GetByIdAsync(int id)
        {
            Business dbBusiness = await _businessRepository.GetByIdAsync(id);
            BusinessResponse convertedBusiness = _businessConverter.ConvertToResponse(dbBusiness);
            return convertedBusiness;
        }

        public async Task<BusinessResponse> CreateAsync(BusinessRequest model)
        {
            Business dbBusinessModel = _businessConverter.CreateDbModel(model);
            Business createdDbBusiness = await _businessRepository.CreateAsync(dbBusinessModel);
            BusinessResponse convertedBusiness = _businessConverter.ConvertToResponse(createdDbBusiness);
            return convertedBusiness;
        }

        public async Task<BusinessResponse> UpdateAsync(int id, BusinessRequest model)
        {
            Business updatedDbBusiness = await _businessRepository.UpdateAsync(id, model);
            BusinessResponse convertedBusiness = _businessConverter.ConvertToResponse(updatedDbBusiness);
            return convertedBusiness;
        }
    }
}
