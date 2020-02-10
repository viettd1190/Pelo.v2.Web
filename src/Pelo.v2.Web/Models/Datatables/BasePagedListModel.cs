using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Datatables
{
    public abstract class BasePagedListModel<T>
    {
        /// <summary>
        ///     Gets or sets data records
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        ///     Gets or sets draw
        /// </summary>
        [JsonProperty("draw")]
        public string Draw { get; set; }

        /// <summary>
        ///     Gets or sets a number of filtered data records
        /// </summary>
        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        /// <summary>
        ///     Gets or sets a number of total data records
        /// </summary>
        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
