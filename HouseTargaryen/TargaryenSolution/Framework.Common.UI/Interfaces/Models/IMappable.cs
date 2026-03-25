namespace Framework.Common.UI.Interfaces.Models
{
    using System;

    /// <summary>
    /// The IMappable Interface
    /// All items that could be mapped into model should inherit this interface 
    /// </summary>
    public interface IMappable
    {
        /// <summary>
        /// Maps current class instance to desired model.
        /// </summary>
        /// <typeparam name="TModel">
        /// the model type
        /// </typeparam>
        /// <returns>
        /// The desired model.
        /// </returns>
        TModel ToModel<TModel>();

        /// <summary>
        /// Maps current class instance to desired model.
        /// </summary>
        /// <param name="destinationType">
        /// The destination type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object ToModel(Type destinationType);
    }
}