using System.Collections.Generic;
using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Dialogs;
using Framework.Common.UI.Products.Shared.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Elements.Links;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs
{
    /// <summary>
    /// Advantage Notifications Dialog
    /// </summary>
    public class AdvantageFoldersDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");
        private static readonly By OpenFoldersLocator = By.XPath(".//saf-anchor");
        private static readonly By FolderingPageTitleHeaderLocator = By.XPath("//div[@id='co_folderTitleWrapper']/h1");
        private static readonly By RootFolderLinkLocator = By.XPath("//button[@type='button' ]/span[contains(text(), 'Research')]");
        private static readonly By ViewThisFolderButtonLocator = By.XPath("//saf-button[@type='button']");
        private static readonly By UserResearchContentSearchResultsLocator = By.XPath("//div[@class='Folder-Item-title']/a");

        /// <summary>
        /// Open Folders Link
        /// </summary>
        public ILink OpenFoldersLink => new Link((IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector(\"a.control[href*='Folders']\"));",
            DriverExtensions.GetElement(ContentTypeContainerLocator, OpenFoldersLocator)));

        /// <summary>
        /// Click Open Folders Link
        /// </summary>
        public void ClickOpenFoldersLink()
        {
            var openFoldersLink = (IWebElement)DriverExtensions.ExecuteScript(
                       "return(arguments[0].shadowRoot.querySelector(\"a.control[href*='Folders']\"));",
                        DriverExtensions.GetElement(ContentTypeContainerLocator, OpenFoldersLocator));

            openFoldersLink.JavascriptClick();
        }

        /// <summary>
        /// Folder Page Title Header
        /// </summary>
        public ILabel FoldersPageTitleHeader = new Label(FolderingPageTitleHeaderLocator);

        /// <summary>
        /// Click Root Folder Link
        /// </summary>
        public ILink RootFolderLink => new Link(RootFolderLinkLocator);

        /// <summary>
        /// View this folder button
        /// </summary>
        public IButton ViewThisFolderButton => new Button(ViewThisFolderButtonLocator);

        /// <summary>
        /// Get the User Research Content Search Results section
        /// </summary>
        public IReadOnlyCollection<IButton> UserResearchContentSearchResults => new ElementsCollection<Button>(ContentTypeContainerLocator, UserResearchContentSearchResultsLocator);
    }
}
