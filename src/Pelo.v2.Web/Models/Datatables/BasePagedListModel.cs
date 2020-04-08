using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Datatables
{
    public abstract class BasePagedListModel<T>
    {
        public IEnumerable<T> Data { get; set; }

        public string Draw { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }

        public int Total { get; set; }
    }
}
