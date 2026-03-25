namespace Framework.Core.DataModel.Configuration.Collections
{
    using System.Configuration;

    using Framework.Core.DataModel.Configuration.Sections;

    /// <summary>
    /// A collection of entities in a repository.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier (enumeration).</typeparam>
    /// <typeparam name="TType">The type of the entity's domain type (enumeration).</typeparam>
    /// <typeparam name="TProxyType">The type of a proxy object to emit.</typeparam>
    internal abstract class EntityElementCollection<TId, TType, TProxyType> : ConfigurationElementCollection
        where TId : struct
        where TType : struct
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EntityElement<TId, TType, TProxyType>)element).Id;
        }
    }
}