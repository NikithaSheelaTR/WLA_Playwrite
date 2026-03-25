namespace Framework.Common.UI.Raw.WestlawEdge.Items.History
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Each item represents item (node) in the Graphical History tree
    /// </summary>
    public class GraphicalNodeItem : BaseItem
    {
        private const string DocumentNodeTitleLocator = ".//*[@class='Graphical-Document-Node-Name']//*[name()='title']";
        private const string SearchNodeTitleLocator = ".//*[@class='Graphical-Search-Node-Title']";
        private const string FilterNodeTitleLocator = ".//*[@class='Graphical-Filter-Node-Title']";
        private const string SearchNodeMetaLocator = "//*[@class='Graphical-Search-Node-Meta']";
        private const string DocumentNodeMetaLocator = "//*[@class='Graphical-Document-Node-Meta']";
        private const string FilterNodeMetaLocator = "//*[@class='Graphical-Filter-Node-Meta']";
        private const string NodeSequenceNumberLocator = "./parent::*//*[@class='Graphical-Sequence-Number-Text']";
        private const string DocumentDurationLocator = "//*[@class='Graphical-Node-Duration-Text']";
        private const string DocumentNodeContentTypeLocator = "//*[@class='Graphical-Document-Node-Title']";
        private static readonly By KeyCiteFlagLocator = By.XPath("//*[name()='path' and contains(@class,'GH-Node')]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="GraphicalNodeItem"/> class. 
        /// </summary>
        /// <param name="container">Container</param>
        public GraphicalNodeItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Returns the type of Graphical node
        /// </summary>
        public string NodeType =>
            new List<string>
                    {
                        "Search", "Filter", "Document", "KeyCite"
                    }
                .FirstOrDefault(item => this.Container.GetAttribute("class").Contains(item)) ?? string.Empty;

        /// <summary>
        /// Returns the title of Node
        /// </summary>
        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NodeType))
                {
                    return DriverExtensions.GetElement(this.Container,
                        By.XPath(
                            new List<string>
                            {
                                DocumentNodeTitleLocator, SearchNodeTitleLocator, FilterNodeTitleLocator
                            }.FirstOrDefault(item => item.Contains(this.NodeType)))).InnerHtml();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the metadata
        /// </summary>
        public string MetaData
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NodeType))
                {
                    return DriverExtensions.GetElement(
                        By.XPath(
                            new List<string>
                                {
                                    DocumentNodeMetaLocator, SearchNodeMetaLocator, FilterNodeMetaLocator
                                }.FirstOrDefault(item => item.Contains(this.NodeType)))).InnerHtml();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
                {
                    string flagClass = DriverExtensions.GetAttribute("class", this.Container, KeyCiteFlagLocator);
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                        model => model.ClassName,
                        string.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdgePremium/History");
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Returns the duration taken to perform Event
        /// </summary>
        public string Duration
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NodeType))
                {
                    return DriverExtensions.GetElement(By.XPath(DocumentDurationLocator)).Text;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the Document Content Type
        /// </summary>
        public string DocumentContent
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NodeType))
                {
                    return DriverExtensions.GetElement(By.XPath(DocumentNodeContentTypeLocator)).Text;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the sequence number shown for current Node
        /// </summary>
        public string SequenceNumberLabel =>
            DriverExtensions.GetText(By.XPath(NodeSequenceNumberLocator), baseElement: this.Container, timeOut: 5)
                            .Trim('.');

        /// <summary>
        /// Clicks on Node to open Details dialog
        /// </summary>
        public IButton OpenNodeDetailsButton => new CustomClickButton(this.Container);
    }
}
