using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Room;
using TravelbilityApp.WebAPI.Filters;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class RoomsController(
        IValidator<RoomInputDto> validator,
        IRoomService roomService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MainRoomDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        public async Task<IActionResult> GetAll([FromQuery] Guid propertyId)
        {
            var roomsData = await roomService.GetAllByPropertyIdAsync(propertyId);

            return Ok(roomsData);
        }

        [AllowAnonymous]
        [HttpGet("detailed")]
        [ProducesResponseType(typeof(IEnumerable<RoomShortDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        public async Task<IActionResult> GetAllDetailed([FromQuery] Guid propertyId)
        {
            var roomsData = await roomService.GetAllDetailedByPropertyIdAsync(propertyId);

            return Ok(roomsData);
        }

        [AllowAnonymous]
        [HttpGet("{roomId:guid}")]
        [ProducesResponseType(typeof(RoomDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [ExistingRoom]
        public async Task<IActionResult> GetById(Guid roomId, [FromQuery] Guid propertyId)
        {
            var roomData = await roomService.GetByIdAndPropertyIdAsync(roomId, propertyId);

            return Ok(roomData);
        }

        [AllowAnonymous]
        [HttpGet("{roomId:guid}/for-edit")]
        [ProducesResponseType(typeof(RoomForEditDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [ExistingRoom]
        public async Task<IActionResult> GetForEditById(Guid roomId, [FromQuery] Guid propertyId)
        {
            var roomData = await roomService.GetForEditByIdAndPropertyIdAsync(roomId, propertyId);

            return Ok(roomData);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainRoomDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [PropertyPublisher]
        public async Task<IActionResult> Create([FromBody] RoomInputDto dto)
        {
            var result = await validator.ValidateAsync(dto);

            if (result.IsValid == false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid == false &&
                ModelState.Any(e => e.Key.StartsWith("ImageUrls[") == false))
            {
                return BadRequest(ModelState);
            }

            var outputDto = await roomService.CreateAsync(dto);

            return Created(string.Empty, outputDto);
        }

        [HttpPut("{roomId:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainRoomDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [PropertyPublisher]
        [ExistingRoom]
        public async Task<IActionResult> Edit(Guid roomId, [FromBody] RoomInputDto dto)
        {
            var result = await validator.ValidateAsync(dto);

            if (result.IsValid == false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid == false &&
                ModelState.Any(e => e.Key != "roomId" && e.Key.StartsWith("ImageUrls[") == false))
            {
                return BadRequest(ModelState);
            }

            var outputDto = await roomService.EditAsync(roomId, dto);

            return Created(string.Empty, outputDto);
        }

        [HttpDelete("{roomId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [PropertyPublisher]
        [ExistingRoom]
        public async Task<IActionResult> DeleteById(Guid roomId, [FromQuery] Guid propertyId)
        {
            await roomService.DeleteByIdAsync(roomId);

            return Ok(roomId);
        }
    }
}
