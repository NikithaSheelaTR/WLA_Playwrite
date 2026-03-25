namespace Framework.Common.UI.Products.Shared.Elements.Buttons
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Outline Builder heading button
    /// </summary>
    public sealed class OutlineHeadingButton : Button
    {
        private const string HeadingButtonTitleLocator = ".//span[contains(@class, 'OutlineBuilder-heading')]";

        /// <summary>
        /// OutlineHeadingCustomButton
        /// </summary>
        /// <param name="locatorBys">container locator</param>
        public OutlineHeadingButton(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// OutlineHeadingCustomButton
        /// </summary>
        /// <param name="container"></param>
        public OutlineHeadingButton(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Button's heading level
        /// </summary>
        public string HeadingLevel => new List<string> { "1", "2", "3" }.First(item => this.GetContainer().GetAttribute("id").Contains(item));

        /// <summary>
        /// Button's title
        /// </summary>
        /// 
        public string Title => DriverExtensions.GetText(By.XPath(HeadingButtonTitleLocator), baseElement: this.GetContainer(), timeOut: 5);

        /// <summary>
        /// Click
        /// </summary>
        public override void Click() =>
            this.GetContainer().CustomClick();

        /// <inheritdoc />
        public override bool Present => !this.GetAttribute("class").Contains("co_disabled");
    }
}