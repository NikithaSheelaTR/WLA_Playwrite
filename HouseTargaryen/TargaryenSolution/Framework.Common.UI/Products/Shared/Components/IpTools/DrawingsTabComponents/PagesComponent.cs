namespace Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items.IpDrawings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Pages component
    /// </summary>
    public class PagesComponent : BaseModuleRegressionComponent
    {
        private static readonly By PagesHeaderLabelLocator = By.XPath(".//h3[@class='IPDrawings-imagesHeader']");
        /// <inheritdoc />
        protected override By ComponentLocator => By.XPath("//div[@class='ipDrawingsResult']");

        /// <summary>
        /// Pages heading link
        /// </summary>
        public ILabel PagesHeadingLabel => new Label(this.ComponentLocator, PagesHeaderLabelLocator);

        /// <summary>
        /// Top navigation component
        /// </summary>
        public PagesTopNavigationComponent TopNavigationComponent => new PagesTopNavigationComponent();

        /// <summary>
        /// Bottom navigation component
        /// </summary>
        public PagesBottomNavigationComponent BottomNavigationComponent => new PagesBottomNavigationComponent();

        /// <summary>
        /// Pages grid component
        /// </summary>
        public PagesGridComponent PagesGridComponent => new PagesGridComponent();
    }
}