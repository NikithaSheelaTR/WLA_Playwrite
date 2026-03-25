namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder
{
    using System;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Add to Outline tabs panel
    /// </summary>
    public class AddToOutlineTabPanel : TabPanel<AddToOutlineTabs>
    {
        private static readonly By ActiveTabLocator = By.XPath("//li[contains(@id,'tab_')][@aria-selected='true']");

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToOutlineTabPanel"/> class. 
        /// </summary>
        public AddToOutlineTabPanel()
        {
            this.ActiveTab = new KeyValuePair<AddToOutlineTabs, BaseTabComponent>(AddToOutlineTabs.Outline, new OutlineTabComponent());            
            this.AllPossibleTabOptions = new Dictionary<AddToOutlineTabs, Type>
                                             {
                                                {
                                                    AddToOutlineTabs.Outline,
                                                     typeof(OutlineTabComponent)
                                                 },
                                                 {
                                                     AddToOutlineTabs.AddNote,
                                                     typeof(AddNoteTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Tabs map
        /// </summary>
        protected override EnumPropertyMapper<AddToOutlineTabs, WebElementInfo> TabsMap
            => EnumPropertyModelCache.GetMap<AddToOutlineTabs, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/");

        /// <summary>
        /// Is Tab active
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsActive(AddToOutlineTabs tab)
            => DriverExtensions.WaitForElement(ActiveTabLocator).Text.Equals(this.TabsMap[tab].Text);

        /// <summary>
        /// Is Tab displayed
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsDisplayed(AddToOutlineTabs tab) => DriverExtensions.IsDisplayed(By.Id(this.TabsMap[tab].Id));
    }
}

