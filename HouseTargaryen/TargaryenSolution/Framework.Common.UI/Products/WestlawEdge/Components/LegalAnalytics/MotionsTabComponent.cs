using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.WestLawNext.Components;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    /// <summary>
    /// MotionsTab 
    /// </summary>
    public class MotionsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_motionReport_tab");
        private static readonly By SaveToFolderLocator = By.Id("co_saveToWidget");

        /// <summary>
        /// SaveToFolderButton
        /// </summary>
        public IButton SaveToFolderButton = new Button(SaveToFolderLocator);

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Motions";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
