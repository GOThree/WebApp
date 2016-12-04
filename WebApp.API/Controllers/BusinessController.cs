using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.Output;
using WebApp.API.Services;

namespace WebApp.API.Controllers
{
    // [Authorize]
    [Route("businesses")]
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsync()
        {
            List<BusinessResponse> businesses = await _businessService.GetAllAsync();
            return Ok(businesses);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            BusinessResponse business = await _businessService.GetByIdAsync(id);
            return Ok(business);
        }
    }
}
