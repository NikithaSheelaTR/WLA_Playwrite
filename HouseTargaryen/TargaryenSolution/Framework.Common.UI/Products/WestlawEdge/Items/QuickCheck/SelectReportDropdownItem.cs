namespace Framework.Common.UI.Products.WestlawEdge.Items.QuickCheck
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// The select report drop down item.
    /// </summary>
    public class SelectReportDropdownItem : BaseItem, IDropdownOptionItem
    {
        private static readonly By LinkLocator = By.XPath("./a");
        private static readonly By DetailsLabelLocator = By.XPath("./a/span");

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectReportDropdownItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public SelectReportDropdownItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The link.
        /// </summary>
        public ILink Link => new Link(this.Container, LinkLocator);

        /// <summary>
        /// The labels.
        /// </summary>
        public IReadOnlyCollection<ILabel> Labels => new ElementsCollection<Label>(this.Container, DetailsLabelLocator);

        /// <summary>
        /// The option text.
        /// </summary>
        public string OptionText => this.Labels.First().Text;

        /// <summary>
        /// Gets the submitted date.
        /// </summary>
        public string SubmittedDate
        {
            get
            {
                string rawDate = this.Labels.ElementAt(1).Text;
                return new Regex(@"\b(?<month>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})\b").Match(rawDate).Value;
            }
        }

        /// <summary>
        /// The is selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSelected => this.Link.GetAttribute("class").Contains("SelectedReport");
    }
}