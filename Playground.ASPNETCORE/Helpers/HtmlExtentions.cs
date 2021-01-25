using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Playground.ASPNETCORE.Helpers
{
    public static class HtmlExtentions
    {
        public static IHtmlContent DisplayNameFor<TModel, TValue>(
            this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression,CultureInfo culture
        )
        {
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            var displayAttribute = (expression.Body as MemberExpression)?.Member.GetCustomAttributes()
                .FirstOrDefault(tt => tt is DisplayAttribute) as DisplayAttribute;
            if (displayAttribute == null)
            {
                return new HtmlString("");
            }
            var resourceType = displayAttribute.ResourceType;
            var name = displayAttribute.Name;
            var resourceManager = new global::System.Resources.ResourceManager(resourceType);
            var displayName = resourceManager.GetString(name, culture);
            return new HtmlString(displayName);
        }
    }
}
