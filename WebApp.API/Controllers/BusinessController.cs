using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.Input;
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
        [Route("{id}", Name = "GetBusinessById")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            BusinessResponse business = await _businessService.GetByIdAsync(id);
            if (business == null)
            {
                return NotFound();
            }

            return Ok(business);
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAsync([FromBody] BusinessRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessResponse newBusiness = await _businessService.CreateAsync(model);
            return CreatedAtRoute("GetBusinessById", new { id = newBusiness.Id }, newBusiness);
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] BusinessRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessResponse updatedBusiness = await _businessService.UpdateAsync(id, model);
            if (updatedBusiness == null)
            {
                return NotFound();
            }

            return Ok(updatedBusiness);
        }
    }
}
