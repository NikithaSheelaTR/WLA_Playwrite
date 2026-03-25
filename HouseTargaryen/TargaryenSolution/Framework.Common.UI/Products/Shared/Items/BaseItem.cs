namespace Framework.Common.UI.Products.Shared.Items
{
    using System;

    using Framework.Common.UI.Interfaces.Models;
    using Framework.Common.UI.Utils.Mapper;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// The base item.
    /// </summary>
    public abstract class BaseItem : BaseWebObject, IMappable
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Framework.Common.UI.Products.Shared.Items.BaseItem" /> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        protected BaseItem(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        protected IWebElement Container { get; }

        /// <inheritdoc />
        public TModel ToModel<TModel>() => MapperService.Map<TModel>(this);

        /// <inheritdoc />
        public object ToModel(Type destinationType) => MapperService.Map(this, this.GetType(), destinationType);
    }
}