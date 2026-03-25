namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    ///   Related Abridjment Copmonent
    /// </summary>
    public class RelatedAbridjmentComponent : BaseModuleRegressionComponent
    {
        private static readonly By RelatedAbridjmentContainer = By.XPath("//*[@id='crsw_abridgment']");

        private static readonly By ClassificationTextLocator = By.XPath(".//*[@id='crsw_abridgmentHeader']//span");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => RelatedAbridjmentContainer;

        /// <summary>
        /// Abridgment Classification Label
        /// </summary>
        public ILabel ClassificationLabel => new Label(this.ComponentLocator, ClassificationTextLocator);
    }
    
}
