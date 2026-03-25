namespace Framework.Common.UI.Products.WestlawEdge.Items
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    public class QuickCheckItemsCollection<TItem> : ItemsCollection<TItem> where TItem : BaseItem
    {
        private const string DocGuidLctMask = ".//a[contains(@href,'{0}')] | .//div[contains(@id,'{0}')]";

        private readonly string parentElement = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckItemsCollection{TItem}"/> class.
        /// </summary>
        /// <param name="collectionLocator">collectionLocator</param>
        /// <param name="itemLocator">itemLocator</param>
        public QuickCheckItemsCollection(By collectionLocator, By itemLocator)
            : base(collectionLocator, itemLocator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckItemsCollection{TItem}"/> class.
        /// </summary>
        /// <param name="collectionContainer">collectionLocator</param>
        /// <param name="itemLocator">itemLocator</param>
        public QuickCheckItemsCollection(IWebElement collectionContainer, By itemLocator)
            : base(collectionContainer, itemLocator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckItemsCollection{TItem}"/> class.
        /// </summary>
        /// <param name="collectionLocator">collectionLocator</param>
        /// <param name="itemLocator">itemLocator</param>
        /// /// <param name="parentElement">parentElement</param>
        public QuickCheckItemsCollection(By collectionLocator, By itemLocator, string parentElement)
            : base(collectionLocator, itemLocator) => this.parentElement = parentElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckItemsCollection{TItem}"/> class.
        /// </summary>
        /// <param name="collectionContainer">collectionLocator</param>
        /// <param name="itemLocator">itemLocator</param>
        /// <param name="parentElement">parentElement</param>
        public QuickCheckItemsCollection(IWebElement collectionContainer, By itemLocator, string parentElement)
            : base(collectionContainer, itemLocator) => this.parentElement = parentElement;

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        public override TItem this[string itemName]
        {
            get
            {
                DriverExtensions.WaitForElementDisplayed(this.Container, this.ItemLocator);
                return itemName.IsWestlawGuid() ? this.GetItemByGuid(itemName) : this.GetItemByName(itemName);
            }
        }

        /// <summary>
        /// The get item by guid.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        protected virtual TItem GetItemByGuid(string guid) =>
            this.parentElement == null
            ? (TItem)Activator.CreateInstance(
                typeof(TItem), DriverExtensions.GetElement(this.Container, this.ItemLocator, By.XPath(string.Format(DocGuidLctMask, guid))))
            : (TItem)Activator.CreateInstance(
                typeof(TItem), DriverExtensions.GetElement(this.Container, this.ItemLocator,
                    By.XPath(string.Format(DocGuidLctMask, guid))).GetParentElement(this.parentElement));

        /// <summary>
        /// Gets item
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        protected virtual TItem GetItemByName(string name) =>
            (TItem)Activator.CreateInstance(
                typeof(TItem),
            DriverExtensions.GetElements(this.Container, this.ItemLocator).First(item => item.Text.Contains(name)));
    }
}
