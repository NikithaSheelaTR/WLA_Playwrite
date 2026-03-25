namespace Framework.Common.UI.Products.Shared.Items.CustomPageAdmin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    using sun.misc;

    /// <summary>
    /// Custom Page Contact With Pages Item
    /// </summary>
    public class CustomPageContactItem : BaseItem
    {
        /// <summary>
        /// String for RegExp to get numbers with quotes from string. E.g. string: 130 Jones (52), result from the string is (52)
        /// </summary>
        private const string Regexp = @"\(([1-9]*)\)";

        private static readonly By ContactNameLocator = By.XPath(".//button[@class='cp_sectionHeader co_genericBoxHeader']/span[not(contains(@class,'icon')) and not(contains(@class,'inactive'))]");
        private static readonly By CustomPageItemLocator = By.XPath(".//li[@class = 'cp_childPageList_item']");
        private static readonly By ContactLocator = By.XPath(".//button[@class='cp_sectionHeader co_genericBoxHeader']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPageContactItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        public CustomPageContactItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Contact Name
        /// </summary>
        public string ContactName =>
            Regex.Split(DriverExtensions.GetElement(this.Container, ContactNameLocator).GetText(), Regexp).First().Trim();

        /// <summary>
        /// Count of Custom pages where contact is Owner
        /// </summary>
        public int CustomPageCount =>
            DriverExtensions.GetElement(this.Container, ContactNameLocator).GetText().RetrieveCountFromBrackets();

        /// <summary>
        /// Is Contact Active
        /// </summary>
        public bool IsContactActive =>
            !DriverExtensions.GetElement(this.Container, ContactLocator).InnerHtml().Contains("Badge badge--gray");

        /// <summary>
        /// Is Contact Expanded
        /// </summary>
        public bool IsContactExpanded =>
            DriverExtensions.GetElement(this.Container, ContactLocator).GetAttribute("aria-expanded")
                             .Equals("true");

        /// <summary>
        /// Is Plus Button Displayed
        /// </summary>
        /// <returns></returns>
        public bool IsPlusButtonDisplayed() => DriverExtensions.GetElement(this.Container, ContactLocator).InnerHtml().Contains("icon_plus");

        /// <summary>
        /// Is minus Button Displayed
        /// </summary>
        /// <returns></returns>
        public bool IsMinusButtonDisplayed() => DriverExtensions.GetElement(this.Container, ContactLocator).InnerHtml().Contains("icon_minus");

        /// <summary>
        /// Custom Page List
        /// </summary>
        public List<CustomPageItem> CustomPageList => this.GetCustomPageList();

        /// <summary>
        /// Expand custom page contact Section if isn't expanded
        /// </summary>
        public void ExpandCustomPagesContactSectionIfNotExpanded()
        {
            if (!this.IsContactExpanded)
            {
                DriverExtensions.GetElement(this.Container, ContactLocator).CustomClick();
            }
        }

        private List<CustomPageItem> GetCustomPageList()
        {
            this.ExpandCustomPagesContactSectionIfNotExpanded();
          
            return DriverExtensions.GetElements(this.Container, CustomPageItemLocator).Select(page => new CustomPageItem(page))
                                   .ToList();
        }
    }
}
