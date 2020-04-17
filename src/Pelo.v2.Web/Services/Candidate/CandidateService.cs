using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pelo.Common.Dtos.Candidate;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Candidate;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Candidate
{
    public interface ICandidateService
    {
        Task<CandidateListModel> GetByPaging(CandidateSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Insert(InsertCandidate insertCandidate);
        
        Task<TResponse<GetCandidateResponse>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateCandidate updateCandidate);
        Task<IEnumerable<CandidateLogResponse>> GetLogs(int id);

        Task<TResponse<bool>> Comment(CandidateComment model,
                                      List<IFormFile> files);
    }

    public class CandidateService : BaseService,
                                    ICandidateService
    {
        private readonly ContextHelper _contextHelper;

        public CandidateService(IHttpService httpService,
                                ILogger<BaseService> logger, ContextHelper contextHelper) : base(httpService, logger)
        {
            _contextHelper = contextHelper;
        }

        #region ICandidateService Members

        public async Task<CandidateListModel> GetByPaging(CandidateSearchModel request)
        {
            try
            {
                var columnOrder = request.ColumnOrder ?? "Name";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;
                    string fromDate = string.Empty; string toDate = string.Empty;
                    if (!string.IsNullOrEmpty(request.FromDate))
                    {
                        CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                        fromDate = string.Format("{0:yyyy-MM-dd} 00:00:00", DateTime.Parse(request.FromDate, MyCultureInfo));
                    }
                    if (!string.IsNullOrEmpty(request.ToDate))
                    {
                        CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                        toDate = string.Format("{0:yyyy-MM-dd} 00:00:00", DateTime.Parse(request.ToDate, MyCultureInfo));
                    }
                    var url = string.Format(ApiUrl.CANDIDATE_GET_BY_PAGING,
                                            request.Name,
                                            fromDate,
                                            toDate,
                                            request.Phone,
                                            request.Code,
                                            request.CandidateStatusId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCandidatePagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new CandidateListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new CandidateModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
                                Code = c.Code,
                                Phone = c.Phone,
                                CandidateStatusName = c.CandidateStatusName,
                                Description = c.Description,
                                UserNameCreated = c.UserNameCreated,
                                UserPhoneCreated = c.UserPhoneCreated,
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

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CANDIDATE_DELETE,
                                        id);
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Delete,
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

        public async Task<TResponse<bool>> Insert(InsertCandidate insertCandidate)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CANDIDATE_UPDATE,
                                                            insertCandidate,
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

        public async Task<TResponse<GetCandidateResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CANDIDATE_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetCandidateResponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCandidateResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCandidateResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCandidate updateCandidate)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CANDIDATE_UPDATE,
                                                            updateCandidate,
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

        #endregion

        public async Task<IEnumerable<CandidateLogResponse>> GetLogs(int id)
        {
            try
            {
                string url = string.Format(ApiUrl.CANDIDATE_GET_LOGS,
                                           id);
                var response = await HttpService.Send<IEnumerable<CandidateLogResponse>>(url,
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

        public async Task<TResponse<bool>> Comment(CandidateComment model,
                                                   List<IFormFile> files)
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

                        var response = await client.PostAsync(ApiUrl.CANDIDATE_COMMENT,
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
    }
}
