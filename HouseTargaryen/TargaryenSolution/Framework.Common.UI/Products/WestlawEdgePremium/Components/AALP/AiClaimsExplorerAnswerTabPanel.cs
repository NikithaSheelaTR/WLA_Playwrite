namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP;

    /// <summary>
    /// Claims Explorer Tab Panel
    /// </summary>
    public class AiClaimsExplorerAnswerTabPanel : TabPanel<ClaimsExplorerAnswerTab>
    {
        private static readonly By CurrentActiveTabLocator = By.XPath(".//li[(@class = 'Tab Tab--active')] | .//*[@aria-selected='true']");

        private readonly IWebElement containerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="AiClaimsExplorerAnswerTabPanel"/> class. 
        /// </summary>
        public AiClaimsExplorerAnswerTabPanel(IWebElement containerElement)
        {
            this.containerElement = containerElement;
            this.AllPossibleTabOptions = new Dictionary<ClaimsExplorerAnswerTab, Type>
                                             {
                                                 {
                                                     ClaimsExplorerAnswerTab.Federal,
                                                     typeof(FederalTabComponent)
                                                 },
                                                 {
                                                     ClaimsExplorerAnswerTab.State,
                                                     typeof(StateTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Tab header label
        /// </summary>
        public ILabel TabHeaderLabel(ClaimsExplorerAnswerTab tab) => new Label(this.Container, By.XPath(this.TabsMap[tab].LocatorString));

        /// <summary>
        /// Browse Tab Tabs Mapper
        /// </summary>
        protected override EnumPropertyMapper<ClaimsExplorerAnswerTab, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<ClaimsExplorerAnswerTab, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/AALP");

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(ClaimsExplorerAnswerTab tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(this.Container, CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> tab </param>
        /// <returns> true if tab is displayed</returns>
        public override bool IsDisplayed(ClaimsExplorerAnswerTab tab) => DriverExtensions.IsDisplayed(this.Container, By.XPath(this.TabsMap[tab].LocatorString));

        /// <summary>
        /// Container
        /// </summary>
        protected IWebElement Container => this.containerElement;
    }
}
