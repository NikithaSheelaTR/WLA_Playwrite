namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Dockets Category Page
    /// </summary>
    public class AdvancedSearchDocketsPage : CommonBrowsePage
    {
        private static readonly By SelectBussinessNameLocator = By.XPath("//input[@value='BUSNAME']/..");
        private static readonly By SelectNameLocator = By.XPath("//input[@value='NAME']/..");
        private static readonly By SelectDocketNumberLocator = By.XPath("//input[@value='DOCKETNUM']/..");
        private static readonly By EnterBussinessNameLocator = By.XPath("//input[@id='co_search_advancedSearch_name']");
        private static readonly By EnterFirstNameLocator = By.XPath("//input[@id='co_search_advancedSearch_firstName']");
        private static readonly By EnterLastNameLocator = By.XPath("//input[@id='co_search_advancedSearch_lastName']");
        private static readonly By EnterDocketNumberLocator = By.XPath("//input[@id='co_search_advancedSearch_caseNumber']");
        private static readonly By SearchButtonLocator = By.XPath("//input[@id='co_search_advancedSearchButton_bottom']");
        private static readonly By ProgressRingLabelLocator = By.XPath("//div[@class='co_search_ajaxLoading']");
        private static readonly By AdditionalLoadingMessageLocator = By.XPath("//div[@class='co_search_additionalLoadingMessage co_hidden']");
        private static readonly By ResultItemsTitleLocator = By.XPath("//ol[@class='co_searchResult_list']/li//a");

        /// <summary>
        /// Select business name radio button
        /// </summary>
        public IRadiobutton SelectBussinessNameRadioButton => new Radiobutton(SelectBussinessNameLocator);

        /// <summary>
        /// Select name radio button
        /// </summary>
        public IRadiobutton SelectNameRadioButton => new Radiobutton(SelectNameLocator);

        /// <summary>
        /// Select docket number radio button
        /// </summary>
        public IRadiobutton SelectDocketNumberRadioButton => new Radiobutton(SelectDocketNumberLocator);

        /// <summary>
        /// Business name textbox
        /// </summary>
        public ITextbox EnterBussinessNameTextBox => new Textbox(EnterBussinessNameLocator);

        /// <summary>
        /// First name textbox
        /// </summary>
        public ITextbox EnterFirstNameTextBox => new Textbox(EnterFirstNameLocator);

        /// <summary>
        /// Last name textbox
        /// </summary>
        public ITextbox EnterLastNameTextBox => new Textbox(EnterLastNameLocator);

        /// <summary>
        /// Docket number textbox
        /// </summary>
        public ITextbox EnterDocketNumberTextBox => new Textbox(EnterDocketNumberLocator);

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);

        /// <summary>
        /// Progress ring label
        /// </summary>
        public ILabel ProgressRingLabel => new Label(ProgressRingLabelLocator);

        /// <summary>
        /// Additional loading message label
        /// </summary>
        public ILabel AdditionalLoadingMessageLabel => new Label(AdditionalLoadingMessageLocator);

        /// <summary>
        /// Result items title links
        /// </summary>
        public IReadOnlyCollection<ILink> ResultItemsTitleLinks => new ElementsCollection<Link>(ResultItemsTitleLocator);
    }
}