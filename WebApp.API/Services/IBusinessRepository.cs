using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Db;

namespace WebApp.API.Services
{
    public interface IBusinessRepository
    {
        Task<List<Business>> GetAllAsync();
        Task<Business> GetByIdAsync(int id);
        Task<Business> CreateAsync(Business newBusiness);
    }
}
