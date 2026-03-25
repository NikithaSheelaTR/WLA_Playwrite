namespace Framework.Common.UI.Products.Shared.Models.EnumProperties
{
    using Framework.Core.DataModel;

    /// <summary>
    /// The date range options property model.
    /// </summary>
    public class DateRangeOptionsModel : BaseTextModel
    {
        /// <summary>
        /// the Id of the first field
        /// </summary>
        public string FirstFieldLocatorString { get; set; }

        /// <summary>
        /// the Id of the first error 
        /// </summary>
        public string FirstErrorLocatorString { get; set; }

        /// <summary>
        /// Locator String
        /// </summary>
        public string LocatorString { get; set; }

        /// <summary>
        /// the count Id value
        /// </summary>
        public string OptionCountLocator { get; set; }

        /// <summary>
        /// the count Id value
        /// </summary>
        public string OptionCountLocatorString { get; set; }

        /// <summary>
        /// the Id of the second field
        /// </summary>
        public string SecondFieldLocatorString { get; set; }

        /// <summary>
        /// the Id of the second error 
        /// </summary>
        public string SecondErrorLocatorString { get; set; }

        /// <summary>
        /// Ok Button 
        /// </summary>
        public string SubmitButtonLocatorString { get; set; }
    }
}