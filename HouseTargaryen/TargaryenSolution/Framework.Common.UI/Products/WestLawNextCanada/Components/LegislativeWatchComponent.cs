namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Contains Progress of Bills component tests of Westlaw Next Canada
    /// </summary>
    public class LegislativeWatchComponent : BaseAlertComponent
    {
        private static readonly By AlertIconButtonLocator = By.XPath("//span[contains(@class,'co_alertDropDown_bellIcon')]");
        private static readonly By ContentWidgetLocator = By.Id("co_mainContainer");
        private static readonly By JurisdictionAlertLinkLocator = By.XPath("//a[@id='crsw_jurisdictionAlertAnchor']");
        private static readonly By ListOfProgressOfBillsDocumentsLinkLocator = By.XPath("//ul[@id='co_tocItemLinks']//a[contains(@class,'co_tocItemLink')]");
        private static readonly By ListOfProgressOfBillsLinkLocator = By.XPath("//div[@class='co_categoryPageCheckBoxGroup']//li/a");        

        /// <summary>
        ///  Alert icon button
        /// </summary>
        public IButton AlertIconButton => new Button(this.ComponentLocator, AlertIconButtonLocator);

        /// <summary>
        /// Legislative Watch Component
        /// </summary>
        protected override By ComponentLocator => ContentWidgetLocator;

        /// <summary>
        ///  Jurisdiction alert link
        /// </summary>
        public ILink JurisdictionAlertLink => new Link(this.ComponentLocator, JurisdictionAlertLinkLocator);

        /// <summary>
        ///  Progress of bills links
        /// </summary>
        public IReadOnlyCollection<ILink> ProgressOfBillsLinks => new ElementsCollection<Link>(this.ComponentLocator, ListOfProgressOfBillsLinkLocator);

        /// <summary>
        ///  Progress of bills document links
        /// </summary>
        public IReadOnlyCollection<ILink> ProgressOfBillsDocumentLinks => new ElementsCollection<Link>(this.ComponentLocator, ListOfProgressOfBillsDocumentsLinkLocator);
    }
}
