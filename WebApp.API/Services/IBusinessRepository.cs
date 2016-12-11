using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Db;
using WebApp.API.Models.Input;

namespace WebApp.API.Services
{
    public interface IBusinessRepository
    {
        Task<List<Business>> GetAllAsync();
        Task<Business> GetByIdAsync(int id);
        Task<Business> CreateAsync(Business newBusiness);
        Task<Business> UpdateAsync(int id, BusinessRequest dbBusinessModel);
    }
}
