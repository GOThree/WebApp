using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.API.Models.Db;

namespace WebApp.API.Services
{
    public interface IBusinessRepository
    {
        Task<List<Business>> GetAllAsync();
    }
}
