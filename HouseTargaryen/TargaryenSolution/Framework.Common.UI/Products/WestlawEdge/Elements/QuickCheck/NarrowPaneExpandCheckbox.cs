namespace Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// Narrow pane expand arrow
    /// </summary>
    public sealed class NarrowPaneExpandCheckbox : CheckBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NarrowPaneExpandCheckbox"/> class. 
        /// Narrow pane toggle arrow
        /// </summary>
        /// <param name="locators">
        /// Locators
        /// </param>
        public NarrowPaneExpandCheckbox(params By[] locators) : base(locators)
        {
        }

        /// <summary>
        /// Is narrow pane expanded
        /// </summary>
        public bool Expanded => this.Text.Contains("Collapse Filters");

        /// <summary>
        /// Sets the state to the toggle arrow
        /// true = expanded
        /// false = collapsed
        /// </summary>
        /// <param name="state">State to be set</param>
        public override void Set(bool state)
        {
            if (this.Expanded != state)
            {
                this.GetContainer().Click();
            }
        }
    }
}