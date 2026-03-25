namespace Framework.Common.Api.Utilities
{
    using System;

    using Framework.Common.Api.Endpoints.Nlu.DataModel;
    using Framework.Core.Utils;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The nlu json converter.
    /// </summary>
    public class NluJsonConverter : GenericJsonConverter<NluIntentV3>
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <param name="jObject">
        /// The j object.
        /// </param>
        /// <returns>
        /// The <see cref="NluIntentV3"/>.
        /// </returns>
        protected override NluIntentV3 Create(Type objectType, JObject jObject)
            =>
            GenericJsonConverter<NluIntentV3>.FieldExists(jObject, "result", JTokenType.String)
                ? (NluIntentV3) new NluTrDiscoverIntentV3()
                : new NluLegalAnalyticsIntentV3();
    }
}
