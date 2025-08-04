namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid Id(this ClaimsPrincipal user)
        {
            var idAsString = user.FindFirstValue("uid");

            return idAsString == null ? Guid.Empty : Guid.Parse(idAsString);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            var isAdminAsString = user.FindFirstValue("IsAdmin");

            return isAdminAsString != null && bool.Parse(isAdminAsString);
        }
    }
}
