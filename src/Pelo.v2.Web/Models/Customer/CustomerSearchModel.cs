namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerSearchModel : BaseSearchModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string ColumnOrder{ get; set; }
    }
}
