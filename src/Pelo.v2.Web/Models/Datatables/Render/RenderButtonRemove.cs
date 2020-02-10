﻿namespace Pelo.v2.Web.Models.Datatables.Render
{
    /// <summary>
    /// Represents button render for DataTables column
    /// </summary>
    public partial class RenderButtonRemove : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderButton class 
        /// </summary>
        /// <param name="title">Button title</param>
        public RenderButtonRemove(string title)
        {
            Title = title;
            ClassName = NopButtonClassDefaults.Danger;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets button title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets button class name
        /// </summary>
        public string ClassName { get; set; }

        #endregion
    }
}