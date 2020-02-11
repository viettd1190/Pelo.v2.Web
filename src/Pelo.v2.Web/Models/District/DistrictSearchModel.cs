namespace Pelo.v2.Web.Models.District
{
    public class DistrictSearchModel : BaseSearchModel
    {
        public int ProvinceId { get; set; }

        public string DistrictName { get; set; }
    }
}
