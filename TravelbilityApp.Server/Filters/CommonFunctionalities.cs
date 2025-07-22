using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TravelbilityApp.WebAPI.Filters
{
    public static class CommonFunctionalities
    {
        public static Guid ParseId(ActionExecutingContext context, HashSet<string> possibleIdNames)
        {
            var parsedId = Guid.Empty;
            var idName = context.ActionArguments.Keys
                .FirstOrDefault(k => possibleIdNames.Contains(k.ToLower()));

            if (idName == null)
            {
                return Guid.Empty;
            }

            var idValue = context.ActionArguments[idName];

            if (idValue == null)
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(idValue.ToString(), out parsedId))
            {
                return parsedId;
            }

            var propertyInfo = idValue.GetType().GetProperties()
                    .FirstOrDefault(p => possibleIdNames.Contains(p.Name.ToLower()));

            var propertyInfoValue = propertyInfo?.GetValue(idValue);

            if (propertyInfoValue is Guid id)
            {
                return id;
            }

            if (propertyInfoValue is string idAsString &&
                Guid.TryParse(idAsString, out parsedId))
            {
                return parsedId;
            }

            return Guid.Empty;
        }
    }
}
