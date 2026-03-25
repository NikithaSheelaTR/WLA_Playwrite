namespace Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck
{
    using System;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using ikvm.extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Negative treatment link.
    /// </summary>
    public sealed class NegativeTreatmentLink : Link
    {
        /// <inheritdoc />
        public NegativeTreatmentLink(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Is link enabled
        /// </summary>
        /// <inheritdoc />
        public override bool Enabled
        {
            get
            {
                try
                {
                    return !this.GetAttribute("href").isEmpty();

                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }
    }
}
