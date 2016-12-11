
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Input;
using WebApp.API.Models.Output;

namespace WebApp.API.Services
{
    public interface IBusinessService
    {
        Task<List<BusinessResponse>> GetAllAsync();
        Task<BusinessResponse> GetByIdAsync(int id);
        Task<BusinessResponse> CreateAsync(BusinessRequest model);
        Task<BusinessResponse> UpdateAsync(int id, BusinessRequest model);
    }
}
