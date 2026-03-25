namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Toolbar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Trillium Favourites Dialog
    /// </summary>
    public class TrilliumDialog : BaseModuleRegressionDialog
    {      
        private static readonly By MyFavouritesCheckBoxLocator = By.XPath(".//div[@class='co_listItem']//input");
        private static readonly By SaveButtonLocator = By.XPath(".//button[text()='Save']");     

        /// <summary>
        /// MyFavourites Checkbox
        /// </summary>
        public ICheckBox MyFavouritesCheckBox => new CheckBox(MyFavouritesCheckBoxLocator);

        /// <summary>
        /// Save Button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);
    }
}
