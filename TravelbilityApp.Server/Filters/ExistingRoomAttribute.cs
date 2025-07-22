using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using TravelbilityApp.Core.Contracts;

using static TravelbilityApp.WebAPI.Filters.CommonFunctionalities;

namespace TravelbilityApp.WebAPI.Filters
{
    public class ExistingRoomAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var parsedRoomId = ParseId(context, ["id", "roomid", "dto"]);

            if (parsedRoomId == Guid.Empty)
            {
                context.Result = new BadRequestResult();
                return;
            }

            IRoomService? roomService =
                context.HttpContext.RequestServices.GetService<IRoomService>();

            if (roomService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            var hasRoomWithGivenId = await roomService.HasRoomWithGivenIdAsync(parsedRoomId);

            if (hasRoomWithGivenId == false)
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }
    }
}