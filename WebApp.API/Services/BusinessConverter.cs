using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.API.Models.Db;
using WebApp.API.Models.Input;
using WebApp.API.Models.Output;

namespace WebApp.API.Services
{
    public class BusinessConverter : IBusinessConverter
    {
        public List<BusinessResponse> ConvertToResponse(List<Business> dbBusinesses)
        {
            List<BusinessResponse> result = dbBusinesses.Select(b => ConvertToResponse(b)).ToList();
            return result;
        }

        public BusinessResponse ConvertToResponse(Business dbBusiness)
        {
            if (dbBusiness == null)
            {
                return null;
            }

            BusinessResponse result = new BusinessResponse
            {
                Id = dbBusiness.Id,
                Name = dbBusiness.Name
            };

            return result;
        }

        public Business CreateDbModel(BusinessRequest model)
        {
            Business result = new Business
            {
                Name = model?.Name
            };

            return result;
        }
    }
}
