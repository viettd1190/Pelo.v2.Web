using System;

namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerDetailModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Ward { get; set; }

        public string CustomerGroup { get; set; }

        public string CustomerVip { get; set; }

        public int Profit { get; set; }

        public int ProfitUpdate { get; set; }

        public string UserCare { get; set; }

        public string UserCarePhone { get; set; }

        public string UserFirst { get; set; }

        public string UserFirstPhone { get; set; }

        public string Description { get; set; }

        public string UserCreated { get; set; }

        public string UserCreatedPhone { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
