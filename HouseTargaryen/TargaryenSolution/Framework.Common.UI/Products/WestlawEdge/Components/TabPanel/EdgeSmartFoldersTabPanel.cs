namespace Framework.Common.UI.Products.WestlawEdge.Components.TabPanel
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Components.TabPanel;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Tab panel component for smart folders page
    /// </summary>
    public class EdgeSmartFoldersTabPanel : SmartFoldersTabPanel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeSmartFoldersTabPanel"/> class. 
        /// </summary>
        public EdgeSmartFoldersTabPanel()
        {
            this.ActiveTab = new KeyValuePair<DocumentTypeTabs, BaseTabComponent>(
                DocumentTypeTabs.Cases,
                new EdgeCasesTabComponent());
            this.AllPossibleTabOptions = new Dictionary<DocumentTypeTabs, Type>
            {
                {
                    DocumentTypeTabs.Cases,
                    typeof(EdgeCasesTabComponent)
                },
                {
                    DocumentTypeTabs.SecondarySources,
                    typeof(EdgeSecondarySourcesTabComponent)
                },
                {
                    DocumentTypeTabs.StatutesAndCourtRules,
                    typeof(EdgeStatutesTabComponent)
                }
            };
        }


        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">tab option</param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns>ITAB object</returns>
        protected override TTab ClickTab<TTab>(DocumentTypeTabs tab)
        {
            Type searchedType;
            this.AllPossibleTabOptions.TryGetValue(tab, out searchedType);

            DriverExtensions.Click(By.XPath(this.TabsMap[tab].LocatorString));
            return (TTab)Activator.CreateInstance(searchedType);
        }
    }
}
