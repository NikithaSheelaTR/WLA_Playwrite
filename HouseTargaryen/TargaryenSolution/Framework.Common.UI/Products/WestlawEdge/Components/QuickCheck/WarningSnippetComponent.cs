namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Warning snippet component
    /// </summary>
    public class WarningSnippetComponent
    {
        private static readonly By TreatmentDocumentLinkLocator = By.XPath(".//div[@class='DA-TreatmentPhrase']/a");
        private static readonly By FullTreatmentPhraseLocator = By.XPath(".//div[@class='DA-TreatmentPhrase']");
        private static readonly By TreatmentTextLocator = By.XPath(".//div[@class='DA-KCSnippetText']/a");

        /// <summary>
        /// Initializes a new instance of the <see cref="WarningSnippetComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public WarningSnippetComponent(IWebElement container)
        {
            this.Container = container;
        }

        private IWebElement Container;

        /// <summary>
        /// The snippet type
        /// </summary>
        /// <returns>
        /// An instance of DocAnalyzerSnippets
        /// </returns>
        public QuickCheckKcSnippets Type =>
              this.Container.GetAttribute("class").Replace("DA-KCWarningSnippet ", "")
              .Trim().GetEnumValueByText<QuickCheckKcSnippets>(sourceFolder: @"Resources/EnumPropertyMaps/WestlawEdge/QuickCheck");

        /// <summary>
        /// Get a list of Treatment links
        /// </summary>
        public IReadOnlyCollection<ILink> TreatmentLinks => new ElementsCollection<Link>(this.Container, TreatmentDocumentLinkLocator);

        /// <summary>
        /// Get treatment texts links
        /// </summary>
        public IReadOnlyCollection<ILink> TreatmentTextLinks => new ElementsCollection<Link>(this.Container, TreatmentTextLocator);

        /// <summary>
        /// FullTreatmentPhraseText
        /// </summary>
        public IReadOnlyCollection<ILabel> FullTreatmentPhrasesLabels => new ElementsCollection<Label>(this.Container, FullTreatmentPhraseLocator);
    }
}
