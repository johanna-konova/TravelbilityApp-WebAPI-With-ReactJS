using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using TravelbilityApp.Core.Contracts;

namespace TravelbilityApp.WebAPI.Filters
{
    public class ExistingPropertyAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("id", out var rowId) == false ||
                rowId is not Guid propertyId)
            {
                context.Result = new BadRequestResult();
                return;
            }

            IPropertyService? propertyService =
                context.HttpContext.RequestServices.GetService<IPropertyService>();

            if (propertyService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            var hasPropertyWithGivenId = await propertyService.HasPropertyWithGivenIdAsync(propertyId);

            if (hasPropertyWithGivenId == false)
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }
    }
}