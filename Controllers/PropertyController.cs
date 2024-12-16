using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RealEstateBackend.Models;
using RealEstateBackend.Services;

namespace RealEstateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ZillowService _zillowService;

        public PropertyController(ZillowService zillowService)
        {
            _zillowService = zillowService;
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetProperty(string propertyId)
        {
            var propertyData = await _zillowService.GetPropertyById(propertyId);

            if (propertyData == null)
            {
                return NotFound(new { Message = "Property not found", PropertyId = propertyId });
            }

            return Ok(propertyData);
        }

    }
}
