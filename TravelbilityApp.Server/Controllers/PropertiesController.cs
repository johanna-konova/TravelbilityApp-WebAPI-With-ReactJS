using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.WebAPI.Filters;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class PropertiesController(
        IValidator<PropertyInputDto> validator,
        IPropertyService propertyService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyInAllDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] DTOs.Property.PropertyQueryParamsDto dto)
        {
            var serviceDto = new Core.DTOs.Property.PropertyQueryParamsDto()
            {
                PropertyTypeIds = dto.PropertyTypeIds,
                FacilityIds = dto.FacilityIds,
                AccessibilityIds = dto.AccessibilityIds,
            };

            var propertiesData = await propertyService.GetAllAsync(serviceDto);

            return Ok(propertiesData);
        }

        [AllowAnonymous]
        [HttpGet("newest")]
        [ProducesResponseType(typeof(IEnumerable<PropertyInNewestAddedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewestAdded([FromQuery] int count = 3)
        {
            var propertiesData = await propertyService.GetNewestAddedAsync(count);

            return Ok(propertiesData);
        }

        [HttpGet("listed")]
        [ProducesResponseType(typeof(IEnumerable<UserPropertyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserId()
        {
            var propertiesData = await propertyService.GetByUserIdAsync(User.Id());

            return Ok(propertiesData);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PropertyDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ExistingProperty]
        public async Task<IActionResult> GetById(Guid id)
        {
            var propertyData = await propertyService.GetByIdAsync(id);

            return Ok(propertyData);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    }
}
