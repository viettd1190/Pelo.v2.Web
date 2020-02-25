﻿using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Commons
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>Result</returns>
        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using (var writer = new StringWriter())
            {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = writer.ToString();
                return htmlOutput;
            }
        }
    }
}
