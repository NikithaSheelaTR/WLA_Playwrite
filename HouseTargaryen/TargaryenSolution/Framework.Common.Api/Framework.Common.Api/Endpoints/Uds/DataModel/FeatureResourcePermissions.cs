namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// The feature resource permissions.
    /// </summary>
    [DataContract]
    public sealed class FeatureResourcePermissions
    {
        /// <summary>
        /// Gets the feature groups.
        /// </summary>
        public List<FeaturesGroup> FeatureGroups
        {
            get
            {
                return this.FeaturesGroupsFromJson.Select(
                    group =>
                        {
                            IEnumerable<Feature> features = group.Value.Select(
                                feature =>
                                    {
                                        FeatureSelectionOption option;
                                        Enum.TryParse(
                                            feature.Value,
                                            true,
                                            out option);
                                        return new Feature
                                        {
                                            Name =
                                                           feature
                                                               .Key,
                                            State =
                                                           option
                                        };
                                    });
                            return new FeaturesGroup
                            {
                                Name = group.Key,
                                Features = new List<Feature>(features)
                            };
                        }).ToList();
            }

            set
            {
                foreach (FeaturesGroup featureGroup in value)
                {
                    foreach (Feature feature in featureGroup.Features)
                    {
                        this.FeaturesGroupsFromJson[featureGroup.Name][feature.Name] = feature.State.ToString();
                    }
                }
            }
        }

        [DataMember(Name = "FeatureResourcePermissionsMap")]
        private Dictionary<string, Dictionary<string, string>> FeaturesGroupsFromJson { get; set; }
    }
}