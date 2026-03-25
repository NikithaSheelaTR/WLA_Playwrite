namespace Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The heading expand checkbox.
    /// </summary>
    public sealed class SectionExpandCheckbox : CheckBox
    {
        /// <inheritdoc />
        public SectionExpandCheckbox(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public override bool Selected => this.GetAttribute("aria-expanded").Equals("true");

        /// <inheritdoc />
        public override void Set(bool value)
        {
            if (this.Selected == value)
            {
                return;
            }

            this.GetContainer().JavascriptClick();
        }
    }
}