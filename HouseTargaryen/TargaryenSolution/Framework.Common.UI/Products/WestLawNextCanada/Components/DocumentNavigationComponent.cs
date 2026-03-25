namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Document Navigation Component
    /// </summary>
    public class DocumentNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocumentNavigationContainerLocator = By.XPath("//*[@id='co_chunkNavOne']");

        private static readonly By NextPageIconLocator = By.XPath(".//*[@id='co_topNextChunkButton']");

        private static readonly By PreviousButtonLocator = By.XPath(".//*[@id='co_topPreviousChunkButton']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DocumentNavigationContainerLocator;

        /// <summary>
        /// Next Page Button
        /// </summary>
        public IButton NextPageButton => new Button(this.ComponentLocator, NextPageIconLocator);

        /// <summary>
        /// Previous Button 
        /// </summary>
        public IButton PreviousButton => new Button(this.ComponentLocator, PreviousButtonLocator);
    }
}
