using Microsoft.AspNetCore.Mvc;
using RealEstateBackend.Services;
using System.Threading.Tasks;

namespace RealEstateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IRentCastService _rentCastService;

        public PropertyController(IRentCastService rentCastService)
        {
            _rentCastService = rentCastService;
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetPropertyById(string propertyId)
        {
            try
            {
                var propertyIdDetails = await _rentCastService.GetPropertyByIdAsync(propertyId);
                return Ok(propertyIdDetails);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{propertyHistory}")]
        public async Task<IActionResult> GetpropertyHistoryDetails(string propertyHistory)
        {
            try
            {
                var propertyHistoryDetails = await _rentCastService.GetPropertyHistoryAsync(propertyHistory);
                return Ok(propertyHistoryDetails);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{address}")]
        public async Task<IActionResult> GetPropertyDetails(string address)
        {
            try
            {
                var propertyDetails = await _rentCastService.GetPropertyAddressAsync(address);
                return Ok(propertyDetails);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
