namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Precision Overruling Flag Dialog
    /// </summary>
    public class PrecisionOverrulingFlagDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("co_kcOverrulingFlagPopup");
        private static readonly By CitationLabelLocator = By.XPath(".//div[@class='co_kcFlagPopup_doc OverrulingPinPointDoc']/span");
        private static readonly By FlagIconLocator = By.XPath(".//div[@class='co_citatorFlag co_rStripedFlagSm']");
        private static readonly By TitleLinkLocator = By.XPath(".//h4[@class='co_kcFlagPopup_titleLink']/a");
        private static readonly By ParagraphLinkLocator = By.XPath(".//a[@class='co_overrulingSnippetLink']");

        /// <summary>
        /// Title label
        /// </summary>
        public ILink TitleLabel => new Link(ContainerLocator, TitleLinkLocator);
                
        /// <summary>
        ///Striped flag image
        /// </summary>
        public IImage FlagImage => new Image(ContainerLocator, FlagIconLocator);

        /// <summary>
        ///Paragraph links
        /// </summary>
        public IReadOnlyCollection<ILink> ParagraphLinks => new ElementsCollection<Link>(ContainerLocator, ParagraphLinkLocator);

        /// <summary>
        /// GetAggregatedCitationText
        /// </summary>
        /// <returns></returns>
        public string GetAggregatedCitationText()
        {
           return DriverExtensions.GetElements(CitationLabelLocator)
                                            .Select(elem => elem.Text)
                                            .ToList()
                                            .Aggregate((i, j) => i + " " + j)
                                            .Trim();
        }
    }
}
