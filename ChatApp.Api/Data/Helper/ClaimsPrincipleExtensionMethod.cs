using System.Security.Claims;

namespace ChatApp.Api.Data.Helper
{
    public static  class ClaimsPrincipleExtensionMethod
    {
        public static string  GetCurrentUserEmail(this ClaimsPrincipal user)
        {
            return  user.FindFirst(ClaimTypes.Email).Value;
        }
        public static string GetCurrentUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name).Value;
        }
        public static string GetCurrentUserById(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }       

    }
}
