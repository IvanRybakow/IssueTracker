using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IssueTracker.WebUI.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        public SortState Property { get; set; }
        public SortState Current { get; set; }
        public string Filter { get; set; }
        public string Action { get; set; }
        public bool Up { get; set; }

        private readonly IUrlHelperFactory _urlHelperFactory;
        public SortHeaderTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";
            string url = Filter == null ? urlHelper.Action(Action, new { sortOrder = Property, isDesc = !Up }) :
                urlHelper.Action(Action, new { sortOrder = Property, isDesc = !Up, filter = Filter });
            output.Attributes.SetAttribute("href", url);
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                tag.AddCssClass(Up ? "glyphicon-chevron-up" : "glyphicon-chevron-down");

                output.PreContent.AppendHtml(tag);
            }
        }

    }
}
