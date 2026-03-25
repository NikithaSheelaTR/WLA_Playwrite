using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.RelatedInfo
{
    /// <summary>
    /// The Co-cite Page
    /// </summary>
    public class CitedWithPage : BaseCoCitationsPage
    {
        private static readonly By PendoDoneButtonLocator = By.XPath("//*[contains(@class, '_pendo-button-primaryButton') or contains(@class, '_pendo-button')]");

        /// <summary>
        /// Constructor dismisses the Pendo guide if it appears
        /// </summary>
        public CitedWithPage()
        {
            var closePendoButton = DriverExtensions.SafeGetElement(PendoDoneButtonLocator);
            if (closePendoButton != null) closePendoButton.Click();
        }
    }
}
