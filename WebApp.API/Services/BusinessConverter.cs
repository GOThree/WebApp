using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.API.Models.Db;
using WebApp.API.Models.Output;

namespace WebApp.API.Services
{
    public class BusinessConverter : IBusinessConverter
    {
        public List<BusinessResponse> ConvertToResponse(List<Business> dbBusinesses)
        {
            List<BusinessResponse> result = dbBusinesses.Select(b => new BusinessResponse
            {
                Id = b.Id
            }).ToList();
            return result;
        }
    }
}
