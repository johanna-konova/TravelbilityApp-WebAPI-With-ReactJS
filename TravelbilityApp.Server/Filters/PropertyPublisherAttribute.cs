using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

using TravelbilityApp.Core.Contracts;

using static TravelbilityApp.WebAPI.Filters.CommonFunctionalities;

namespace TravelbilityApp.WebAPI.Filters
{
    public class PropertyPublisherAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var parsedPropertyId = ParseId(context, ["id", "propertyid", "dto"]);

            if (parsedPropertyId == Guid.Empty)
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

            var userId = context.HttpContext.User.Id();

            var isUserPropertyPublisher = await propertyService.IsUserPropertyPublisherAsync(parsedPropertyId, userId);

            if (isUserPropertyPublisher == false)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}