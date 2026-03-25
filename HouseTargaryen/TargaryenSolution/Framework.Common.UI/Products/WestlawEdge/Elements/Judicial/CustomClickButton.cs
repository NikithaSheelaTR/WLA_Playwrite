namespace Framework.Common.UI.Products.WestlawEdge.Elements.Judicial
{
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// JudicialBackToMainUploadingPageButton
    /// </summary>
    public sealed class CustomClickButton : Button
    {
        /// <summary>
        /// JudicialCustomButton
        /// </summary>
        /// <param name="locatorBys">container locator</param>
        public CustomClickButton(params By[] locatorBys): base(locatorBys)
        {
        }

        /// <summary>
        /// JudicialCustomButton
        /// </summary>
        /// <param name="container"></param>
        public CustomClickButton(IWebElement container): base(container)
        {
        }

        /// <inheritdoc />
        public CustomClickButton(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Click
        /// </summary>
        public override void Click() => 
            this.GetContainer().CustomClick();

        /// <inheritdoc />
        public override bool Present => !this.GetAttribute("class").Contains("co_disabled");
    }
}
