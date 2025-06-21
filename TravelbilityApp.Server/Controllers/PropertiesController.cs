using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class PropertiesController(
        IValidator<CreatePropertyDto> validator,
        IPropertyService propertyService) : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
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
        public async Task<IActionResult> GetById(Guid id)
        {
            var propertyData = await propertyService.GetByIdAsync(id);

            if (propertyData == null)
            {
                return NotFound();
            }

            return Ok(propertyData);
        }

        [HttpGet("listed")]
        [ProducesResponseType(typeof(IEnumerable<UserPropertyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserId()
        {
            var propertiesData = await propertyService.GetByUserIdAsync(User.Id());

            return Ok(propertiesData);
        }
    }
}
