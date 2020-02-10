using System;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Models;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services
{
    public class BaseService
    {
        protected IHttpService HttpService;

        public BaseService(IHttpService httpService)
        {
            HttpService = httpService;
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
            return Task.FromResult(new TResponse<T>
            {
                Data = default(T),
                IsSuccess = false,
                Message = message
            });
        }
    }
}
