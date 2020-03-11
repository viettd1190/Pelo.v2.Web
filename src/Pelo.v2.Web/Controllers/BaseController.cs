using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Pelo.v2.Web.Controllers
{
    public class BaseController : Controller
    {
        protected int GetUserId()
        {
            try
            {
                if(User.Identity.IsAuthenticated)
                {
                    var identities = (ClaimsIdentity) User.Identity;
                    if(identities != null)
                    {
                        var claim = identities.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        if(claim != null)
                        {
                            return Convert.ToInt32(claim.Value);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //
            }

            return 0;
        }
    }
}
