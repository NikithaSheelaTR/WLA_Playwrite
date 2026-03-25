namespace Framework.Common.UI.Interfaces.Components
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Items;

    /// <summary>
    /// The ItemsCollection interface.
    /// </summary>
    /// <typeparam name="TItem">The type of items</typeparam>
    public interface IItemsCollection<out TItem> : IReadOnlyList<TItem> where TItem : BaseItem
    {
        /// <summary>
        /// Gets item by it's name
        /// </summary>
        /// <param name="name">The name of item</param>
        /// <returns>The item</returns>
        TItem this[string name] { get; }

        /// <summary>
        /// The any.
        /// </summary>
        /// <returns>True if results are not empty</returns>
        bool Any();
        
        /// <summary>
        /// The first.
        /// </summary>
        /// <returns>The First item of the collection</returns>
        TItem First();

        /// <summary>
        /// The last.
        /// </summary>
        /// <returns>The Last item of the collection</returns>
        TItem Last();

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>True if results contain desired item </returns>
        bool Contains(string name);
    }
}