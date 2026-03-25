namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Cost Recovery Caps Header Component
    /// </summary>
    public class CostRecoveryCapsTabPanel : TabPanel<CostRecoveryCapsTabs>
    {
        private static readonly By CurrentActiveTabLocator =
            By.XPath("//ul[@class='wa_pricingNavigation wa_tabs buttonTabs']/li[@class='active']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CostRecoveryCapsTabPanel"/> class. 
        /// </summary>
        public CostRecoveryCapsTabPanel()
        {
            this.ActiveTab = new KeyValuePair<CostRecoveryCapsTabs, BaseTabComponent>(CostRecoveryCapsTabs.CreateSessionCap, new CreateSessionCapTabComponent());
            this.AllPossibleTabOptions = new Dictionary<CostRecoveryCapsTabs, Type>
            {
                {
                    CostRecoveryCapsTabs.CreateSessionCap,
                    typeof(CreateSessionCapTabComponent)
                },
                {
                    CostRecoveryCapsTabs.CreateMonthlyCap,
                    typeof(CreateMonthlyCapTabComponent)
                },
                {
                    CostRecoveryCapsTabs.ActiveCaps,
                    typeof(ManageCapsTabComponent)
                },
                {
                    CostRecoveryCapsTabs.InactiveCaps,
                    typeof(ViewInactiveTabComponent)
                }
            };
        }

        /// <summary>
        /// Verify that tab is active
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is active, false otherwise </returns>
        public override bool IsActive(CostRecoveryCapsTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTabLocator).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Verify that tab is displayed
        /// </summary>
        /// <param name="tab"> Tab to verify </param>
        /// <returns> True if tab is displayed, false otherwise </returns>
        public override bool IsDisplayed(CostRecoveryCapsTabs tab)
            => DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString), 5);
    }
}
