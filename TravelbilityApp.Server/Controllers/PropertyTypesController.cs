using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class PropertyTypesController(IPropertyTypeService propertyTypeService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(PropertyTypeOptionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var propertyTypes = await propertyTypeService.GetAllAsync();

            return Ok(propertyTypes);
        }
    }
}
