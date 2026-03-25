namespace Framework.Common.UI.Products.Shared.Items
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    public class ItemsCollection<TItem> : IItemsCollection<TItem> where TItem : BaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsCollection{TItem}"/> class.
        /// </summary>
        /// <param name="collectionLocator">
        /// The collection locator.
        /// </param>
        /// <param name="itemLocator">
        /// The item locator.
        /// </param>
        public ItemsCollection(By collectionLocator, By itemLocator)
            : this(DriverExtensions.WaitForElement(collectionLocator), itemLocator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsCollection{TItem}"/> class.
        /// </summary>
        /// <param name="collectionContainer">
        /// The container.
        /// </param>
        /// <param name="itemLocator">
        /// The item locator.
        /// </param>
        public ItemsCollection(IWebElement collectionContainer, By itemLocator)
        {
            this.Container = collectionContainer;
            this.ItemLocator = itemLocator;
        }

        /// <summary>
        /// The count.
        /// </summary>
        public virtual int Count => DriverExtensions.IsDisplayed(this.Container, this.ItemLocator)
                                        ? DriverExtensions.GetElements(this.Container, this.ItemLocator).Count
                                        : 0;

        /// <summary>
        /// Gets the item locator.
        /// </summary>
        protected By ItemLocator { get; }

        /// <summary>
        /// Gets the container.
        /// </summary>
        protected IWebElement Container { get; }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        public virtual TItem this[int index]
        {
            get
            {
                DriverExtensions.WaitForElementDisplayed(this.Container, this.ItemLocator);
                return (TItem)Activator.CreateInstance(
                    typeof(TItem),
                    DriverExtensions.GetElements(this.Container, this.ItemLocator).ElementAt(index));
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        public virtual TItem this[string itemName]
        {
            get
            {
                DriverExtensions.WaitForElementDisplayed(this.Container, this.ItemLocator);
                return (TItem)Activator.CreateInstance(
                    typeof(TItem),
                    DriverExtensions.GetElements(this.Container, this.ItemLocator)
                                    .First(x => x.Text.Contains(itemName)));
            }
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Any() =>
            DriverExtensions.GetElements(this.Container, this.ItemLocator).Any()
            && DriverExtensions.IsDisplayed(this.ItemLocator, 5);

        /// <summary>
        /// The first.
        /// </summary>
        /// <returns>
        /// The item
        /// </returns>
        public virtual TItem First() =>
            (TItem)Activator.CreateInstance(
                typeof(TItem),
                DriverExtensions.GetElements(this.Container, this.ItemLocator).First());

        /// <summary>
        /// The last.
        /// </summary>
        /// <returns>
        /// The item
        /// </returns>
        public virtual TItem Last() =>
            (TItem)Activator.CreateInstance(
                typeof(TItem),
                DriverExtensions.GetElements(this.Container, this.ItemLocator).Last());

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Contains(string itemName) =>
            DriverExtensions.GetElements(this.Container, this.ItemLocator)
                            .Any(x => x.Text.Contains(itemName));

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            return this.GetItems().GetEnumerator();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
        /// <summary>
        /// The get items.
        /// </summary>
        /// <returns>
        /// The list of items
        /// </returns>
        protected virtual IEnumerable<TItem> GetItems() => DriverExtensions
            .GetElements(this.Container, this.ItemLocator)
            .Select(locator => (TItem)Activator.CreateInstance(typeof(TItem), locator));
    }
}