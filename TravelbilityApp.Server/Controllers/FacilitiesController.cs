using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Facility;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class FacilitiesController(IFacilityService facilityService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(FacilityOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var facilities = await facilityService.GetAllAsync();

            return Ok(facilities);
        }

        [AllowAnonymous]
        [HttpGet("in-property")]
        [ProducesResponseType(typeof(FacilityOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllInProperty()
        {
            var propertyFacilities = await facilityService.GetAllInAsync();

            return Ok(propertyFacilities);
        }

        [AllowAnonymous]
        [HttpGet("in-room")]
        [ProducesResponseType(typeof(FacilityOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllInRoom()
        {
            var roomFacilities = await facilityService.GetAllInAsync(WhereStatus.OnlyInRoom);

            return Ok(roomFacilities);
        }

        [AllowAnonymous]
        [HttpGet("accessibility")]
        [ProducesResponseType(typeof(FacilityOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAccessibility()
        {
            var propertyAccessibility = await facilityService.GetAccessibilityAsync();

            return Ok(propertyAccessibility);
        }
    }
}
