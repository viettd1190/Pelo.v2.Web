using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.UI;

namespace Pelo.v2.Web.Commons
{
    public static class LayoutExtensions
    {
        /// <summary>
        /// Specify system name of admin menu item that should be selected (expanded)
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="systemName">System name</param>
        public static void SetActiveMenuItemSystemName(this IHtmlHelper html, string systemName)
        {
            var pageHeadBuilder = EngineContext.Current.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.SetActiveMenuItemSystemName(systemName);
        }
    }
}
