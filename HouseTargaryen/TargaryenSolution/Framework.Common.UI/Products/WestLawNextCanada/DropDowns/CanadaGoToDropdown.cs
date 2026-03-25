namespace Framework.Common.UI.Products.WestLawNextCanada.DropDowns
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the Canada GoTo Dropdown
    /// </summary>
    public class CanadaGoToDropdown : GoToDropdown
    {
        private static readonly By ParagraphInputLocator = By.Id("crsw_paragraphNavInput");
        private static readonly By GoButtonLocator = By.Id("crsw_paragraphNavGo");

        /// <summary>
        /// Pragraph Input
        /// </summary>
        public ITextbox ParagraphInput => new Textbox(ParagraphInputLocator);

        /// <summary>
        /// Go button
        /// </summary>
        public IButton GoButton => new Button(GoButtonLocator);
    }
}
