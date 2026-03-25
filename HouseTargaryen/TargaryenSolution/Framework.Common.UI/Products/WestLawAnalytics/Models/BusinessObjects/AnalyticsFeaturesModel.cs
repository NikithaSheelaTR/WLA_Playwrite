namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    using System;

    /// <summary>
    /// Features Model
    /// </summary>
    public class AnalyticsFeaturesModel : IEquatable<AnalyticsFeaturesModel>
    {
        /// <summary>
        /// Client Matter
        /// </summary>
        public bool ClientMatter { get; set; }

        /// <summary>
        /// Practice Area
        /// </summary>
        public bool PracticeArea { get; set; }

        /// <summary>
        /// Research Description
        /// </summary>
        public bool ResearchDescription { get; set; }

        /// <summary>
        /// Chargeable To Client
        /// </summary>
        public bool ChargeableToClient { get; set; }

        /// <summary>
        /// Compare 2 objects
        /// </summary>
        /// <param name="featuresModel"> Features Model to compare </param>
        /// <returns> True if objects equals, false otherwise </returns>
        public bool Equals(AnalyticsFeaturesModel featuresModel)
        {
            if (object.ReferenceEquals(null, featuresModel))
            {
                return false;
            }

            if (object.ReferenceEquals(this, featuresModel))
            {
                return true;
            }

            return this.ClientMatter == featuresModel.ClientMatter && this.PracticeArea == featuresModel.PracticeArea
                   && this.ResearchDescription == featuresModel.ResearchDescription
                   && this.ChargeableToClient == featuresModel.ChargeableToClient;
        }

        /// <summary>
        /// Compare 2 objects
        /// </summary>
        /// <param name="obj"> Object to compare </param>
        /// <returns> True if objects equals, false otherwise </returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((AnalyticsFeaturesModel)obj);
        }

        /// <summary>
        /// Override 'GetHashCode' method
        /// </summary>
        /// <returns> Hash Code </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.ClientMatter.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PracticeArea.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ResearchDescription.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ChargeableToClient.GetHashCode();
                return hashCode;
            }
        }
    }
}
