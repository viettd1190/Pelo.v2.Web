namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerSearchModel : BaseSearchModel
    {
        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        public int? ProvinceId { get; set; } = 0;

        public int? DistrictId { get; set; } = 0;

        public int? WardId { get; set; } = 0;

        public string Address { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";

        public int CustomerGroupId { get; set; } = 0;

        public int CustomerVipId { get; set; } = 0;

    }
}
