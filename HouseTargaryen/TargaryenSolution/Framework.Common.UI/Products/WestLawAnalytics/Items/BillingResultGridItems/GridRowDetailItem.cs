namespace Framework.Common.UI.Products.WestLawAnalytics.Items.BillingResultGridItems
{
    using System;
    using System.Text.RegularExpressions;
    using Framework.Common.UI.Interfaces.Models;
    using Framework.Common.UI.Products.WestLawAnalytics.Dialogs;
    using Framework.Common.UI.Utils.Mapper;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Presents detail info for expanded session
    /// </summary>
    public class GridRowDetailItem : IMappable
    {
        private const string ContainerMask = "//div[@id='co_billingInvestigationTransactionGrid_{0}']";

        private static readonly By IsChargeableLocator = By.XPath(".//*[@class='wa_historicalDataChargeable']");

        private static readonly By LastEditedByLocator = By.XPath(".//*[@class='wa_historicalDataLastEditedBy']");

        private static readonly By PracticeLabelLocator = By.XPath(".//*[@class='wa_historicalDataPracticeArea']");

        private static readonly By ReasonCodeLabelLocator = By.XPath(".//*[@class='wa_historicalDataReasonCode']");

        private static readonly By ResearchDescriptionLabelLocator =
            By.XPath(".//*[@class='wa_historicalDataResearchDescription']");

        private static readonly By ProductLabelLocator = By.XPath(".//*[@class='wa_historicalDataProduct']");

        private static readonly By EditSessionButtonLocator = By.XPath("//button[@class='wa_editSession']");

        private readonly By containerLocator; // Root container for an element

        private readonly string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridRowDetailItem"/> class.
        /// </summary>
        /// <param name="id"> correspondent div id for detail item </param>
        public GridRowDetailItem(string id)
        {
            this.id = new Regex("Row_(\\w*)").Match(id).Value.Replace("Row_", string.Empty);
            if (this.id.Equals(string.Empty))
            {
                throw new NoSuchElementException("Empty div id");
            }

            this.containerLocator = By.XPath(string.Format(ContainerMask, this.id));
        }

        /// <summary>
        /// Gets client id
        /// </summary>
        /// <returns>Client id</returns>
        public string GetClientMatter() => DriverExtensions.GetText(this.containerLocator);

        /// <summary>
        /// Returns formatted date string
        /// </summary>
        /// <returns> Formatted date, if it exists, default time otherwise </returns>
        public DateTime GetLastEditedDate()
            =>
                !string.IsNullOrEmpty(this.GetLastEditedBy())
                    ? DateTime.Parse(new Regex(" \\d*/\\d*/\\d*").Match(this.GetLastEditedBy()).Value)
                    : default(DateTime);

        /// <summary>
        /// Gets editors name and date
        /// </summary>
        /// <returns>editors name and time if records was edited, empty string otherwise </returns>
        public string GetLastEditedBy() => DriverExtensions.IsDisplayed(this.containerLocator, LastEditedByLocator) ? DriverExtensions.GetText(this.containerLocator, LastEditedByLocator) : string.Empty;

        /// <summary>
        /// Gets practice text
        /// </summary>
        /// <returns>Practice text</returns>
        public string GetPracticeArea() => DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.containerLocator), PracticeLabelLocator).Text;
        
        /// <summary>
        /// Gets reason of changing chargeable/non-chargeable
        /// </summary>
        /// <returns>Reason text</returns>
        public string GetReasonCode() => DriverExtensions.GetText(this.containerLocator, ReasonCodeLabelLocator);

        /// <summary>
        /// Get detailed description why session was edited
        /// </summary>
        /// <returns>Reason description text</returns>
        public string GetResearchDescription()
            => DriverExtensions.GetText(this.containerLocator, ResearchDescriptionLabelLocator);

        /// <summary>
        /// Get Product text
        /// </summary>
        /// <returns> Product text </returns>
        public string GetProduct() => DriverExtensions.GetText(this.containerLocator, ProductLabelLocator);

        /// <summary>
        /// Get Product text
        /// </summary>
        /// <returns> Product text </returns>
        public string GetChargeable() => DriverExtensions.GetText(this.containerLocator, IsChargeableLocator);
                
        /// <summary>
        /// Clicks on "Edit" Button
        /// </summary>
        /// <returns>
        /// The <see cref="EditBillingInformationDialog"/>.
        /// </returns>
        public EditBillingInformationDialog ClickEditButton()
        {
            DriverExtensions.WaitForElement(EditSessionButtonLocator).Click();
            return new EditBillingInformationDialog();
        }

        /// <summary>
        /// Verify that edit button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsEditButtonDisplayed() => DriverExtensions.IsDisplayed(EditSessionButtonLocator, 5);

        /// <summary>
        /// The to model.
        /// </summary>
        /// <typeparam name="TModel"> The type of model </typeparam>
        /// <returns> The <see cref="!:TModel" />. </returns>
        public TModel ToModel<TModel>() => MapperService.Map<TModel>(this);

        /// <summary>
        /// The to model.
        /// </summary>
        /// <param name="destinationType"> The destination type. </param>
        /// <returns> The <see cref="T:System.Object" />. </returns>
        public object ToModel(Type destinationType)
            => MapperService.Map(this, this.GetType(), destinationType);
    }
}