namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    using System;

    using Framework.Common.UI.Products.WestLawAnalytics.Enums;

    /// <summary>
    /// Alert Model for Westlaw Analytics
    /// </summary>
    public class AlertModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Alert Name
        /// </summary>
        public string AlertName { get; set; }

        /// <summary>
        /// Cap Amount
        /// </summary>
        public string CapAmount { get; set; }

        /// <summary>
        /// Apply To option
        /// </summary>
        public ApplyToOptions ApplyTo { get; set; }

        /// <summary>
        /// Cost Condition option
        /// </summary>
        public CostConditionOptions CostCondition { get; set; }

        /// <summary>
        /// Greater Or Less Than option
        /// </summary>
        public GreaterOrLessThanOptions GreaterOrLessThan { get; set; }

        /// <summary>
        /// Time Frame 
        /// </summary>
        public TimeFrameOptions TimeFrame { get; set; }

        /// <summary>
        /// From Date
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// To Date
        /// </summary>
        public string ToDate { get; set; }

        /// <summary>
        /// Override 'GetHashCode' method
        /// </summary>
        /// <returns> Hash Code </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Email != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.Email) : 0;
                hashCode = (hashCode * 397) ^ (this.AlertName != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.AlertName) : 0);
                hashCode = (hashCode * 397) ^ (this.CapAmount != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.CapAmount) : 0);
                hashCode = (hashCode * 397) ^ (int)this.ApplyTo;
                hashCode = (hashCode * 397) ^ (int)this.CostCondition;
                hashCode = (hashCode * 397) ^ (int)this.GreaterOrLessThan;
                hashCode = (hashCode * 397) ^ (int)this.TimeFrame;
                hashCode = (hashCode * 397) ^ (this.FromDate != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.FromDate) : 0);
                hashCode = (hashCode * 397) ^ (this.ToDate != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToDate) : 0);
                return hashCode;
            }
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

            return this.Equals((AlertModel)obj);
        }

        /// <summary>
        /// Compare 2 objects
        /// </summary>
        /// <param name="alertModel"> Alert Model to compare </param>
        /// <returns> True if objects equals, false otherwise </returns>
        protected bool Equals(AlertModel alertModel)
        {
            return string.Equals(this.Email, alertModel.Email, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(this.AlertName, alertModel.AlertName, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(this.CapAmount, alertModel.CapAmount, StringComparison.InvariantCultureIgnoreCase)
                   && this.ApplyTo == alertModel.ApplyTo && this.CostCondition == alertModel.CostCondition
                   && this.GreaterOrLessThan == alertModel.GreaterOrLessThan && this.TimeFrame == alertModel.TimeFrame
                   && string.Equals(this.FromDate, alertModel.FromDate, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(this.ToDate, alertModel.ToDate, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
