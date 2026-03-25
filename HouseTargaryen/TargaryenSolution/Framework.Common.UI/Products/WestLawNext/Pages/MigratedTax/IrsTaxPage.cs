namespace Framework.Common.UI.Products.WestLawNext.Pages.MigratedTax
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.RightPaneComponents;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// IRS Tax Home Category Page
    /// </summary>
    public class IrsTaxPage : CommonMigratedTaxPage
    {
        private static readonly By IrsTaxLinksLocator = By.CssSelector(".co_2Column a");

        /// <summary>
        /// IRS Customized Libraries
        /// </summary>
        public IrsCustomizedLibrariesComponent IrsCustomizedLibraries { get; set; } = new IrsCustomizedLibrariesComponent();

        /// <summary>
        /// IRS Exclusive Content Widget
        /// </summary>
        public IrsExclusiveContentComponent IrsExclusiveContent { get; set; } = new IrsExclusiveContentComponent();

        /// <summary>
        /// Topical Libraries
        /// </summary>
        public TopicalLibrariesComponent TopicalLibraries { get; set; } = new TopicalLibrariesComponent();

        /// <summary>
        /// Method for verifying if the text on link is bold
        /// </summary>
        /// <returns>List of elements</returns>
        public List<string> GetBoldLinksFromIrsTaxPage(string fontWeight = "700")
            => DriverExtensions.GetElements(IrsTaxLinksLocator).Where(link => link.GetCssValue("font-weight").Equals(fontWeight))
                                .Select(elem => elem.Text).ToList();
    }
}