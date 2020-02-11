using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Enums;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Account;
using Pelo.v2.Web.Services.Account;

namespace Pelo.v2.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult LogOn()
        {
            return View(new LogonModel());
        }

        [HttpPost]
        public async Task<IActionResult> LogOn(LogonModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountService.LogOn(model.Username,
                                                         model.Password);
                if(result.IsSuccess)
                {
                    var logonDetail = JwtTokenUtil.GetLogonDetail(result.Data.AccessToken);

                    if(logonDetail != null)
                    {
                        var claims = new List<Claim>
                                     {
                                             new Claim(ClaimTypes.NameIdentifier,
                                                       logonDetail.Id.ToString()),
                                             new Claim(ClaimTypes.Name,
                                                       logonDetail.DisplayName),
                                             new Claim(ClaimTypes.GivenName,
                                                       logonDetail.Username),
                                             new Claim("avatar",
                                                       logonDetail.Avatar),
                                             new Claim("Token",
                                                       result.Data.AccessToken)
                                     };
                        var identity = new ClaimsIdentity(claims,
                                                          "cookie");
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                      principal,
                                                      new AuthenticationProperties
                                                      {
                                                              IsPersistent = model.RememberMe,
                                                              ExpiresUtc = DateTime.UtcNow.AddDays(365)
                                                      });
                        if(string.IsNullOrEmpty(model.ReturnUrl))
                            return RedirectToAction("Index",
                                                    "Home");

                        return RedirectToAction(model.ReturnUrl);
                    }

                    ModelState.AddModelError("",
                                             ErrorEnum.AUTHENTICATION_WRONG.GetStringValue());
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Logon",
                                    "Account",
                                    new
                                    {
                                            returnUrl = "/"
                                    });
        }
    }
}
