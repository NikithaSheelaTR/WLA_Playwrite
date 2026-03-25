namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;

    /// <summary>
    /// Represents a generic Cobalt entity entry in a test environment repository.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier (enumeration).</typeparam>
    /// <typeparam name="TType">The type of the entity's domain type (enumeration).</typeparam>
    public class CobaltEntityInfo<TId, TType> : ICobaltEntityInfo
        where TId : struct
        where TType : struct
    {
        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        public TId Id { get; internal set; }

        /// <summary>
        /// Gets the reporting tag name.
        /// </summary>
        public string TagName { get; internal set; }

        /// <summary>
        /// Gets the entity type.
        /// </summary>
        public TType Type { get; internal set; }

        /// <summary>
        /// Gets the entity type.
        /// </summary>
        Enum ICobaltEntityInfo.Id => this.Id as Enum;

        /// <summary>
        /// Gets the entity type.
        /// </summary>
        Enum ICobaltEntityInfo.Type => this.Type as Enum;

        /// <summary>
        /// A string representation of the current object.
        /// </summary>
        /// <returns>Returns a string representation of the current object.</returns>
        public override string ToString() =>
            string.Format(
                "[{0}]: Id={1}, Type={2}, Tag={3}",
                this.GetType().Name,
                this.Id,
                this.Type,
                this.TagName);
    }
}