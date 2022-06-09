using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ComputerShopUI.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;
       
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagedListModel PageList{ get; set; }
        public string Href { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "nav";

            TagBuilder tag = new("ul");
            tag.AddCssClass("pagination");
            tag.AddCssClass("m-0");

            TagBuilder startItem = CreateTag(PageList.CurrentPage - 1, "&laquo;", urlHelper);
            
            if (!PageList.CanPrevious)
            {
                startItem.AddCssClass("disabled");
            }
            
            tag.InnerHtml.AppendHtml(startItem);

            int startPosition = PageList.CurrentPage - 2;
            int endPosition = PageList.CurrentPage + 2;

            if (startPosition <= 0)
            {
                startPosition = 1;
            }

            if(endPosition > PageList.TotalPages)
            {
                endPosition = PageList.TotalPages;
            }

            for (int i = startPosition; i <= endPosition; i++)
            {
                TagBuilder item = CreateTag(i, i.ToString(), urlHelper);
                
                if (i == PageList.CurrentPage)
                {
                    item.AddCssClass("active");
                }
                
                tag.InnerHtml.AppendHtml(item);
            }

            TagBuilder endItem = CreateTag(PageList.CurrentPage + 1, "&raquo;", urlHelper);
            
            if (!PageList.CanNext)
            {
                endItem.AddCssClass("disabled");
            }
           
            tag.InnerHtml.AppendHtml(endItem);

            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, string content, IUrlHelper urlHelper)
        {
            TagBuilder item = new("li");
            TagBuilder link = new("a");

            link.Attributes["href"] = $"Home?category={Href}&page={pageNumber}";

            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.AppendHtml(content);
            item.InnerHtml.AppendHtml(link);

            return item;
        }
    }
}
