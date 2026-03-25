namespace Framework.Common.UI.Products.Shared.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Chunk Navigation Component
    /// </summary>
    public class ChunkNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By PreviousChunkButtonLocator = By.ClassName("co_prev");
        private static readonly By NextChunkButtonLocator = By.ClassName("co_next");
        private static readonly By ChunkCountStatusLocator = By.XPath("//strong[contains(@id, 'CountStatus')]");
        private static readonly By CurrentChunkNumberLocator = By.XPath("//input[contains(@id, 'Current')]");
        private static readonly By ErrorMessageLocator = By.ClassName("co_infoBox_message");
        private static readonly By ContainerLocator = By.ClassName("co_chunkPagination");
        private EnumPropertyMapper<ChunkNavigationContainers, WebElementInfo> chunkNavigationContainersMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// ChunkNavigationContainersMap
        /// </summary>
        protected EnumPropertyMapper<ChunkNavigationContainers, WebElementInfo> ChunkNavigationContainersMap
            =>
                this.chunkNavigationContainersMap =
                this.chunkNavigationContainersMap ?? EnumPropertyModelCache.GetMap<ChunkNavigationContainers, WebElementInfo>();

        /// <summary>
        /// IsErrorMessageDisplayed
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsErrorMessageDisplayed(ChunkNavigationContainers container) => DriverExtensions.IsDisplayed(ErrorMessageLocator);

        /// <summary>
        /// GetCountOfChunks
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns> The <see cref="int"/>. </returns>
        public int GetCountOfChunks(ChunkNavigationContainers container) => int.Parse(this.GetCountStatusText(container));

        /// <summary>
        /// ClickNextChunkButton
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <returns> Page object </returns>
        public T ClickNextChunkButton<T>(ChunkNavigationContainers container) where T : ICreatablePageObject
        {
            DriverExtensions.Click(
                DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                NextChunkButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// ClickPreviousChunkButton
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <returns> Page object </returns>
        public virtual T ClickPreviousChunkButton<T>(ChunkNavigationContainers container) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                 DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                 PreviousChunkButtonLocator).Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if the Next Chunk Arrow is Enabled
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns>Whether or not the NextChunkArrow is enabled</returns>
        public bool IsNextChunkButtonEnabled(ChunkNavigationContainers container)
            =>
                !DriverExtensions.GetAttribute(
                    "class",
                    DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                    NextChunkButtonLocator).Contains("disabled");

        /// <summary>
        /// Checks if the Previous Chunk Arrow is Enabled
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns>Whether or not the PreviousChunkArrow is enabled</returns>
        public bool IsPreviousChunkButtonEnabled(ChunkNavigationContainers container)
            =>
                !DriverExtensions.GetAttribute(
                    "class",
                    DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                    PreviousChunkButtonLocator).Contains("disabled");

        /// <summary>
        /// Checks if the Next Chunk Arrow is Displayed
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns>Whether or not the NextChunkArrow is displayed</returns>
        public bool IsNextChunkButtonDisplayed(ChunkNavigationContainers container)
            =>
                DriverExtensions.WaitForElement(
                    DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                    NextChunkButtonLocator).Displayed;

        /// <summary>
        /// Checks if the Previous Chunk Arrow is Displayed
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns>Whether or not the PreviousChunkArrow is displayed</returns>
        public bool IsPreviousChunkButtonDisplayed(ChunkNavigationContainers container)
            =>
                DriverExtensions.WaitForElement(
                    DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                    PreviousChunkButtonLocator).Displayed;

        /// <summary>
        /// ChangeChunkField
        /// </summary>
        /// <param name="container"> The container. </param> 
        /// <param name="chunkNumber"> The chunk Number. </param>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <returns> Page object </returns>
        public T ChangeChunkField<T>(ChunkNavigationContainers container, string chunkNumber) where T : ICreatablePageObject
        {
            IWebElement chunkElement = this.GetChunkFieldElement(container);

            // ClearUsingButtons() should be replaced by Clear() method when Clear() will be fixed
            chunkElement.ClearUsingButtons();
            DriverExtensions.WaitForJavaScript();
            chunkElement.SendKeys(chunkNumber);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.PressKey(Keys.Enter);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks that CountStatus id is present and not still loading.
        /// </summary>
        /// <returns>Boolean whether or not the element is displayed correctly</returns>
        public bool IsChunkCountStatusPresent()
        {
            // => this.GetCountStatusText(ChunkNavigationContainers.TopChunkContainer) != "...";
            if (this.GetCountStatusText(ChunkNavigationContainers.TopChunkContainer) != "...")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// NavigateBackwardChunkPages
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <param name="count"> The count. </param>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <returns> Page object </returns>
        public T NavigateBackwardChunkPages<T>(ChunkNavigationContainers container, int count) where T : ICreatablePageObject
        {
            for (int i = 0; i < count; i++)
            {
                this.ClickPreviousChunkButton<T>(container);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// NavigateForwardChunkPages
        /// </summary>       
        /// <param name="container"> The container. </param>
        /// <param name="count"> The count. </param>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <returns> Page object </returns>
        public T NavigateForwardChunkPages<T>(ChunkNavigationContainers container, int count) where T : ICreatablePageObject
        {
            this.WaitForChunkComponentIsLoaded(container);
            for (int i = 0; i < count; i++)
            {
                this.ClickNextChunkButton<T>(container);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigate to frist page.
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns>Page object </returns>
        public T NavigateToFirstPage<T>(ChunkNavigationContainers container) where T : ICreatablePageObject
        {
            while (this.IsPreviousChunkButtonEnabled(container))
            {
                this.ClickPreviousChunkButton<T>(container);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// WaitForComponentIsLoaded
        /// </summary>
        /// <param name="container"> The container. </param>
        public void WaitForChunkComponentIsLoaded(ChunkNavigationContainers container)
            => DriverExtensions.WaitForElement(
                DriverExtensions.WaitForElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)), NextChunkButtonLocator);

        /// <summary>
        /// GetChunkFieldElement
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns> The <see cref="IWebElement"/>. </returns>
        private IWebElement GetChunkFieldElement(ChunkNavigationContainers container)
            =>
                DriverExtensions.WaitForElement(
                    DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                    CurrentChunkNumberLocator);

        /// <summary>
        /// GetCountStatusText
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <returns> The <see cref="string"/>. </returns>
        private string GetCountStatusText(ChunkNavigationContainers container)
            =>
                DriverExtensions.WaitForElement(
                    DriverExtensions.GetElement(By.XPath(this.ChunkNavigationContainersMap[container].LocatorString)),
                    ChunkCountStatusLocator).Text;
    }
}