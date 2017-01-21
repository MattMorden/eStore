using eStoreWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eStoreWebsite.Helpers
{
    public static class CatalogueHelper {
        public static HtmlString Catalogue(this HtmlHelper helper, string id)
        {
            // Create tag builder
            var builder = new TagBuilder("ul");
            StringBuilder innerHtml = new StringBuilder();

            // Create valid id
            builder.GenerateId(id);

            // Render tag
            if (HttpContext.Current.Session["cart"] != null) //haven't been to db yet
            {
                CartItemDTO[] cart = (CartItemDTO[])HttpContext.Current.Session["cart"];
                foreach (CartItemDTO item in cart)
                {
                    innerHtml.Append("<div class='col-lg-3 col-md-3 col-sm-3 col-sm-3 col-xs-12'><ul> <li style='list-style-type: none'><h4 id='Name" + item.ProductCode + "'>" + item.ProductName + "</h4>");
                    innerHtml.Append("<div><img class ='img' alt ='' src = '/Images/" + item.Graphic + "' id='Graphic" + item.ProductCode + "' width = '275' height ='275' style='padding-right:7%'/>");
                    innerHtml.Append("<div class='info'>");
                    innerHtml.Append("<p id='Descr" + item.ProductCode + "'data-description='" + item.Description + "'");
                    innerHtml.Append(item.Description.Substring(0, 20) + "...</p>");
                    innerHtml.Append("<div class='price'><span class='st'>Our price:</span>");
                    innerHtml.Append("<strong id='Price" + item.ProductCode + "'>" + "$" + string.Format("{0:0.00}", item.Msrp));
                    innerHtml.Append("</strong></div>");
                    innerHtml.Append("<a href='#details_popup' data-toggle='modal' class='btn btn-danger'");
                    innerHtml.Append("data-prodcd ='" + item.ProductCode + "'>Details</a><br/><br/></div></div></div>");
                }
            }
            builder.InnerHtml = innerHtml.ToString();
            return new HtmlString(builder.ToString());
        }
    }
}