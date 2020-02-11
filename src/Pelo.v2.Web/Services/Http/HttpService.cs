using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;

namespace Pelo.v2.Web.Services.Http
{
    public interface IHttpService
    {
        Task<TResponse<T>> Send<T>(string url,
                                   object content,
                                   HttpMethod method,
                                   bool authentication = false,
                                   string version = "", string contentType = "");
    }

    public class HttpService : IHttpService
    {
        private readonly ContextHelper _contextHelper;

        private ILogger<HttpService> _logger;

        public HttpService(ContextHelper contextHelper,ILogger<HttpService> logger)
        {
            _contextHelper = contextHelper;
            _logger = logger;
        }

        #region IHttpService Members

        public async Task<TResponse<T>> Send<T>(string url,
                                                object content,
                                                HttpMethod method,
                                                bool authentication = false,
                                                string version = "", string contentType = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string mediaType = "application/json";

                    var request = new HttpRequestMessage
                    {
                        Method = method,
                        RequestUri = new Uri(url),
                        Content = new StringContent(JsonConvert.SerializeObject(content),
                                                                      Encoding.UTF8,
                                                                      mediaType)
                    };

                    if (!string.IsNullOrEmpty(version))
                    {
                        request.Content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("v",
                                                                                                    version));
                    }

                    if (authentication)
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",
                                                                                      _contextHelper.GetToken());
                    }

                    request.Headers.Add("Controller", _contextHelper.GetController());
                    request.Headers.Add("Action", _contextHelper.GetAction());
                    if (!string.IsNullOrEmpty(contentType))
                    {
                        request.Headers.Add("Content-Type", contentType);
                    }
                    var response = await client.SendAsync(request);
                    _logger.LogInformation($"Request: {request.RequestUri.ToString()}");
                    _logger.LogInformation($"Response: {response.StatusCode}");

                    var res = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Res: {res}");
                    var obj = JsonConvert.DeserializeObject<TResponse<T>>(res);
                    if (obj != null)
                    {
                        if (obj.IsSuccess)
                        {
                            return await Task.FromResult(new TResponse<T>
                            {
                                Data = obj.Data,
                                IsSuccess = true,
                                Message = string.Empty
                            });
                        }

                        return await Task.FromResult(new TResponse<T>
                        {
                            Data = default(T),
                            IsSuccess = false,
                            Message = obj.Message
                        });
                    }

                    return await Task.FromResult(new TResponse<T>
                    {
                        Data = default(T),
                        IsSuccess = false,
                        Message = res
                    });
                }
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.ToString());
                return await Task.FromResult(new TResponse<T>
                {
                    Data = default(T),
                    IsSuccess = false,
                    Message = exception.ToString()
                });
            }
        }

        #endregion
    }
}
