using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebApi.Core.User
{
    public interface IAspNetUser
    {
        bool IsAuthenticated();
        HttpContext GetContext();
    }
}
