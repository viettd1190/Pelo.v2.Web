using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pelo.v2.Web.TagHelpers
{
    /// <summary>
    ///     nop-label tag helper
    /// </summary>
    [HtmlTargetElement("nop-label",
            Attributes = ForAttributeName,
            TagStructure = TagStructure.WithoutEndTag)]
    public class NopLabelTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";

        private const string TextDisplay = "text-display";

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public NopLabelTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        ///     HtmlGenerator
        /// </summary>
        protected IHtmlGenerator Generator { get; set; }

        /// <summary>
        ///     An expression to be evaluated against the current model
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Text display in label
        /// </summary>
        [HtmlAttributeName(TextDisplay)]
        public string Text { get; set; }

        /// <summary>
        ///     ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        ///     Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context,
                                     TagHelperOutput output)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //generate label
            var tagBuilder = Generator.GenerateLabel(ViewContext,
                                                     For.ModelExplorer,
                                                     For.Name,
                                                     Text,
                                                     new
                                                     {
                                                             @class = "form-label semibold"
                                                     });
            if(tagBuilder != null)
            {
                //create a label wrapper
                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;
                //merge classes
                var classValue = output.Attributes.ContainsName("class")
                                         ? $"{output.Attributes["class"].Value} label-wrapper"
                                         : "label-wrapper";
                output.Attributes.SetAttribute("class",
                                               classValue);

                //add label
                output.Content.SetHtmlContent(tagBuilder);
            }
        }
    }
}
