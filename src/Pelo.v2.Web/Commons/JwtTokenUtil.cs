using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Pelo.Common.Dtos.Account;

namespace Pelo.v2.Web.Commons
{
    public class JwtTokenUtil
    {
        public static int GetUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if(jwtToken != null)
                try
                {
                    return Convert.ToInt32(jwtToken.Claims.First(claim => claim.Type == "Id")
                                                   .Value);
                }
                catch (Exception exception)
                {
                    //
                }

            return 0;
        }

        public static LogonDetail GetLogonDetail(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if(jwtToken != null)
                try
                {
                    var isValid = true;

                    int id;
                    var username = string.Empty;
                    var displayName = string.Empty;
                    var avatar = string.Empty;

                    if(!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")
                                             ?.Value,
                                     out id))
                        isValid = false;

                    if(isValid)
                    {
                        username = jwtToken.Claims.FirstOrDefault(c => c.Type == "Username")
                                           ?.Value ?? string.Empty;
                        displayName = jwtToken.Claims.FirstOrDefault(c => c.Type == "DisplayName")
                                              ?.Value ?? string.Empty;
                        avatar = jwtToken.Claims.FirstOrDefault(c => c.Type == "Avatar")
                                         ?.Value ?? string.Empty;
                    }

                    if(isValid)
                        return new LogonDetail
                               {
                                       Id = id,
                                       Username = username,
                                       DisplayName = displayName,
                                       Avatar = avatar
                               };
                }
                catch (Exception exception)
                {
                    //
                }

            return null;
        }
    }
}
