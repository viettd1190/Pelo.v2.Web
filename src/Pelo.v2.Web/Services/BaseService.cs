using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Models;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services
{
    public class BaseService
    {
        protected IHttpService HttpService;

        protected ILogger<BaseService> _logger;

        public BaseService(IHttpService httpService,ILogger<BaseService> logger)
        {
            HttpService = httpService;
            _logger = logger;
        }

        protected async Task<TResponse<T>> Execute<T>(string url,
                                                      object data,
                                                      HttpMethod method,
                                                      bool authentication = false)
        {
            try
            {
                var response = await HttpService.Send<T>(url,
                                                         data,
                                                         method,
                                                         authentication);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<T>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<T>(exception);
            }
        }

        /// <summary>
        ///     Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Ok<T>(T data)
        {
            return Task.FromResult(new TResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = string.Empty
            });
        }

        /// <summary>
        ///     Fail
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Fail<T>(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return Task.FromResult(new TResponse<T>
            {
                Data = default(T),
                IsSuccess = false,
                Message = ex.ToString()
            });
        }

        /// <summary>
        ///     Fail
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Fail<T>(string message)
        {
            _logger.LogError(message);
            return Task.FromResult(new TResponse<T>
            {
                Data = default(T),
                IsSuccess = false,
                Message = message
            });
        }
    }
}
