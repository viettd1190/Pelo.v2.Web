﻿namespace Pelo.v2.Web.Models
{
    public abstract class BaseSearchModel
    {
        #region Ctor

        public BaseSearchModel()
        {
            //set the default values
            Length = 10;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a page number
        /// </summary>
        public int Page => (Start / Length) + 1;

        /// <summary>
        ///     Gets a page size
        /// </summary>
        public int PageSize => Length;

        /// <summary>
        ///     Gets or sets a comma-separated list of available page sizes
        /// </summary>
        public string AvailablePageSizes { get; set; }

        /// <summary>
        ///     Gets or sets draw. Draw counter. This is used by DataTables to ensure that the Ajax returns from server-side
        ///     processing requests are drawn in sequence by DataTables (Ajax requests are asynchronous and thus can return out of
        ///     sequence).
        /// </summary>
        public string Draw { get; set; }

        /// <summary>
        ///     Gets or sets skiping number of rows count. Paging first record indicator.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        ///     Gets or sets paging length. Number of records that the table can display in the current draw.
        /// </summary>
        public int Length { get; set; }

        public ColumnRequestItem[] Columns { get; set; }

        public OrderRequestItem[] Order { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Set grid page parameters
        /// </summary>
        public void SetGridPageSize()
        {
            SetGridPageSize(20,
                            "10, 20, 50, 100");
        }

        /// <summary>
        ///     Set popup grid page parameters
        /// </summary>
        public void SetPopupGridPageSize()
        {
            SetGridPageSize(10,
                            "10, 20, 50, 100");
        }

        /// <summary>
        ///     Set grid page parameters
        /// </summary>
        /// <param name="pageSize">Page size; pass null to use default value</param>
        /// <param name="availablePageSizes">Available page sizes; pass null to use default value</param>
        public void SetGridPageSize(int pageSize,
                                    string availablePageSizes = null)
        {
            Start = 0;
            Length = pageSize;
            AvailablePageSizes = availablePageSizes;
        }

        #endregion
    }

    public class ColumnRequestItem
    {
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public SearchRequestItem Search { get; set; }
    }

    public class OrderRequestItem
    {
        public int Column { get; set; }

        public string Dir { get; set; }
    }

    public class SearchRequestItem
    {
        public string Value { get; set; }

        public bool Regex { get; set; }
    }
}
