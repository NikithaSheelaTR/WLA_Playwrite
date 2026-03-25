namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.RelatedInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.CitedWith;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using java.sql;

    using OpenQA.Selenium;

    using Button = Framework.Common.UI.Products.Shared.Elements.Buttons.Button;
    using CheckBox = Framework.Common.UI.Products.Shared.Elements.Checkboxes.CheckBox;
    using Label = Framework.Common.UI.Products.Shared.Elements.Labels.Label;

    /// <summary>
    /// The co-citing fly-out
    /// </summary>
    public class CoCitingDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.ClassName("Panel-close");
        private static readonly By CoCiteItemsLocator = By.ClassName("ResultItem");
        private static readonly By ContainerLocator = By.ClassName("Panel-right");
        private static readonly By ProximityFiltersLocator = By.ClassName("CoCites-Panel-proximity");
        private static readonly By IntroductorySignalsFilterLocator = By.ClassName("CoCites-Panel-introductorySignals-filter");
        private static readonly By IntroductorySignalsPanelLocator = By.XPath("//div[@class='CoCites-Panel-filtersApplied CoCites-Panel-introductorySignals']");
        private static readonly By TitleLocator = By.ClassName("Panel-heading"); 
        private static readonly By FullPageButtonLocator = By.XPath(".//a[text() = 'Full page']");
        private static readonly By SuccessMessageLocator = By.XPath("//div[@class = 'co_foldering_popupMessageContainer']//div[contains(@class, 'success')]");
        private static readonly By ItemsSelectedLabelLocator = By.XPath(".//div[contains(@class, 'SelectItem-nbrSelected')]");
        private static readonly By SelectAllCheckBoxLocator = By.XPath(".//label[text() = 'Select all items' ]/input");
        private static readonly By ClearSelectedLinkLocator = By.XPath(".//button[text() = 'Clear selected']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCitingDialog"/> class. 
        /// </summary>
        public CoCitingDialog() => DriverExtensions.WaitForElementDisplayed(TitleLocator);

        /// <summary>
        /// The toolbar
        /// </summary>
        public CoCitesToolbarComponent Toolbar => new CoCitesToolbarComponent(this.Container);

        /// <summary>
        /// Footer toolbar
        /// </summary>
        public CoCitesFooterToolbarComponent FooterToolbar => new CoCitesFooterToolbarComponent(ContainerLocator);

        /// <summary>
        /// Dialog title
        /// </summary>
        public ILabel Title => new Label(this.Container, TitleLocator);

        /// <summary>
        /// Success message
        /// </summary>
        public ILabel SuccessMessage => new Label(SuccessMessageLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);

        /// <summary>
        /// View full page button
        /// </summary>
        public IButton FullPageButton => new Button(this.Container, FullPageButtonLocator);

        /// <summary>
        /// Proximity filters label
        /// </summary>
        public ILabel ProximityFiltersLabel => new Label(this.Container, ProximityFiltersLocator);

        /// <summary>
        /// The I
        /// </summary>
        public ILabel IntroductorySignalsLabel => new Label(this.Container, IntroductorySignalsPanelLocator);

        /// <summary>
        /// Returns list of Introductory Signals applied
        /// </summary>
        public IList<string> GetAppliedIntroSignals()
        {
            DriverExtensions.WaitForElement(IntroductorySignalsFilterLocator);
            IReadOnlyCollection<IWebElement> elementList = DriverExtensions.GetElements(IntroductorySignalsFilterLocator);
            List<string> appliedFilterList = new List<string>();

            foreach (var element in elementList)
            {
                appliedFilterList.Add(element.Text);
            }

            return appliedFilterList;
        }

        /// <summary>
        /// List of co-cite items
        /// </summary>
        public ItemsCollection<CoCitationsItem> CoCiteList =>
            new ItemsCollection<CoCitationsItem>(this.Container, CoCiteItemsLocator);

        #region Select all items functionality

        /// <summary>
        /// Items Selected label
        /// </summary>
        public ILabel ItemsSelectedLabel => new Label(this.Container, ItemsSelectedLabelLocator);

        /// <summary>
        /// Select all checkbox
        /// </summary>
        public ICheckBox SelectAllResultsCheckBox => new CheckBox(this.Container, SelectAllCheckBoxLocator);

        /// <summary>
        /// Clear selected link
        /// </summary>
        public ILink ClearAllCheckboxesLink => new Link(this.Container, ClearSelectedLinkLocator);
        #endregion

        /// <summary>
        /// Container element
        /// </summary>
        private By Container => ContainerLocator;
    }
}