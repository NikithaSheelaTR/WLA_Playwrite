namespace Framework.Core.DataModel.Configuration.Collections
{
    using System;
    using System.Configuration;
    using System.Runtime.InteropServices;

    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Sections;

    /// <summary>
    /// Collection of endpoints in a repository.
    /// </summary>
    internal class EndpointElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndpointElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            int sizeOfEnvId = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(EnvironmentId))),
                sizeOfModuleId = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(CobaltModuleId))),
                sizeOfProductId = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(CobaltProductId)));
            var bitHash = new byte[sizeOfEnvId + sizeOfModuleId + sizeOfProductId];
            var ei = (EndpointElement)element;

            Array.Copy(ei.EnvironmentId.GetBytes(), 0, bitHash, 0, sizeOfEnvId);
            Array.Copy(ei.ModuleId.GetBytes(), 0, bitHash, sizeOfEnvId, sizeOfModuleId);
            Array.Copy(ei.ProductId.GetBytes(), 0, bitHash, sizeOfEnvId + sizeOfModuleId, sizeOfProductId);
            return bitHash;
        }
    }
}