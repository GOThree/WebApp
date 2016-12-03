using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models.Db;

namespace WebApp.API.Services
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ApplicationDbContext _context;

        public BusinessRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Business>> GetAllAsync()
        {
            List<Business> businesses = await _context.Businesses.ToListAsync();
            return businesses;
        }
    }
}
