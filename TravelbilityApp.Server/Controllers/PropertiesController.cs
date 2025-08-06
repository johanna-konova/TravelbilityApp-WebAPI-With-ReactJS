using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Common;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Data.Models.Enums;
using TravelbilityApp.WebAPI.Filters;

using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Property;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class PropertiesController(
        IValidator<PropertyInputDto> validator,
        IPropertyService propertyService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet("newest")]
        [ProducesResponseType(typeof(IEnumerable<PropertyInNewestAddedDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewestAdded([FromQuery] int count)
        {
            var propertiesData = await propertyService.GetNewestAddedAsync(count);

            return Ok(propertiesData);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<PropertyInAllDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] DTOs.PropertyQueryParamsDto dto)
        {
            //throw new Exception();

            var serviceDto = new PropertyQueryParamsDto()
            {
                PropertyTypeIds = dto.PropertyTypeIds,
                RoomTypeIds = dto.RoomTypeIds,
                PropertyFacilityIds = dto.PropertyFacilityIds,
                RoomFacilityIds = dto.RoomFacilityIds,
                PropertyAccessibilityIds = dto.PropertyAccessibilityIds,
                RoomAccessibilityIds = dto.RoomAccessibilityIds,
                CurrentPageNumber = dto.CurrentPageNumber,
                PropertiesPerPage = dto.PropertiesPerPage,
            };

            var propertiesData = await propertyService.GetAllAsync(serviceDto);

            return Ok(propertiesData);
        }

        [HttpGet("listed")]
        [ProducesResponseType(typeof(PagedResultDto<UserPropertyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByUserId([FromQuery] int currenPageNumber = 1)
        {
            var propertiesData = await propertyService.GetAllByUserIdAsync(User.Id(), currenPageNumber);

            return Ok(propertiesData);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PropertyDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty(PropertyStatus.Published)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var propertyData = await propertyService.GetByIdAsync(id, PropertyStatus.Published);

            return Ok(propertyData);
        }

        [AllowAnonymous]
        [HttpGet("listed/{id:guid}")]
        [ProducesResponseType(typeof(UserPropertyDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var propertyData = await propertyService.GetByUserIdAsync(id, User.Id());

            return Ok(propertyData);
        }

        [HttpGet("{id:guid}/for-edit")]
        [ProducesResponseType(typeof(PropertyForEditDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty(PropertyStatus.Saved)]
        [PropertyPublisher]
        public async Task<IActionResult> GetForEditById(Guid id)
        {
            var propertyData = await propertyService.GetForEditByIdAsync(id, User.Id(), PropertyStatus.Saved);

            return Ok(propertyData);
        }

        [HttpGet("admin")]
        [ProducesResponseType(typeof(PagedResultDto<PropertForAdminShortDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Admin]
        public async Task<IActionResult> GetAllForAdmin([FromQuery] int currenPageNumber = 1)
        {
            var propertiesData = await propertyService.GetAllForAdminAsync(currenPageNumber);

            return Ok(propertiesData);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [NotAdmin]
        public async Task<IActionResult> Create([FromBody] PropertyInputDto dto)
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

            var createdPropertyId = await propertyService.CreateAsync(dto, User.Id());

            return Created(string.Empty, new { id = createdPropertyId });
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [PropertyPublisher]
        public async Task<IActionResult> Edit(Guid id, [FromBody] PropertyInputDto dto)
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
                ModelState.Any(e => e.Key != "id" && e.Key.StartsWith("ImageUrls[") == false))
            {
                return BadRequest(ModelState);
            }

            var editedPropertyId = await propertyService.EditAsync(id, dto);

            return Created(string.Empty, new { id = editedPropertyId });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [PropertyPublisher]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await propertyService.ChangePropertyStatus(id, PropertyStatus.Deleted);

            return Ok(id);
        }

        [HttpPut("{id:guid}/send-for-approval")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [PropertyPublisher]
        public async Task<IActionResult> SendForApprovalById(Guid id)
        {
            if (await propertyService.HasAccessibleRoom(id) == false)
            {
                return BadRequest(RequiredAtLeastAccessibleRoom);
            }

            await propertyService.ChangePropertyStatus(id, PropertyStatus.Pending);

            return Ok(id);
        }

        [HttpPut("{id:guid}/publish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [Admin]
        public async Task<IActionResult> PublishById(Guid id)
        {
            await propertyService.ChangePropertyStatus(id, PropertyStatus.Published);

            return Ok(id);
        }

        [HttpPut("{id:guid}/reject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExistingProperty]
        [Admin]
        public async Task<IActionResult> RejectById(Guid id)
        {
            await propertyService.ChangePropertyStatus(id, PropertyStatus.Rejected);

            return Ok(id);
        }
    }
}
