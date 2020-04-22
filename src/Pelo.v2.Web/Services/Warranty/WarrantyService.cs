using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pelo.Common.Dtos.Warranty;
using Pelo.Common.Exceptions;
using Pelo.Common.Extensions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Models.Warranty;
using Pelo.v2.Web.Services.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Services.Warranty
{
    public interface IWarrantyService
    {
        Task<TResponse<bool>> Update(UpdateWarrantyRequest model);

        Task<IEnumerable<WarrantyLogResponse>> GetLogs(int id);

        Task<TResponse<bool>> Comment(WarrantyCommentModel model,
                                      List<IFormFile> files);
        Task<WarrantyListModel> GetByPaging(WarrantySearchModel model);

        Task<TResponse<bool>> Insert(InsertWarrantyRequest model);

        Task<TResponse<GetWarrantyByIdResponse>> GetById(int id);

        Task<WarrantyListModel> GetByCustomerIdPaging(CustomerWarrantySearchModel request);
    }
    public class WarrantyService : BaseService,
                                  IWarrantyService
    {
        private readonly ContextHelper _contextHelper;

        public WarrantyService(IHttpService httpService,
                              ILogger<BaseService> logger,ContextHelper contextHelper) : base(httpService,
                                                                  logger)
        {
            _contextHelper = contextHelper;
        }

        public async Task<TResponse<bool>> Comment(WarrantyCommentModel model, List<IFormFile> files)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        foreach (var file in files)
                        {
                            var fileStream = file.OpenReadStream();
                            form.Add(CreateFileContent(fileStream,
                                                       file.FileName,
                                                       file.ContentType));
                        }

                        var paras = new List<KeyValuePair<string, string>>();
                        var para = new Tuple<int, string>(model.Id, model.Comment);
                        paras.Add(new KeyValuePair<string, string>("para", para.ToJson()));

                        form.Add(new FormUrlEncodedContent(paras));

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextHelper.GetToken());

                        var response = await client.PostAsync(ApiUrl.WARRANTY_COMMENT,
                                                              form);
                        response.EnsureSuccessStatusCode();

                        var res = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<TResponse<bool>>(res);
                        if (obj.IsSuccess)
                        {
                            return await Task.FromResult(new TResponse<bool>
                            {
                                Data = obj.Data,
                                IsSuccess = true,
                                Message = string.Empty
                            });
                        }

                        return await Task.FromResult(new TResponse<bool>
                        {
                            Data = default,
                            IsSuccess = false,
                            Message = obj.Message
                        });
                    }
                }
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }

        private StreamContent CreateFileContent(Stream stream,
                                                string fileName,
                                                string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            }; // the extra quotes are key here
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }

        public async Task<WarrantyListModel> GetByCustomerIdPaging(CustomerWarrantySearchModel request)
        {
            try
            {
                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.WARRANTY_GET_BY_ID,
                                            request.CustomerId,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetWarrantyPagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                    if (response.IsSuccess)
                        return new WarrantyListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new WarrantyModel
                            {
                                Id = c.Id,
                                Code = c.Code,
                                Branch = c.Branch,
                                WarrantyStatus = c.WarrantyStatus,
                                WarrantyStatusColor = c.WarrantyStatusColor,
                                UserCreatedPhone = c.UserCreatedPhone,
                                DateCreated = string.Format("{0:dd-mm-yyyy HH:mm}",c.DateCreated),
                                DeliveryDate = string.Format("{0:dd-mm-yyyy HH:mm}", c.DeliveryDate),
                                Products = c.Products.Select(v => new ProductInWarrantySimpleList
                                {
                                    Description = v.Description,
                                    Id = v.Id,
                                    Name = v.Name
                                })
                                                                                      .ToList(),
                                UserCreated = c.UserCreated,
                                PageSize = request.PageSize,
                                PageSizeOptions = request.AvailablePageSizes
                            })
                        };

                    throw new PeloException(response.Message);
                }

                throw new PeloException("Request is null");
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TResponse<GetWarrantyByIdResponse>> GetById(int id)
        {
            try
            {
                var response = await HttpService.Send<GetWarrantyByIdResponse>(string.Format(ApiUrl.WARRANTY_GET_BY_ID, id),
                                                                              null,
                                                                              HttpMethod.Get,
                                                                              true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetWarrantyByIdResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetWarrantyByIdResponse>(exception);
            }
        }

        public async Task<WarrantyListModel> GetByPaging(WarrantySearchModel request)
        {
            try
            {
                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.WARRANTY_GET_BY_PAGING,
                                            request.CustomerPhone,
                                            request.CustomerName,
                                            request.Code,
                                            request.WarrantyStatusId,
                                            request.UserCreatedId,
                                            request.UserCareId,
                                            request.FromDate,
                                            request.ToDate,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetWarrantyPagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                    if (response.IsSuccess)
                        return new WarrantyListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new WarrantyModel
                            {
                                Id = c.Id,
                                Code = c.Code,
                                WarrantyStatus = c.WarrantyStatus,
                                WarrantyStatusColor = c.WarrantyStatusColor,
                                CustomerName= c.CustomerName,
                                CustomerPhone1 = c.CustomerPhone1,
                                CustomerPhone2 = c.CustomerPhone2,
                                CustomerPhone3 = c.CustomerPhone3,
                                CustomerAddress = c.CustomerAddress,
                                UserCreatedPhone = c.UserCreatedPhone,
                                DeliveryDate = string.Format("{0:dd-mm-yyyy HH:mm}", c.DeliveryDate),
                                Products = c.Products.Select(v => new ProductInWarrantySimpleList
                                {
                                    Description = v.Description,
                                    Id = v.Id,
                                    Name = v.Name
                                })
                                                                                      .ToList(),
                                UserCreated = c.UserCreated,
                                PageSize = request.PageSize,
                                PageSizeOptions = request.AvailablePageSizes
                            })
                        };

                    throw new PeloException(response.Message);
                }

                throw new PeloException("Request is null");
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<IEnumerable<WarrantyLogResponse>> GetLogs(int id)
        {
            try
            {
                string url = string.Format(ApiUrl.WARRANTY_GETLOG,
                                           id);
                var response = await HttpService.Send<IEnumerable<WarrantyLogResponse>>(url,
                                                                                   null,
                                                                                   HttpMethod.Get,
                                                                                   true);
                if (response.IsSuccess)
                {
                    return response.Data;
                }

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TResponse<bool>> Insert(InsertWarrantyRequest model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARRANTY_INSERT,
                                                            model,
                                                            HttpMethod.Post,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(true);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateWarrantyRequest model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARRANTY_UPDATE,
                                                            model,
                                                            HttpMethod.Put,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(true);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }
    }
}
