using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace Pelo.v2.Web.Models.Datatables
{
    public class DataTablesModel
    {
        #region Const

        protected const string DEFAULT_PAGING_TYPE = "simple_numbers";

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DataTablesModel()
        {
            //set default values
            Info = true;
            RefreshButton = true;
            ServerSide = true;
            Processing = true;
            Paging = true;
            PagingType = DEFAULT_PAGING_TYPE;

            Filters = new List<FilterParameter>();
            ColumnCollection = new List<ColumnProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets table name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets URL for data read (ajax)
        /// </summary>
        public DataUrl UrlRead { get; set; }

        /// <summary>
        /// Gets or sets URL for delete action (ajax)
        /// </summary>
        public DataUrl UrlDelete { get; set; }

        /// <summary>
        /// Gets or sets URL for update action (ajax)
        /// </summary>
        public DataUrl UrlUpdate { get; set; }

        /// <summary>
        /// Gets or sets search button Id
        /// </summary>
        public string SearchButtonId { get; set; }

        /// <summary>
        /// Gets or set filters controls
        /// </summary>
        public IList<FilterParameter> Filters { get; set; }

        /// <summary>
        /// Gets or sets data for table (ajax, json, array)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Enable or disable the display of a 'processing' indicator when the table is being processed 
        /// </summary>
        public bool Processing { get; set; }

        /// <summary>
        /// Feature control DataTables' server-side processing mode
        /// </summary>
        public bool ServerSide { get; set; }

        /// <summary>
        /// Enable or disable table pagination.
        /// </summary>
        public bool Paging { get; set; }

        /// <summary>
        /// Enable or disable information ("1 to n of n entries")
        /// </summary>
        public bool Info { get; set; }

        /// <summary>
        /// Enable or disable refresh button
        /// </summary>
        public bool RefreshButton { get; set; }

        /// <summary>
        /// Pagination button display options.
        /// </summary>
        public string PagingType { get; set; }

        /// <summary>
        /// Number of rows to display on a single page when using pagination
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// This parameter allows you to readily specify the entries in the length drop down select list that DataTables shows when pagination is enabled
        /// </summary>
        public string LengthMenu { get; set; }

        /// <summary>
        /// Indicates where particular features appears in the DOM
        /// </summary>
        public string Dom { get; set; }

        /// <summary>
        /// Feature control ordering (sorting) abilities in DataTables
        /// </summary>
        public bool Ordering { get; set; }

        /// <summary>
        /// Gets or sets custom render header function name(js)
        /// See also https://datatables.net/reference/option/headerCallback
        /// </summary>
        public string HeaderCallback { get; set; }

        /// <summary>
        /// Gets or sets a number of columns to generate in a footer. Set 0 to disable footer
        /// </summary>
        public int FooterColumns { get; set; }

        /// <summary>
        /// Gets or sets custom render footer function name(js)
        /// See also https://datatables.net/reference/option/footerCallback
        /// </summary>
        public string FooterCallback { get; set; }

        /// <summary>
        /// Gets or sets indicate of child table
        /// </summary>
        public bool IsChildTable { get; set; }

        /// <summary>
        /// Gets or sets child table
        /// </summary>
        public DataTablesModel ChildTable { get; set; }

        /// <summary>
        /// Gets or sets primary key column name for parent table
        /// </summary>
        public string PrimaryKeyColumn { get; set; }

        /// <summary>
        /// Gets or sets bind column name for delete action. If this field is not specified, the default will be the alias "id" for the delete action
        /// </summary>
        public string BindColumnNameActionDelete { get; set; }

        /// <summary>
        /// Gets or set column collection 
        /// </summary>
        public IList<ColumnProperty> ColumnCollection { get; set; }

        #endregion
    }

    public class FilterParameter
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the FilterParameter class by default as string type parameter
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        public FilterParameter(string name)
        {
            Name = name;
            Type = typeof(string);
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="modelName">Filter parameter model name</param>
        public FilterParameter(string name, string modelName)
        {
            Name = name;
            ModelName = modelName;
            Type = typeof(string);
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="type">Filter parameter type</param>
        public FilterParameter(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="value">Filter parameter value</param>
        public FilterParameter(string name, object value)
        {
            Name = name;
            Type = value.GetType();
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class for linking "parent-child" tables
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="parentName">Filter parameter parent name</param>
        /// <param name="isParentChildParameter">Parameter indicator for linking "parent-child" tables</param>
        public FilterParameter(string name, string parentName, bool isParentChildParameter = true)
        {
            Name = name;
            ParentName = parentName;
            Type = typeof(string);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Filter field name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Filter model name
        /// </summary>
        public string ModelName { get; }

        /// <summary>
        /// Filter field type
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Filter field value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Filter field parent name
        /// </summary>
        public string ParentName { get; set; }

        #endregion
    }

    public partial class ColumnProperty
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the ColumnProperty class
        /// </summary>
        /// <param name="data">The data source for the column from the rows data object</param>
        public ColumnProperty(string data)
        {
            Data = data;
            //set default values
            Visible = true;
            Encode = true;
            Orderable = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Set the data source for the column from the rows data object / array.
        /// See also "https://datatables.net/reference/option/columns.data"
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Set the column title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Render (process) the data for use in the table. This property will modify the data that is used by DataTables for various operations.
        /// </summary>
        public IRender Render { get; set; }

        /// <summary>
        /// Column width assignment. This parameter can be used to define the width of a column, and may take any CSS value (3em, 20px etc).
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// Column autowidth assignment. This can be disabled as an optimisation (it takes a finite amount of time to calculate the widths) if the tables widths are passed in using "width".
        /// </summary>
        public bool AutoWidth { get; set; }

        /// <summary>
        /// Indicate whether the column is master check box
        /// </summary>
        public bool IsMasterCheckBox { get; set; }

        /// <summary>
        /// Class to assign to each cell in the column.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Enable or disable the display of this column.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Enable or disable filtering on the data in this column.
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Enable or disable ordering on the data in this column
        /// </summary>
        public bool Orderable { get; set; }

        /// <summary>
        /// Enable or disable editing on the data in this column.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// Data column type
        /// </summary>
        public EditType EditType { get; set; }

        /// <summary>
        /// Enable or disable encode on the data in this column.
        /// </summary>
        public bool Encode { get; set; }

        #endregion
    }

    public enum EditType
    {
        Number = 1,
        Checkbox = 2,
        String = 3
    }

    public interface IRender
    {
    }

    public class DataUrl
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the DataUrl class
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public DataUrl(string actionName,
                       string controllerName,
                       RouteValueDictionary routeValues)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
        }

        /// <summary>
        ///     Initializes a new instance of the DataUrl class
        /// </summary>
        /// <param name="url">URL</param>
        public DataUrl(string url)
        {
            Url = url;
        }

        /// <summary>
        ///     Initializes a new instance of the DataUrl class
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="dataId">Name of the column whose value is to be used as identifier in URL</param>
        public DataUrl(string url,
                       string dataId)
        {
            Url = url;
            DataId = dataId;
        }

        /// <summary>
        ///     Initializes a new instance of the DataUrl class
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="trimEnd">
        ///     Parameter indicating that you need to delete all occurrences of the character "/" at the end of
        ///     the line
        /// </param>
        public DataUrl(string url,
                       bool trimEnd)
        {
            Url = url;
            TrimEnd = trimEnd;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the name of the action.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the controller.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets the route values.
        /// </summary>
        public RouteValueDictionary RouteValues { get; set; }

        /// <summary>
        ///     Gets or sets data Id
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        ///     Gets or sets parameter indicating that you need to delete all occurrences of the character "/" at the end of the
        ///     line
        /// </summary>
        public bool TrimEnd { get; set; }

        #endregion
    }
}
