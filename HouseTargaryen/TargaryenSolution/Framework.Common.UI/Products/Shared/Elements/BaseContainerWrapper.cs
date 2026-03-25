namespace Framework.Common.UI.Products.Shared.Elements
{
    using Framework.Common.UI.Constants;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The base container wrapper. Represents the core logic of working with inner container IWebElement. 
    /// </summary>
    public abstract class BaseContainerWrapper
    {
        /// <summary>
        /// The current container. Represents the wrapee IWebElement
        /// </summary>
        private readonly IWebElement currentContainer;

        private readonly IWebElement outerContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContainerWrapper"/> class.
        /// </summary>
        /// <param name="locatorBys">The locator bys.</param>
        protected BaseContainerWrapper(params By[] locatorBys)
        {
            this.CurrentElementLocator = new ByChained(locatorBys);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContainerWrapper"/> class.
        /// </summary>
        /// <param name="outerContainer">The outer container.</param>
        /// <param name="locatorBys">The locator bys.</param>
        protected BaseContainerWrapper(IWebElement outerContainer, params By[] locatorBys)
        {
            this.CurrentElementLocator = new ByChained(locatorBys);
            this.outerContainer = outerContainer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContainerWrapper"/> class.
        /// </summary>
        /// <param name="elementContainer">The already retrieved element container.</param>
        protected BaseContainerWrapper(IWebElement elementContainer)
        {
            this.currentContainer = elementContainer;
        }

        /// <summary>
        /// The current element locator.
        /// </summary>
        protected ByChained CurrentElementLocator { get; set; }

        /// <summary>
        /// The outer container. Used to create Container Elements with pointing to outer container if needed.
        /// </summary>
        private IWebElement OuterContainer => this.outerContainer ?? DriverExtensions.GetElement(WebDriverConstants.DefaultContainerElementBy);

        /// <summary>
        /// Gets the container. The logic of retrieving the wrapee IWebElement
        /// </summary>
        /// <param name="elementWaitTimeout">The element retrieval timeout (optional).
        /// </param>
        /// <returns>The <see cref="IWebElement"/> as an element container (wrapped).
        /// </returns>
        protected IWebElement GetContainer(int elementWaitTimeout = WebDriverTimeouts.ElementRetrieve)
        {
            if (this.currentContainer != null)
            {
                return this.currentContainer;
            }

            IWebElement container;

            // Retrieving the element using Safe Method Executor
            ExecutionResult executionResult = SafeMethodExecutor.Execute(
                () => DriverExtensions.WaitForElement(
                    this.OuterContainer,
                    this.CurrentElementLocator,
                    elementWaitTimeout),
                out container);

            // Logging error if element retrieval is not successful
            if (executionResult.ResultType != ExecutionResultType.Success)
            {
                Logger.LogError("Element retrieval failure");
                executionResult.LogDetails();
            }

            return container;
        }
    }
}