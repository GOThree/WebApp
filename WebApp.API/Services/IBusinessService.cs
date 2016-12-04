
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Output;

namespace WebApp.API.Services
{
    public interface IBusinessService
    {
        Task<List<BusinessResponse>> GetAllAsync();
        Task<BusinessResponse> GetByIdAsync(int id);
    }
}
