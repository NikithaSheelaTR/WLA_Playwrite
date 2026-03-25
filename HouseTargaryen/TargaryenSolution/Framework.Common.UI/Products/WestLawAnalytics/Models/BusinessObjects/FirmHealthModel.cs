namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    /// <summary>
    /// Describe item of the grid from Analytics page in Westlaw Analytics
    /// </summary>
    public class FirmHealthModel
    {
        /// <summary>
        /// Description: Office Location/User/Client/Practice Area/Product Platform
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// In Plan
        /// </summary>
        public string InPlan { get; set; }

        /// <summary>
        /// Out of Plan
        /// </summary>
        public string OutOfPlan { get; set; }

        /// <summary>
        /// Standard Charge
        /// </summary>
        public string StandardCharge { get; set; }

        /// <summary>
        /// % Out of Plan
        /// </summary>
        public string PercentOutOfPlan { get; set; }
    }
}
