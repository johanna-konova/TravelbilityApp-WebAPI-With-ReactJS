using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Room;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class BedTypesController(IBedTypeService bedTypeService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(RoomBedTypeOptionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            var bedTypes = await bedTypeService.GetAllAsync();

            return Ok(bedTypes);
        }
    }
}
