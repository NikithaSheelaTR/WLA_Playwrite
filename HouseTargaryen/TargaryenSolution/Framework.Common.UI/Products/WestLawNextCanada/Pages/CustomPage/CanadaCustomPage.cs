namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.CustomPage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Custom Page
    /// </summary>
    public class CanadaCustomPage : CustomPage
    {
        private static readonly By NavigateButtonLocator = By.XPath("//*[@class='a11yDropdown-button']");
        private static readonly By PageListCorrectOptionLinkLocator = By.XPath("//*[@class='a11yDropdown-item cp_page_name']//a");
        private static readonly By DeleteButtonLocator = By.XPath("//*[@id='cp_toolSectionToolbar']//*[contains(text(),'Delete')]");
        private static readonly By EditPageDeleteButtonLocator = By.XPath("//*[@class='co_inlineList']//button");
        private static readonly By SaveChangesButtonLocator = By.XPath("//*[@id='cp_saveChanges_button']");
        private static readonly By EditNameLinkLocator = By.XPath("//*[@id='cp_renamePage']");
        private static readonly By SaveCustomPageAlertsLocator = By.XPath("//*[@id='co_CustomPages_SaveAlerts']");

        /// <summary>
        /// CustomPages component
        /// </summary>
        public CustomPagesComponent CustomPagesComponent { get; } = new CustomPagesComponent();

        /// <summary>
        /// Favorites Widget on the right hand side
        /// </summary>
        public FavoritesComponent FavoritesComponent { get; } = new FavoritesComponent();

        /// <summary>
        /// Folders Widget on the right hand side
        /// </summary>
        public FoldersComponent FoldersComponent { get; } = new FoldersComponent();

        /// <summary>
        /// MyAlerts Widget on the right hand side
        /// </summary>
        public MyAlertsComponent MyAlertsComponent { get; } = new MyAlertsComponent();

        /// <summary>
        /// NavigateButton
        /// </summary>
        public IButton NavigateButton => new Button(NavigateButtonLocator);

        /// <summary>
        /// DeleteButton
        /// </summary>
        public IButton DeleteButton => new Button(DeleteButtonLocator);

        /// <summary>
        ///  SaveChangesButton
        /// </summary>
        public IButton SaveChangesButton => new Button(SaveChangesButtonLocator);

        /// <summary>
        ///  SaveCustomPageAlertsButton
        /// </summary>
        public IButton SaveCustomPageAlertsButton => new Button(SaveCustomPageAlertsLocator);

        /// <summary>
        /// EditPageDeleteButtons
        /// </summary>
        public IReadOnlyCollection<IButton> EditPageDeleteButtons => new ElementsCollection<Button>(EditPageDeleteButtonLocator);

        /// <summary>
        /// EditNameLink
        /// </summary>
        public IButton EditNameLink => new Button(EditNameLinkLocator);

        /// <summary>
        /// PageListCorrectOptionLinks
        /// </summary>
        public IReadOnlyCollection<ILink> PageListCorrectOptionLinks => new ElementsCollection<Link>(PageListCorrectOptionLinkLocator);

        /// <summary>
        /// ReorderWiget
        /// </summary>
        public bool ReorderWiget()
        {            
          DriverExtensions.DragAndDrop(FoldersComponent.FoldersWidget, MyAlertsComponent.MyAlertsWidget);
          return FoldersComponent.FoldersWidget.Location.Y > FavoritesComponent.FavouriteWidget.Location.Y;
        }

        /// <summary>
        /// List of Deletebuttons
        /// </summary>        
        public void ClickAllDeleteButtons()
        {
            var deleteButtons = EditPageDeleteButtons.ToList();

            while (deleteButtons.Any())
            {
                deleteButtons.First().Click();

                // Refresh the list of buttons after the click action
                deleteButtons = EditPageDeleteButtons.ToList();
                DriverExtensions.WaitForJavaScript();
            }
        }
    }
}