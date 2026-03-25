namespace Framework.Common.UI.Products.Shared.Components.SignOff
{
    using System;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// History Event Component
    /// </summary>
    public class SignOffSessionDetailsItem
    {
        private static readonly By DescriptionLocator = By.XPath(".//td[2]");

        private static readonly By DateTimeViewedLocator = By.XPath(".//td[3]//span");

        /// <summary>
        /// Initializes a new instance of the <see cref="SignOffSessionDetailsItem"/> class. 
        /// </summary>
        /// <param name="container">The container.</param>
        public SignOffSessionDetailsItem(IWebElement container)
        {
            this.Container = container;
        }

        private IWebElement Container { get; }

        /// <summary>
        /// The get text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDescriptionText() => DriverExtensions.GetElement(this.Container, DescriptionLocator).Text;

        /// <summary>
        /// Gets Date time
        /// </summary>
        /// <returns>The <see cref="DateTime"/>.</returns>
        public DateTime GetDateTime() => DateTime.Parse(DriverExtensions.GetElement(this.Container, DateTimeViewedLocator).Text);
    }
}
