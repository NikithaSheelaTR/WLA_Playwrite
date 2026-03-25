namespace Framework.Common.UI.Products.ANZ.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// VolumeIssue Dialog for Volume/Issue Facet
    /// </summary>
    public class VolumeIssueDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string VolumeOptionLctMask = ".//label[contains(text(), '{0}')]";        

        private static readonly By AllAvailableOptions = By.XPath(".//div[contains(@class,'co_overlayBox_leftContent')]//li/label");

        private static readonly By AllSelectedOptions = By.XPath(".//div[contains(@class,'co_overlayBox_rightContent')]//li/div/button[1]");

        private static readonly By ContainerLocator = By.CssSelector(
            "#co_facet_publicationVolumeIssue_popup");        

        /// <summary>
        /// Volume/Issue Options List
        /// </summary>
        public IReadOnlyCollection<ILabel> VolumeIssueAllAvailableOptions => new ElementsCollection<Label>(this.Container, AllAvailableOptions);

        /// <summary>
        /// Volume/Issue Selected Options List
        /// </summary>
        public IReadOnlyCollection<ILabel> VolumeIssueAllSelectedOptions => new ElementsCollection<Label>(this.Container, AllSelectedOptions);

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container =>
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Select volume option from all available list options
        /// </summary>
        /// <param name="volume"> volume option  </param>
        /// <returns>Reference for VolumeIssueDialog</returns>
        public VolumeIssueDialog SelectVolumeOption(string volume)
        {
            DriverExtensions.GetElements(this.Container, By.XPath(string.Format(VolumeOptionLctMask, volume))).Last().Click();
            return this;
        }
    }
}
