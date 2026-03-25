namespace Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// The original report expand checkbox.
    /// </summary>
    public class OriginalReportExpandCheckbox : CheckBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OriginalReportExpandCheckbox"/> class. 
        /// Original Report Expand Checkbox
        /// </summary>
        /// <param name="containerElement">
        /// Container
        /// </param>
        public OriginalReportExpandCheckbox(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Selected = expanded
        /// </summary>
        public override bool Selected => !this.GetContainer().Text.Contains("Show all");

        /// <summary>
        /// Expand original report component
        /// </summary>
        /// <param name="value">True = expand, False = collapse</param>
        public override void Set(bool value)
        {
            if (this.Selected != value)
            {
                this.GetContainer().Click();
            }
        }
    }
}