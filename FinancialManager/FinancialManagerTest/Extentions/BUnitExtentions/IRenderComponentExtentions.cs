using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerTest.Extentions.BUnitExtentions
{
    public static class IRenderComponentExtentions
    {
        public static bool HasMarkupElement<T>(this IRenderedComponent<T> component, string cssSelector, string markupElement) where T : IComponent
        {
            var selectedComponents = component.FindAll(cssSelector);
            return selectedComponents.Any(element =>
            {
                try
                {
                    element.HasMarkupElement(markupElement);
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }
    }
}
