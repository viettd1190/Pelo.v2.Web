using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Pelo.v2.Web.Commons
{
    public class ContextHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private IActionContextAccessor _actionContextAccessor;

        public ContextHelper(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
        }

        public string GetToken()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return string.Empty;
            }

            return ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims.ToList()[4]
                                                                                   .Value;
        }

        public string GetController()
        {
            return _actionContextAccessor.ActionContext?.RouteData?.Values["controller"]?.ToString() ?? string.Empty;
        }

        public string GetAction()
        {
            return _actionContextAccessor.ActionContext?.RouteData?.Values["action"]?.ToString() ?? string.Empty;
        }
    }
}
