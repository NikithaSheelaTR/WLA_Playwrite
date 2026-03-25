namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Tray item component
    /// </summary>
    public class QuickCheckTrayItem : BaseItem
    {
        private static readonly By StatusLocator = By.XPath(".//*[@class='co_deliveryQueue_itemStatus' or @class='DA-UploadError']");
        private static readonly By SubmittedDateLocator = By.XPath(".//span[@class='co_dividerDot'][2]");
        private static readonly By FileNameLocator = By.XPath(".//strong[not(contains(text(),'Submitted'))]");
        private static readonly By ShowDetailsButtonLocator = By.XPath(".//button[@class='co_linkBlue']");
        private static readonly By PauseMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckTrayItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuickCheckTrayItem(IWebElement container) : base(container)
        {
        }
        
        /// <summary>
        /// Get status of report
        /// </summary>
        /// <returns>report status</returns>
        public ILabel ReportStatusLabel => new Label(this.Container, StatusLocator);

        /// <summary>
        /// Gets the show detail button
        /// </summary>
        public IButton ShowDetailsButton => new Button(this.Container, ShowDetailsButtonLocator);

        /// <summary>
        /// DocumentName label
        /// </summary>
        public ILabel DocumentNameLabel => new Label(this.Container, FileNameLocator);

        /// <summary>
        /// PauseMessage label
        /// </summary>
        public ILabel PauseMessageLabel => new Label(this.Container, PauseMessageLocator);

        /// <summary>
        /// Get document date
        /// </summary>
        /// <returns>Document date</returns>
        public DateTime GetSubmittedDate() =>
            DateTime.Parse(DriverExtensions.GetElement(this.Container, SubmittedDateLocator).Text);

        /// <summary>
        /// Click on item
        /// </summary>
        /// <typeparam name="T">doc analyzer page</typeparam>
        /// <returns>Doc analyzer page</returns>
        public T Click<T>() where T : QuickCheckBasePage
        {
            this.Container.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}