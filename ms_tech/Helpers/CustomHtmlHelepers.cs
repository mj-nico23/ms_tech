using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ms_tech.Helpers
{
    public static class CustomHtmlHelepers
    {
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("title", linkText);

            switch (linkText.ToLower())
            {
                case "editar":
                    img.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/images/edit.png"));
                    break;
                case "detalles":
                    img.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/images/view.png"));
                    break;
                case "eliminar":
                    img.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/images/delete.png"));
                    break;
                default:
                    break;
            }

            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());

        }
    }
}