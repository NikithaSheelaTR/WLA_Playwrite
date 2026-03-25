namespace Framework.Common.UI.Products.WestlawEdge.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Edge Negative Treatment Component on the Edge Research Organizer page
    /// </summary>
    public class EdgeNegativeTreatmentComponent : BaseModuleRegressionComponent
    {
        private const string DocByPositionLctMask = "//li[{0}]//div[@class='FolderingNegativeTreatmentContent']/a";
        private const string FlagByPositionLctMask = "//li[{0}]//div[@class='FolderingNegativeTreatmentIcons']/a";

        private static readonly By ContainerLocator = By.XPath("//*[contains(@id,'coid_folderAnalysisKeyCite')]");
        private static readonly By FolderAnalysisKeyCiteBoxLocator = By.ClassName("FolderingNegativeTreatment");
        private static readonly By FolderAnalysisKeyCiteDocumentLocator = By.XPath("//*[@id='coid_folderAnalysisKeyCiteContainer']//div[@class='FolderingNegativeTreatmentContent']");
        private static readonly By ShowMoreLocator = By.XPath("//button[text()='Show more negative treatment']");
        private static readonly By ShowLessLocator = By.XPath("//button[text()='Show less negative treatment']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on a specific document based on the documents position
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="position"> Document position (top position is 1)</param>
        /// <returns> New instance of the page </returns>
        public T ClickOnDocumentByPosition<T>(int position) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(DocByPositionLctMask, position))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on a specific document's KeyCite flag based on the documents position
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="position"> Document position (top position is 1)</param>
        /// <returns> New instance of the page </returns>
        public T ClickOnFlagByPosition<T>(int position) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(FlagByPositionLctMask, position))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if the Folder Analysis KeyCite Box is displayed
        /// </summary>
        /// <returns>The display status of the Folder Analysis KeyCite box</returns>
        public bool IsFolderAnalysisKeyCiteBoxDisplayed() => DriverExtensions.IsDisplayed(FolderAnalysisKeyCiteBoxLocator, 5);

        /// <summary>
        /// Checks if the Folder Analysis KeyCite container is present
        /// </summary>
        /// <returns>The display status of the Folder Analysis KeyCite box</returns>
        public bool IsFolderAnalysisKeyCiteContainerPresent() => DriverExtensions.IsElementPresent(this.ComponentLocator);

        /// <summary>
        /// Counts the number of items that are displayed in the Folder Analysis KeyCite box
        /// </summary>
        /// <returns>The number of docs in the folder Analysis KeyCite box</returns>
        public int GetFolderAnalysisKeyCiteDocumentsCount() => DriverExtensions.GetElements(FolderAnalysisKeyCiteDocumentLocator).Count;

        /// <summary>
        ///Click Show more Negative treatment link  
        /// </summary>
        public IButton ShowMoreButton { get; } = new Button(ShowMoreLocator);

        ///<summary>
        ///Click ShowLess Negative treatment Link 
        /// </summary>
        public IButton ShowLessButton { get; } = new Button(ShowLessLocator);
    }
}