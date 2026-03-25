namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Compare Text item
    /// </summary>
    public class CompareTextItem : RedlineComparisonItem
    {
        private static readonly By ItemInputTitleLocator = By.ClassName("co_comparisonItem_titleInput");
        private static readonly By ItemTitleLocator = By.XPath("//div[@class='co_redlineLightbox_tabItem_itemTitle']/label");
        private static readonly By PrimaryButtonLocator = By.XPath("//span[@class='co_redlineLightbox_tabItem_itemSelected']/span");

        /// <summary>
        /// Initializes a new instance of the <see cref="RedlineComparisonItem"/> class. 
        /// </summary>
        /// <param name="container"> Container </param>
        public CompareTextItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Get the text from input field
        /// </summary>
        /// <returns>Text from Input field</returns>
        public string GetItemTitleFromInputField() => DriverExtensions.GetText(ItemInputTitleLocator);

        /// <summary>
        /// Update the title of the item
        /// </summary>
        /// <param name="title">Title to be updated</param>
        public void UpdateTitle(string title)
        {
            DriverExtensions.WaitForElementDisplayed(ItemInputTitleLocator).Clear();
            DriverExtensions.WaitForElementDisplayed(ItemInputTitleLocator).SendKeys(title);
        }

        /// <summary>
        /// Get the title of the item
        /// </summary>
        public string GetItemTitle() => DriverExtensions.WaitForElementDisplayed(ItemTitleLocator).Text;

        /// <summary>
        /// Get the primary button
        /// </summary>
        public IButton PrimaryButton => new Button(PrimaryButtonLocator);
    }
}