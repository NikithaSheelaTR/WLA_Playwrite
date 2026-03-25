namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.PreviousInteractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Items.PreviousInteractions;
    using Framework.Common.UI.Raw.WestlawEdge.Models.PreviousInteractions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Previously Annotated Dialog
    /// </summary>
    public class PreviouslyAnnotatedDialog : BasePreviouslyInteractionsDialog
    {
        private static readonly By ContainerLocator =
            By.XPath("//div[@id = 'co_previouslyAnnotated_info']");

        private static readonly By ItemLocator =
            By.XPath(".//li");

        private static readonly By DialogTitleLocator =
            By.XPath("//div[@id = 'co_previouslyAnnotated_info']/h4");

        private static readonly By ViewLinkLocator =
            By.XPath(".//a[contains(text(), 'View')]");

        private static readonly By GoToDocumentLinkLocator =
            By.XPath(".//a[contains(text(), 'Go to Document')]");

        /// <summary>
        /// Container
        /// </summary>
        private IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Does 'View' link exist at item index
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <returns> True if link exist, false otherwise </returns>
        public bool IsViewLinkDisplayedInItem(int itemIndex) => DriverExtensions.GetElements(this.Container, ViewLinkLocator).ElementAt(itemIndex).Displayed;

        /// <summary>
        /// Is 'Go to Document' link displayed at item index
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <returns> True if link exist, false otherwise </returns>
        public bool IsGoToDocumentLinkDisplayedInItem(int itemIndex) => DriverExtensions.GetElements(this.Container, GoToDocumentLinkLocator).ElementAt(itemIndex).Displayed;

        /// <summary>
        /// Does 'Annotations:' text exist in dialog
        /// </summary>
        /// <returns> True if 'Annotations:' text exists, false otherwise </returns>
        public bool IsDialogTitleCorrect() => DriverExtensions.IsDisplayed(DialogTitleLocator);

        /// <summary>
        /// Get 'Annotated Item' models 
        /// </summary>
        /// <returns>List of 'Annotated Item' models</returns>
        public List<AnnotatedModel> GetAnnotatedModels() =>
            DriverExtensions.GetElements(ContainerLocator, ItemLocator).Select(x => new AnnotatedItem(x))
                .Select(x => x.ToModel<AnnotatedModel>()).ToList();

        /// <summary>
        /// Is date on the dialog equal to current date
        /// </summary>
        /// <param name="date">
        /// Annotation date and time on dialog.
        /// </param>
        /// <returns>
        /// True if dates match, false otherwise 
        /// </returns>
        public bool IsDateCorrect(string date) =>
            DateTime.Now.Date.Equals(Convert.ToDateTime(date));

        /// <summary>
        /// Is time on the dialog match current time
        /// </summary>
        /// <param name="time">
        /// Annotation time on the dialog.
        /// </param>
        /// <returns>
        /// True if time match(difference not more than 2 minutes), false otherwise 
        /// </returns>
        public bool IsTimeCorrect(string time) =>
            DateTime.Now.Minute - Convert.ToDateTime(time).Minute < 2;

        /// <summary>
        /// Click 'View in Folder' link
        /// </summary>
        /// <typeparam name="T">Type of returned page</typeparam>
        /// <param name="index">Item index</param>
        /// <returns></returns>
        public T ClickOnViewLinkByIndex<T>(int index) where T: ICreatablePageObject => this.ClickElement<T>(DriverExtensions.GetElements(this.Container, ViewLinkLocator).ElementAt(index));

        /// <summary>
        /// Click 'Go to Document' link
        /// </summary>
        /// <typeparam name="T">Type of returned page</typeparam>
        /// <param name="index">Item index</param>
        /// <returns></returns>
        public T ClickGoToDocumentLinkByIndex<T>(int index) where T : ICreatablePageObject => this.ClickElement<T>(DriverExtensions.GetElements(this.Container,GoToDocumentLinkLocator).ElementAt(index));
    }
}
