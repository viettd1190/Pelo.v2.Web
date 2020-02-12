using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Account;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Account
{
    public interface IAccountService
    {
        Task<TResponse<LogonResponse>> LogOn(string username,
                                             string password);
    }

    public class AccountService : BaseService,
                                  IAccountService
    {
        public AccountService(IHttpService httpService,
                              ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IAccountService Members

        public async Task<TResponse<LogonResponse>> LogOn(string username,
                                                          string password)
        {
            try
            {
                return await Execute<LogonResponse>(ApiUrl.LOG_ON,
                                                    new
                                                    {
                                                            username,
                                                            password
                                                    },
                                                    HttpMethod.Post);
            }
            catch (Exception exception)
            {
                return await Fail<LogonResponse>(exception);
            }
        }

        #endregion
    }
}
