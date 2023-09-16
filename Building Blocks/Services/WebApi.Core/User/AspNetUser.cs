using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace WebApi.Core.User
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AspNetUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public HttpContext GetContext()
        {
            return _contextAccessor.HttpContext;
        }
 
        public bool IsAuthenticated()
        {
            var context = GetContext();

            return context.User.Identity.IsAuthenticated;
        }
    }
}
