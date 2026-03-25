namespace Framework.Common.UI.Products.WestlawNextCorrectional.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Correctional Specific Common Browse Page
    /// </summary>
    public class CorrectionalBrowsePage : CommonBrowsePage
    {
        private static readonly By SearchAllContentRadioButtonLocator = By.XPath("//input[@id='coid_browseHideCheckboxes']");
        private static readonly By SpecifyContentRadioButtonLocator = By.XPath("//input[@id='coid_browseShowCheckboxes']");
        private static readonly By CorrectionalCategoryPageLinksLocator = By.XPath("//div[@class='co_categoryPageCheckBoxGroup']//a");
        private static readonly By UnreportedJudgementsLinksLocator = By.XPath("//*[@id='co_browseItemHeading_Unreported Judgments']//following-sibling::div//a");
        private static readonly By UnreportedJudgementsHeaderLocator = By.Id("co_browseItemHeading_Unreported Judgments");

        /// <summary>
        /// Search All Content RadioButton
        /// </summary>
        public IRadiobutton SearchAllContentRadioButton { get; } = new Radiobutton(SearchAllContentRadioButtonLocator);

        /// <summary>
        /// Specify Content RadioButton
        /// </summary>
        public IRadiobutton SpecifyContentRadioButton { get; } = new Radiobutton(SpecifyContentRadioButtonLocator);

        /// <summary>
        /// Correctional Category Page Links
        /// </summary>
        public IReadOnlyCollection<ILink> CorrectionalCategoryPageLinks { get; } = new ElementsCollection<Link>(CorrectionalCategoryPageLinksLocator);

        /// <summary>
        /// Unreported Judgements Links
        /// </summary>
        public IReadOnlyCollection<ILink> UnreportedJudgementsLinks { get; } = new ElementsCollection<Link>(UnreportedJudgementsLinksLocator);

        /// <summary>
        /// Unreported Judgements Header
        /// </summary>
        public ILabel UnreportedJudgementsHeader { get; } = new Label(UnreportedJudgementsHeaderLocator);
    }
}
