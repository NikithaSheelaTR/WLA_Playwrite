namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Quick access item
    /// </summary>
    public class QuickAccessItem : BaseItem
    {
        private static readonly By TitleLocator = By.XPath("//*[contains(@class,'__QuickAccessTitle--')]");

        private static readonly By CitationLocator = By.XPath(".//div[contains(@class,'QuickAccessCitationLine')]");

        private static readonly By KeyCiteIconLocator = By.XPath(".//div[@class='QuickAccess-iconColumn']/a");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="QuickAccessItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public QuickAccessItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Title label
        /// </summary>
        public ILink TitleLink => new Link(this.Container, TitleLocator);

        /// <summary>
        /// Citation label
        /// </summary>
        public ILabel CitationLabel => new Label(this.Container, CitationLocator);

        /// <summary>
        /// Actions menu dropdown
        /// </summary>
        public ActionsMenuDropdown ActionsMenu => new ActionsMenuDropdown(this.Container);

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteIconLocator))
                {
                    string flagClass = DriverExtensions.GetAttribute("class", this.Container, KeyCiteIconLocator);
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                        model => model.ClassName,
                        string.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdge/Folders");
                }

                return KeyCiteFlag.NoFlag;
            }
        }
    }
}
    
