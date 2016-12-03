using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Db;
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
    }
}
