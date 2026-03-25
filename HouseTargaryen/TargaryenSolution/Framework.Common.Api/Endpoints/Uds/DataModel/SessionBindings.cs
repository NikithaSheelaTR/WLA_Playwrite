namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// The session bindings.
    /// </summary>
    [DataContract]
    public class SessionBindings
    {
        /// <summary>
        /// Gets or sets the application configuration.
        /// </summary>
        [DataMember(Name = "ApplicationConfiguration")]
        public Dictionary<string, string> ApplicationConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the feature resource permissions.
        /// </summary>
        [DataMember(Name = "FeatureResourcePermissions")]
        public FeatureResourcePermissions FeatureResourcePermissions { get; set; }

        /// <summary>
        /// Gets or sets the infrastructure access controls.
        /// </summary>
        [DataMember(Name = "InfrastructureAccessControls")]
        public List<string> InfrastructureAccessControls { get; set; }

        /// <summary>
        /// Gets or sets the routing table.
        /// </summary>
        [DataMember(Name = "RoutingTable")]
        public Dictionary<string, string> RoutingTable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SSL enabled.
        /// </summary>
        [DataMember(Name = "SslEnabled")]
        public bool SslEnabled { get; set; }

        /// <summary>
        /// The set IACs
        /// </summary>
        /// <param name="iacs">
        /// The IACs.
        /// </param>
        /// <returns>
        /// The <see cref="SessionBindings"/>.
        /// </returns>
        public SessionBindings SetIacs(List<string> iacs)
        {
            if (iacs == null || !iacs.Any())
            {
                return this;
            }

            this.InfrastructureAccessControls.AddRange(
                iacs.Where(iac => !iac.Equals(string.Empty)).Select(iac => iac).ToList());
            return this;
        }

        /// <summary>
        /// The Inset IACs
        /// </summary>
        /// <param name="iacs">
        /// The IACs.
        /// </param>
        /// <returns>
        /// The <see cref="SessionBindings"/>.
        /// </returns>
        public SessionBindings UnsetIacs(List<string> iacs)
        {
            if (iacs == null || !iacs.Any())
            {
                return this;
            }

            this.InfrastructureAccessControls = this
                .InfrastructureAccessControls.Where(iac => !iacs.Contains(iac)).Select(iac => iac).ToList();
            return this;
        }

        /// <summary>
        /// The grant FACs
        /// ToDo: replace input parameters by List{Feature}
        /// </summary>
        /// <param name="facs">
        /// The FACs.
        /// </param>
        /// <returns>
        /// The <see cref="SessionBindings"/>.
        /// </returns>
        public SessionBindings GrantFacs(List<string> facs)
        {
            if (facs == null || !facs.Any())
            {
                return this;
            }

            this.FeatureResourcePermissions.FeatureGroups = this.ModifyFacs(facs, FeatureSelectionOption.Grant);

            return this;
        }

        /// <summary>
        /// The Deny FACs
        /// ToDo: replace input parameters by List{Feature}
        /// </summary>
        /// <param name="facs">
        /// The FACs.
        /// </param>
        /// <returns>
        /// The <see cref="SessionBindings"/>.
        /// </returns>
        public SessionBindings DenyFacs(List<string> facs)
        {
            if (facs == null || !facs.Any())
            {
                return this;
            }

            this.FeatureResourcePermissions.FeatureGroups = this.ModifyFacs(facs, FeatureSelectionOption.Deny);

            return this;
        }

        /// <summary>
        /// The method modifies FeatureResourcePermissionsMap.
        /// </summary>
        /// <param name="facs">
        /// The FACs.
        /// </param>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <returns>
        /// </returns>
        private List<FeaturesGroup> ModifyFacs(ICollection<string> facs, FeatureSelectionOption state)
        {
            List<FeaturesGroup> temporaryGroups = this.FeatureResourcePermissions.FeatureGroups;

            List<Feature> features = temporaryGroups.First(featureGroup => featureGroup.Name.Equals("APPLICATION")).Features;

            if (state == FeatureSelectionOption.Grant)
            {
                if (!features.Any(fac => facs.Contains(fac.Name)))
                {
                    facs.ToList().ForEach(fac => features.Add(new Feature { Name = fac, State = state }));
                }
            }

            if (state == FeatureSelectionOption.Deny)
            {
                features.Where(fac => facs.Contains(fac.Name)).ToList().ForEach(fac => fac.State = state);
            }

            return temporaryGroups;
        }
    }
}