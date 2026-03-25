namespace Framework.Common.UI.Products.WestlawEdge.Items
{
    using System;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// RecentFolderDocumentItem on the foldering pop-up
    /// </summary>
    public class RecentFolderDocumentItem : BaseItem
    {
        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[@class = 'flags']/a");
        private static readonly By TitleLocator = By.XPath(".//div[@class = 'Folder-Item-title']/a");

        /// <inheritdoc />
        public RecentFolderDocumentItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// TitleLink
        /// </summary>
        public ILink TitleLink => new Link(this.Container, TitleLocator);

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public List<KeyCiteFlag> GetKeyCiteFlags()
        {
            var flagList = new List<KeyCiteFlag>();
            if (DriverExtensions.IsDisplayed(this.Container, KeyCiteFlagLocator))
            {
                List<string> flagClasses = DriverExtensions.GetElements(this.Container, KeyCiteFlagLocator).Select(flag => flag.GetAttribute("class")).ToList();

                flagClasses.ForEach(flagClass =>
                {
                    flagList.Add(KeyCiteFlagsMap.Single(
                        map => !string.IsNullOrEmpty(map.Value.ClassName)
                               && flagClass.Contains(map.Value.ClassName)).Key);
                });
            }

            return flagList;
        }

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(String.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/FoldersPopUp");
    }
}