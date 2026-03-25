namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;

    /// <summary>
    /// Represents a view of a Cobalt entity entry in a test environment repository.
    /// </summary>
    public interface ICobaltEntityInfo
    {
        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        Enum Id { get; }

        /// <summary>
        /// Gets the reporting tag name.
        /// </summary>
        string TagName { get; }

        /// <summary>
        /// Gets the entity type.
        /// </summary>
        Enum Type { get; }

        /// <summary>
        /// A string representation of the current object.
        /// </summary>
        /// <returns>Returns a string representation of the current object.</returns>
        string ToString();
    }
}