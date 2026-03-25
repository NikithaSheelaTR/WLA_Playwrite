namespace Framework.Common.UI.Products.Shared.Components.BreadCrumb
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Bread Crumb Item
    /// </summary>
    public class BreadCrumbItem : BaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadCrumbItem"/> class. 
        /// </summary>
        /// <param name="rootElement">
        /// The root Element.
        /// </param>
        public BreadCrumbItem(IWebElement rootElement) : base(rootElement)
        {
            this.Type = this.DetectType(this.Container);
        }

        /// <summary>
        /// Title attribute
        /// </summary>
        /// <returns>Title</returns>
        public string Title =>
            string.IsNullOrWhiteSpace(this.Container.GetAttribute("title"))
                ? this.Container.Text
                : this.Container.GetAttribute("title");

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// BreadCrumbItemType Type.
        /// </value>
        public BreadCrumbItemType Type { get; }

        /// <summary>
        /// Click
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>Page</returns>
        public T Click<T>() where T : ICreatablePageObject
        {
            if (this.IsBreadCrumbItemLink())
            {
                this.Container.Click();
                return DriverExtensions.CreatePageInstance<T>();
            }

            throw new InvalidOperationException("Click by Dead Link");
        }

        /// <summary>
        /// Inner Text
        /// </summary>
        /// <returns>Text</returns>
        public string GetBreadCrumbItemText() => this.Container.Text;

        /// <summary>
        /// Verify that type is Dead Link
        /// </summary>
        /// <returns> True if Dead link, false otherwise </returns>
        public bool IsBreadCrumbItemDeadLink() => this.Type.Equals(BreadCrumbItemType.DeadLink);

        /// <summary>
        /// Verify that type is Link
        /// </summary>
        /// <returns> True if link, false otherwise </returns>
        public bool IsBreadCrumbItemLink() => this.Type.Equals(BreadCrumbItemType.Link);

        /// <summary>
        /// The detect type.
        /// </summary>
        /// <param name="rootElement"> The root element. </param>
        /// <returns> The <see cref="BreadCrumbItemType"/>. </returns>
        private BreadCrumbItemType DetectType(IWebElement rootElement)
            =>
                rootElement.TagName.Equals("a")
                && rootElement.GetAttribute("class").Contains("co_website_browseBreadCrumbItem")
                    ? BreadCrumbItemType.Link
                    : BreadCrumbItemType.DeadLink;
    }
}