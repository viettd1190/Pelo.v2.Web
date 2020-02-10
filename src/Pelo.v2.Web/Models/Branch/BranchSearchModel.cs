namespace Pelo.v2.Web.Models.Branch
{
    public class BranchSearchModel : BaseSearchModel
    {
        public string Name { get; set; }

        public string HotLine { get; set; }

        public int ProvinceId{ get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public string ColumnOrder{ get; set; }
    }
}
