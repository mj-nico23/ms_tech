using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ms_tech.Helpers
{
    public static class CustomHtmlHelepers
    {
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes)
        {
            return ImageActionLink(htmlHelper, linkText, action, controller, routeValues, htmlAttributes, "");
        }

        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes, string image)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.Attributes.Add("title", linkText);

            if (image != "")
                img.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/images/" + image));
            else
            {
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
                    case "nuevo":
                        img.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/images/add.png"));
                        break;
                    default:
                        break;
                }
            }

            var anchor = new TagBuilder("a") { InnerHtml = img.ToString(TagRenderMode.SelfClosing) };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(anchor.ToString());
        }

        public static IHtmlString IconButton(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes)
        {
            return IconButton(htmlHelper, linkText, action, controller, routeValues, htmlAttributes, "");
        }

        public static IHtmlString IconButton(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes, string glyphicono)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var span = new TagBuilder("span");
            span.SetInnerText(" " + linkText);

            var icon = new TagBuilder("i");

            if (glyphicono != "")
                icon.Attributes.Add("class", "glyphicon glyphicon-" + glyphicono);
            else
            {
                switch (linkText.ToLower().Substring(0, 4))
                {
                    case "edit":
                        icon.Attributes.Add("class", "glyphicon glyphicon-pencil");
                        break;
                    case "deta":
                        icon.Attributes.Add("class", "glyphicon glyphicon-open-file");
                        break;
                    case "elim":
                        icon.Attributes.Add("class", "glyphicon glyphicon-remove");
                        break;
                    case "nuev":
                        icon.Attributes.Add("class", "glyphicon glyphicon-plus");
                        break;
                    case "volv":
                        icon.Attributes.Add("class", "glyphicon glyphicon-arrow-left");
                        break;
                    default:
                        break;
                }
            }

            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append(icon.ToString(TagRenderMode.Normal));
            innerHtml.Append(span.ToString(TagRenderMode.Normal));

            TagBuilder parent = new TagBuilder("a");
            parent.InnerHtml = innerHtml.ToString();
            parent.Attributes.Add("class", "btn btn-default");
            parent.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            parent.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(parent.ToString());
        }

        public static IHtmlString BotonBuscar(this HtmlHelper htmlHelper)
        {
            var icon = new TagBuilder("i");
            icon.Attributes.Add("class", "glyphicon glyphicon-search");

            var span = new TagBuilder("span");
            span.SetInnerText(" " + "Buscar");

            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append(icon.ToString(TagRenderMode.Normal));
            innerHtml.Append(span.ToString(TagRenderMode.Normal));

            TagBuilder parent = new TagBuilder("button");
            parent.Attributes.Add("type", "submit");
            parent.InnerHtml = innerHtml.ToString();
            parent.Attributes.Add("class", "btn btn-default");

            return MvcHtmlString.Create(parent.ToString());
        }

        public static IHtmlString BotonGuardar(this HtmlHelper htmlHelper)
        {
            return BotonGuardar(htmlHelper, "Guardar");
        }

        public static IHtmlString BotonGuardar(this HtmlHelper htmlHelper, string texto)
        {
            var icon = new TagBuilder("i");
            icon.Attributes.Add("class", "glyphicon glyphicon-ok");

            var span = new TagBuilder("span");
            span.SetInnerText(" " + texto);

            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append(icon.ToString(TagRenderMode.Normal));
            innerHtml.Append(span.ToString(TagRenderMode.Normal));

            TagBuilder parent = new TagBuilder("button");
            parent.Attributes.Add("type", "submit");
            parent.InnerHtml = innerHtml.ToString();
            parent.Attributes.Add("class", "btn btn-default");

            return MvcHtmlString.Create(parent.ToString());
        }

        public static IHtmlString BotonEliminar(this HtmlHelper htmlHelper, string texto)
        {
            var icon = new TagBuilder("i");
            icon.Attributes.Add("class", "glyphicon glyphicon-remove");

            var span = new TagBuilder("span");
            span.SetInnerText(" " + texto);

            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append(icon.ToString(TagRenderMode.Normal));
            innerHtml.Append(span.ToString(TagRenderMode.Normal));

            TagBuilder parent = new TagBuilder("button");
            parent.Attributes.Add("type", "submit");
            parent.InnerHtml = innerHtml.ToString();
            parent.Attributes.Add("class", "btn btn-default");

            return MvcHtmlString.Create(parent.ToString());
        }

        public static IHtmlString BotonEliminar(this HtmlHelper htmlHelper)
        {
            return BotonEliminar(htmlHelper, "Eliminar");
        }
    }
}