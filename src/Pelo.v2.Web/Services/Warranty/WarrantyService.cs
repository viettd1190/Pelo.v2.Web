using Microsoft.AspNetCore.Http;
using Pelo.Common.Models;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Models.Warranty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Services.Warranty
{
    public interface IWarrantyService
    {
        //Task<Warranty> GetByPaging(WarrantySearchModel request);

        //Task<TResponse<bool>> Insert(InsertWarranty model);

        //Task<TResponse<bool>> Update(UpdateCrmModel model);

        //Task<WarrantyListModel> GetByCustomerIdPaging(CustomerComponentSearchModel request);

        //Task<UpdateWarrantyModel> GetById(int id);

        //Task<IEnumerable<WarrantyLogResponse>> GetLogs(int id);

        Task<TResponse<bool>> Comment(WarrantyCommentModel model,
                                      List<IFormFile> files);
    }
}
