using System.Collections.Generic;
using WebApp.API.Models.Db;
using WebApp.API.Models.Output;

namespace WebApp.API.Services
{
    public interface IBusinessConverter
    {
        List<BusinessResponse> ConvertToResponse(List<Business> dbBusinesses);
    }
}
