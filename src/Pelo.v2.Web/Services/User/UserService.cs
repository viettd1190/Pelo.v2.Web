using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.User;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.User;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDisplaySimpleModel>> GetAll();

        Task<UserListModel> GetByPaging(UserSearchModel request);

        //Task<TResponse<bool>> Insert(UserInsert request);

        //Task<TResponse<UserUpdate>> GetById(int id);

        //Task<TResponse<bool>> Delete(int id);
    }

    public class UserService : BaseService,
                               IUserService
    {
        public UserService(IHttpService httpService,
                           ILogger<BaseService> logger) : base(httpService,
                                                               logger)
        {
        }

        #region IUserService Members

        public async Task<IEnumerable<UserDisplaySimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<UserDisplaySimpleModel>>(ApiUrl.USER_GET_ALL,
                                                                                 null,
                                                                                 HttpMethod.Get,
                                                                                 true);

                if (response.IsSuccess)
                    return response.Data;

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<UserListModel> GetByPaging(UserSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    if(request.Columns != null
                       && request.Columns.Any()
                       && request.Order != null
                       && request.Order.Any())
                    {
                        sortDir = request.Order[0]
                                         .Dir;
                        columnOrder = request.Columns[request.Order[0]
                                                             .Column]
                                             .Data;
                    }

                    var url = string.Format(ApiUrl.USER_GET_BY_PAGING,
                                            request.Code,
                                            request.FullName,
                                            request.PhoneNumber,
                                            request.BranchId,
                                            request.DepartmentId,
                                            request.RoleId,
                                            request.Status,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetUserPagingResponse>>(url,
                                                                                             null,
                                                                                             HttpMethod.Get,
                                                                                             true);

                    if(response.IsSuccess)
                        return new UserListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new UserModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Code = c.Code,
                                                                                     FullName = c.FullName,
                                                                                     Branch = c.Branch,
                                                                                     Department = c.Department,
                                                                                     Role = c.Role,
                                                                                     Username = c.Username,
                                                                                     PhoneNumber = c.PhoneNumber,
                                                                                     DateCreated = c.DateCreated,
                                                                                     IsActive = c.IsActive,
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

        #endregion

        //public async Task<TResponse<bool>> Insert(UserInsert request)
        //{
        //    try
        //    {
        //        var url = ApiUrl.USER_INSERT;
        //        var response = await HttpService.Send<bool>(url,
        //                                                    request,
        //                                                    HttpMethod.Post,
        //                                                    true);
        //        if(response.IsSuccess)
        //        {
        //            return await Ok(true);
        //        }

        //        return await Fail<bool>(response.Message);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await Fail<bool>(exception);
        //    }
        //}

        //public async Task<TResponse<UserUpdate>> GetById(int id)
        //{
        //    try
        //    {
        //        var url = string.Format(ApiUrl.USER_GET_BY_ID,
        //                                id);
        //        var response = await HttpService.Send<UserUpdate>(url,
        //                                                               null,
        //                                                               HttpMethod.Get,
        //                                                               true);
        //        if(response.IsSuccess)
        //        {
        //            return await Ok(response.Data);
        //        }

        //        return await Fail<UserUpdate>(response.Message);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await Fail<UserUpdate>(exception);
        //    }
        //}

        //public async Task<TResponse<bool>> Delete(int id)
        //{
        //    try
        //    {
        //        var url = string.Format(ApiUrl.USER_DELETE,
        //                                id);
        //        var response = await HttpService.Send<bool>(url,
        //                                                    null,
        //                                                    HttpMethod.Delete,
        //                                                    true);
        //        if(response.IsSuccess)
        //        {
        //            return await Ok(true);
        //        }

        //        return await Fail<bool>(response.Message);
        //    }
        //    catch (Exception exception)
        //    {
        //        return await Fail<bool>(exception);
        //    }
        //}
    }
}
