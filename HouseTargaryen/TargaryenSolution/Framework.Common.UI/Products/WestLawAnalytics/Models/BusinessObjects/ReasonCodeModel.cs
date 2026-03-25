namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    /// <summary>
    /// Practice Area model from ReasonCodeGridItem.cs
    /// </summary>
    public class ReasonCodeModel
    {
        /// <summary>
        /// Reason Code description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Reason Code status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Reason Code name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Override 'Equals' method
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True - if equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            var model = (ReasonCodeModel)obj;
            return this.Name.Equals(model.Name) && this.Description.Equals(model.Description)
                   && this.Status.Equals(model.Status);
        }

        /// <summary>
        /// Overriden 'GetHashCode' method
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}