using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Pages;
using Framework.Common.UI.Products.WestLawAnalytics.Pages;
using java.sql;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Pages
{
    /// <summary>
    /// Advantage Sign Out page.
    /// </summary>
    public class AdvantageSignOffPage : CommonSignOffPage 
    {
        private static readonly By SignOutLabelLocator = By.XPath("//div[@class='co_signOff_message']/h1");
        private static readonly By ReturnToWlAdvantageLocator = By.XPath("//input[@id= 'coid_website_signBackOnButton']");
                
        /// <summary>
        /// Gets the sign out label.
        /// </summary>
        public ILabel SignOutLabel => new Label(SignOutLabelLocator);

        /// <summary>
        /// Return to Westlaw Advantage Button.
        /// </summary>
        public IButton ReturnToWlAdvantageButton => new Button(ReturnToWlAdvantageLocator);
    }
}