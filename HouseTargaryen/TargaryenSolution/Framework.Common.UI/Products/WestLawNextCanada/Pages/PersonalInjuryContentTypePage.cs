namespace Framework.Common.UI.Products.WestLawNextCanada.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// PersonalInjury ContentType Page
    /// </summary>
    public class PersonalInjuryContentTypePage : CommonDocumentPage
    {
        private static readonly By EasyEditLinkLocator = By.XPath(".//*[@id='co_docToolsWidgetEasyEditLink']");
        private static readonly By MinimizeButtonLocator = By.XPath(".//*[@id='coid_deliveryWaitMessage_minimizeButton']");

        /// <summary>
        /// PersonalInjuryContentType Component
        /// </summary>
        public PersonalInjuryContentTypeComponent PersonalInjuryContentType { get; } = new PersonalInjuryContentTypeComponent();

        /// <summary>
        /// EasyEdit Link
        /// </summary>
        public ILink EasyEditLink => new Link(EasyEditLinkLocator);

        /// <summary>
        /// MinimizeButton  
        /// </summary>
        public IButton MinimizeButton => new Button(MinimizeButtonLocator);
    }
}
