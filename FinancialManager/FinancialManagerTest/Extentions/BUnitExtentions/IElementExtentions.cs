using AngleSharp.Dom;
using Bunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerTest.Extentions.BUnitExtentions
{
    public static class IElementExtentions
    {
        public static bool HasMarkupElement(this IElement element, string markup)
        {
            return element.ToMarkup().Contains(markup);
        }
    }
}
