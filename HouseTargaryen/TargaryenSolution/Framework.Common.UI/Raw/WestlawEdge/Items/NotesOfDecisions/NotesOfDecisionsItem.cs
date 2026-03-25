namespace Framework.Common.UI.Raw.WestlawEdge.Items.NotesOfDecisions
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The notes of decisions item.
    /// </summary>
    public class NotesOfDecisionsItem : BaseItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//input[@type='checkbox']");

        private static readonly By HeadingLocator = By.XPath(".//h4");

        private static readonly By TextLocator = By.XPath("./div");

        private static readonly By SubNodLocator = By.XPath(".//p[@class = 'noteText']");

        private static readonly By KeyNumberIconLocator = By.XPath(".//img[@class='TLRkey']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="NotesOfDecisionsItem"/> class. 
        /// </summary>
        /// <param name="nodItemContainer"> The Experiment Item Container. </param>
        public NotesOfDecisionsItem(IWebElement nodItemContainer): base(nodItemContainer)
        {
        }

        /// <summary>
        /// Verifies that the checkbox is selected.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if checkbox is selected. </returns>
        public bool IsCheckboxSelected => DriverExtensions.IsCheckboxSelected(this.Container, CheckboxLocator);

        /// <summary>
        /// Gets NOD Heading.
        /// </summary>
        /// <returns> NOD Heading </returns>
        public string NodHeading => DriverExtensions.SafeGetElement(this.Container, HeadingLocator)
                                                    ?.Text.Split(new[] { "\r\n" }, StringSplitOptions.None)
                                                    .Last() ?? "No usual heading";

        /// <summary>
        /// Gets NOD text.
        /// </summary>
        /// <returns> Experiment Type </returns>
        public string NodText => DriverExtensions.SafeGetElement(this.Container, TextLocator)?.Text ?? "Root heading";

        /// <summary>
        /// Get NOD subitem.
        /// </summary>
        /// <returns> Experiment Type </returns>
        public int NodSubItemCount => DriverExtensions.GetElements(this.Container, SubNodLocator).Count;

        /// <summary>
        /// Get count of key number icons for item if any
        /// </summary>
        /// <returns>
        /// Get count of key number icons for item
        /// </returns>
        public int NodKeyNumberIconCount => DriverExtensions.GetElements(this.Container, KeyNumberIconLocator).Count;
       
        /// <summary>
        /// Checks NOD checkbox.
        /// </summary>
        /// <param name="selected"> The selected. </param>
        /// <returns> The <see cref="EdgeExperimentAdministrationPage"/>.  </returns>
        public EdgeNotesOfDecisionsPage SelectNodCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, this.Container, CheckboxLocator);
            return new EdgeNotesOfDecisionsPage();
        }
    }
}
