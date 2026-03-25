namespace Framework.Common.UI.Products.Shared.Components.TabPanel
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Tab Panel class to operate with tabs within dialog
    /// </summary>
    /// <typeparam name="T">T </typeparam>
    public abstract class TabPanel<T>
        where T : struct
    {
        /// <summary>
        /// Active Tab
        /// </summary>
        protected KeyValuePair<T, BaseTabComponent> ActiveTab { get; set; }

        /// <summary>
        /// Tab Options
        /// </summary>
        protected Dictionary<T, Type> AllPossibleTabOptions { get; set; }

        /// <summary>
        /// Gets the BrowseTab enumeration to BaseTextModel map.
        /// </summary>
        protected virtual EnumPropertyMapper<T, WebElementInfo> TabsMap => EnumPropertyModelCache.GetMap<T, WebElementInfo>();

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <returns> True if tab is active </returns>
        public abstract bool IsActive(T tab);

        /// <summary>
        /// Is Tab Displayed
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <returns> True if tab is displayed </returns>
        public abstract bool IsDisplayed(T tab);

        /// <summary>
        /// Set Active Tab
        /// </summary>
        /// <param name="tab"> tab option </param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns> The <see cref="BaseTabComponent"/>.Tab component </returns>
        public virtual TTab SetActiveTab<TTab>(T tab) where TTab : BaseTabComponent
        {
            Type searchedType;

            if (!this.AllPossibleTabOptions.TryGetValue(tab, out searchedType))
            {
                throw new NotFoundException("Tab is not found");
            }

            if (!this.ActiveTab.Key.Equals(tab) || this.ActiveTab.Value == null)
            {
                this.ActiveTab = new KeyValuePair<T, BaseTabComponent>(tab, this.ClickTab<TTab>(tab));
            }

            return (TTab)this.ActiveTab.Value;
        }

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">tab option</param>
        /// <typeparam name="TTab">BaseTabComponent instance</typeparam>
        /// <returns>ITAB object</returns>
        protected virtual TTab ClickTab<TTab>(T tab)
            where TTab : BaseTabComponent
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.TabsMap[tab].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<TTab>();
        }
    }
}