using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class FacilitiesController(IFacilityService facilityService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(PropertyFacilityOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var propertyFacilities = await facilityService.GetAllAsync();

            return Ok(propertyFacilities);
        }

        [AllowAnonymous]
        [HttpGet("accessibility")]
        [ProducesResponseType(typeof(PropertyFacilityOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAccessibility()
        {
            var propertyAccessibility = await facilityService.GetAccessibilityAsync();

            return Ok(propertyAccessibility);
        }
    }
}
