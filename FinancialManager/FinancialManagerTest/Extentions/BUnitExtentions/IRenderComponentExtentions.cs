using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;

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
                    element.MarkupMatches($"<{cssSelector}>{markupElement}</{cssSelector}>");
                }
                catch
                {
                    return false;
                }
                return true;
            });
        }

        public static IElement GetElementBuyItsText<T>(this IRenderedComponent<T> componet, string cssSelector, string text) where T : IComponent
        {
            var selectedComponents = componet.FindAll(cssSelector);
            var requiredComponetn = selectedComponents.FirstOrDefault(element => element.HasMarkupElement(text));
            if (requiredComponetn is null)
            {
                throw new Exception($"{nameof(componet)} does not have any elements of <{cssSelector}> with text of {text}");
            }
            return requiredComponetn;
        }
    }
}
