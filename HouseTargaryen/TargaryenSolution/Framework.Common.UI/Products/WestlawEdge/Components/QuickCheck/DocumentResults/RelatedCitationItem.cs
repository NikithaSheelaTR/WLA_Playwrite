namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document analyzer=>=>Recommendations page=>Recommendations tab=>Results pane to the right.
    /// Gets the citations(links with flags under the "This recommendation relates to cases already cited in your document:" section)
    /// </summary>
    public sealed class RelatedCitationItem : BaseItem
    {
        private static readonly By KeyCiteFlagLocator = By.XPath(".//a[contains(@oldtitle,'KeyCite')]");

        private static readonly By LinkLocator = By.XPath(".//a[contains(@href,'Document')]");

        private static readonly By ImpliedOverrulingLocator = By.XPath(".//a[contains(@oldtitle,'KeyCite Overruling')]");

        private static readonly By LexisCiteLocator = By.XPath("./button");

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedCitationItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public RelatedCitationItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The title link.
        /// </summary>
        public ILink TitleLink => new Link(this.Container, LinkLocator);

        /// <summary>
        /// Lexis label
        /// </summary>
        public ILabel LexisLabel => new Label(this.Container, LexisCiteLocator);

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(this.Container, KeyCiteFlagLocator))
                {
                    string flagClass = DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator).GetAttribute("class");
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(mod => mod.ClassName);
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// The Implied Overruling present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsImpliedOverrulingPresent => DriverExtensions.GetElements(this.Container, ImpliedOverrulingLocator).Any();

        /// <summary>
        /// The click key cite flag.
        /// </summary>
        /// <typeparam name="TPage">
        /// the type of page
        /// </typeparam>
        /// <returns>
        /// The Document page
        /// </returns>
        public TPage ClickKeyCiteFlag<TPage>() where TPage : ICreatablePageObject
        {
            DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }
    }
}