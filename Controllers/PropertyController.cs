using Microsoft.AspNetCore.Mvc;
using RealEstateBackend.Services;
using System.Threading.Tasks;

namespace RealEstateBackend.Controllers
{
    [ApiController]
    [Route("api/property")]
    public class PropertyController : ControllerBase
    {
        private readonly IRentCastService _rentCastService;

        public PropertyController(IRentCastService rentCastService)
        {
            _rentCastService = rentCastService;
        }

        [HttpGet("{address}")]
        public async Task<IActionResult> GetPropertyDetails(string address)
        {
            try
            {
                var propertyDetails = await _rentCastService.GetPropertyDetailsAsync(address);
                return Ok(propertyDetails);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
