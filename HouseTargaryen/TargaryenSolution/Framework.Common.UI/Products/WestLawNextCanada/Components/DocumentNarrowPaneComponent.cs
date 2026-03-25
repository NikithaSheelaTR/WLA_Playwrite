namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Products.WestLawNextCanada.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using sun.reflect.generics.tree;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Document  Narrow Pane
    /// </summary>
    public class DocumentNarrowPaneComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocumentNarrowPaneContainerLocator = By.XPath("//*[@id='co_rightColumn']");

        private static readonly By RightPaneLocator = By.XPath(".//*[contains(@id,'crsw_rightPaneCaseViewsLink_')]");

        private static readonly By DocumentRightBoxLocator = By.XPath(".//*[@id='crsw_canadianAbridgment']/h3/span/strong");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DocumentNarrowPaneContainerLocator;

        /// <summary>
        ///Document Pane Label
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILabel DocumentPaneLabel => new Label(this.ComponentLocator,DocumentRightBoxLocator);

        /// <summary>
        /// Right Pane Links
        /// </summary>
        public IReadOnlyCollection<ILink> RightPaneLinks => new ElementsCollection<Link>(this.ComponentLocator, RightPaneLocator);
    }
}
