using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Room;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class RoomTypesController(IRoomTypeService roomTypeService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(RoomTypeOptionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            var roomTypes = await roomTypeService.GetAllAsync();

            return Ok(roomTypes);
        }
    }
}
