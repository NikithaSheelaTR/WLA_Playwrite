namespace Framework.Core.Cobalt.Uds
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Framework.Core.Utils;
    using Newtonsoft.Json;


    /// <summary>
    /// Stores information regarding UDS Session Bindings.
    /// </summary>
    public sealed class SessionBindings
    {
        /// <summary>
        /// Gets or sets the routing table for the UDS Session Bindings.
        /// </summary>
        /// <value>The routing table for the UDS Session Bindings.</value>
        public Dictionary<string, string> RoutingTable { get; set; }

        /// <summary>
        /// Gets or sets an indication whether within the UDS Session Bindings whether SSL is enabled.
        /// </summary>
        /// <value>An indication whether within the UDS Session Bindings whether SSL is enabled.</value>
        public bool? SslEnabled { get; set; }

        /// <summary>
        /// Gets or sets the application configuration for the UDS Session Bindings.
        /// </summary>
        /// <value>The application configuration for the UDS Session Bindings.</value>
        public Dictionary<string, string> ApplicationConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the feature resource permissions for the UDS Session Bindings.
        /// </summary>
        /// <value>The feature resource permissions for the UDS Session Bindings.</value>
        public Dictionary<string, Dictionary<string, string>> FeatureResourcePermissions { get; set; }

        /// <summary>
        /// Gets or sets the infrastructure access controls for the UDS Session Bindings.
        /// </summary>
        /// <value>The infrastructure access controls for the UDS Session Bindings.</value>
        public IList<string> InfrastructureAccessControls { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="SessionBindings"/> is equal to the current <see cref="SessionBindings"/>.
        /// </summary>
        /// <param name="aThat">The <see cref="SessionBindings"/> to compare with the current <see cref="SessionBindings"/>.</param>
        /// <returns><b>true</b> if the specified Object is equal to the current <see cref="SessionBindings"/>; otherwise, <b>false</b>.</returns>
        public override bool Equals(object aThat)
        {
            if (aThat == null || this.GetType() != aThat.GetType())
            {
                return false;
            }

            var that = (SessionBindings)aThat;
            return SessionBindings.StringDictionariesEqual(this.RoutingTable, that.RoutingTable)
                   && EqualsUtils.AreEqual(this.SslEnabled, that.SslEnabled)
                   && SessionBindings.StringDictionariesEqual(
                       this.ApplicationConfiguration,
                       that.ApplicationConfiguration)
                   && SessionBindings.FeatureResourcePermissionsEqual(
                       this.FeatureResourcePermissions,
                       that.FeatureResourcePermissions) 
                   && EqualsUtils.AreEqual(
                       this.InfrastructureAccessControls,
                       that.InfrastructureAccessControls);
        }

        /// <summary>
        /// Determines whether two dictionaries containing a string for the key and string for the value contain equal contents by value - not reference.
        /// </summary>
        /// <param name="aThis">A dictionary containing a string for the key and string for the value.</param>
        /// <param name="aThat">A second dictionary containing a string for the key and string for the value to compare against<c>aThis</c>.</param>
        /// <returns>An indication whether the two dictionaries contain equal contents by value - not reference.</returns>
        private static bool StringDictionariesEqual(Dictionary<string, string> aThis, Dictionary<string, string> aThat)
        {
            // determine if both are valued or both are null
            if ((aThis == null) != (aThat == null))
            {
                return false;
            }

            // determine if both are the same object
            if (!object.ReferenceEquals(aThis, aThat))
            {
                // if not same object, do they have the same count?
                if (aThis.Count != aThat.Count)
                {
                    return false;
                }

                // if same length, then compare value-by-value
                foreach (KeyValuePair<string, string> keyValuePair in aThis)
                {
                    string value;
                    if (!aThat.TryGetValue(keyValuePair.Key, out value) || !EqualsUtils.AreEqual(keyValuePair.Value, value))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two dictionaries containing a string for the key and a dictionary for the the value contain equal contents by value - not reference.
        /// </summary>
        /// <param name="aThis">A dictionary containing a string for the key and a dictionary for the value.</param>
        /// <param name="aThat">A second dictionary containing a string for the key and a dictionary for the value to compare against<c>aThis</c>.</param>
        /// <returns>An indication whether the two dictionaries contain equal contents by value - not reference.</returns>
        private static bool FeatureResourcePermissionsEqual(Dictionary<string, Dictionary<string, string>> aThis, Dictionary<string, Dictionary<string, string>> aThat)
        {
            // determine if both are valued or both are null
            if ((aThis == null) != (aThat == null))
            {
                return false;
            }

            // determine if both are the same object
            if (!object.ReferenceEquals(aThis, aThat))
            {
                // if not same object, do they have the same count?
                if (aThis.Count != aThat.Count)
                {
                    return false;
                }

                // if same length, then compare value-by-value
                foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in aThis)
                {
                    Dictionary<string, string> resourcePermissions;
                    if (!aThat.TryGetValue(keyValuePair.Key, out resourcePermissions) || !SessionBindings.StringDictionariesEqual(keyValuePair.Value, resourcePermissions))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="SessionBindings"/>.</returns>
        public override int GetHashCode()
        {
            int result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.RoutingTable);
            result = HashCodeUtils.Hash(result, this.SslEnabled);
            result = HashCodeUtils.Hash(result, this.ApplicationConfiguration);
            result = HashCodeUtils.Hash(result, this.FeatureResourcePermissions);
            result = HashCodeUtils.Hash(result, this.InfrastructureAccessControls);
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="SessionBindings"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="SessionBindings"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            using (var jsonWriter = new JsonTextWriter(new StringWriter(sb)))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("RoutingTable");

                if (this.RoutingTable != null)
                {
                    jsonWriter.WriteStartObject();

                    foreach (KeyValuePair<string, string> kvp in this.RoutingTable)
                    {
                        jsonWriter.WritePropertyName(kvp.Key);
                        jsonWriter.WriteValue(kvp.Value);
                    }

                    jsonWriter.WriteEndObject();
                }
                else
                {
                    jsonWriter.WriteNull();
                }

                jsonWriter.WritePropertyName("SslEnabled");
                jsonWriter.WriteValue(this.SslEnabled);

                jsonWriter.WritePropertyName("ApplicationConfiguration");

                if (this.ApplicationConfiguration != null)
                {
                    jsonWriter.WriteStartObject();

                    foreach (KeyValuePair<string, string> kvp in this.ApplicationConfiguration)
                    {
                        jsonWriter.WritePropertyName(kvp.Key);
                        jsonWriter.WriteValue(kvp.Value);
                    }

                    jsonWriter.WriteEndObject();
                }
                else
                {
                    jsonWriter.WriteNull();
                }

                jsonWriter.WritePropertyName("FeatureResourcePermissions");

                if (this.FeatureResourcePermissions != null)
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName("FeatureResourcePermissionsMap");
                    jsonWriter.WriteStartObject();

                    foreach (KeyValuePair<string, Dictionary<string, string>> kvp in this.FeatureResourcePermissions)
                    {
                        jsonWriter.WritePropertyName(kvp.Key);
                        jsonWriter.WriteStartObject();

                        foreach (KeyValuePair<string, string> interiorKvp in kvp.Value)
                        {
                            jsonWriter.WritePropertyName(interiorKvp.Key);
                            jsonWriter.WriteValue(interiorKvp.Value);
                        }

                        jsonWriter.WriteEndObject();
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }
                else
                {
                    jsonWriter.WriteNull();
                }

                jsonWriter.WritePropertyName("InfrastructureAccessControls");
                if (this.InfrastructureAccessControls != null)
                {
                    jsonWriter.WriteStartArray();

                    foreach (string infrastructureAccessControl in this.InfrastructureAccessControls)
                    {
                        jsonWriter.WriteValue(infrastructureAccessControl);
                    }

                    jsonWriter.WriteEndArray();
                }
                else
                {
                    jsonWriter.WriteNull();
                }

                jsonWriter.WriteEndObject();
            }

            return sb.ToString();
        }
    }
}
