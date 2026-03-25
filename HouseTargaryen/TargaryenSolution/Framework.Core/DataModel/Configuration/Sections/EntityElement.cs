namespace Framework.Core.DataModel.Configuration.Sections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Represents a generic entity entry in a test environment repository file.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier (enumeration).</typeparam>
    /// <typeparam name="TType">The type of the entity's domain type (enumeration).</typeparam>
    /// <typeparam name="TProxyType">The type of a proxy object to emit.</typeparam>
    internal abstract class EntityElement<TId, TType, TProxyType> : ConfigurationElement, IProxyEmittable<TProxyType>
        where TId : struct
        where TType : struct
    {
        /// <summary>
        /// Gets a light-weight proxy object for a corresponding configuration element.
        /// </summary>
        public abstract TProxyType ProxyObject { get; }

        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.IdPropertyName, IsKey = true, IsRequired = true)]
        internal TId Id => (TId)this[ConfigurationConstants.IdPropertyName];

        /// <summary>
        /// Gets the reporting tag name.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.ReportingTagPropertyName, IsRequired = true)]
        internal string TagName => (string)this[ConfigurationConstants.ReportingTagPropertyName];

        /// <summary>
        /// Gets the entity type.
        /// </summary>
        [ConfigurationProperty(ConfigurationConstants.TypePropertyName, IsRequired = true)]
        internal TType Type => (TType)this[ConfigurationConstants.TypePropertyName];
    }
}