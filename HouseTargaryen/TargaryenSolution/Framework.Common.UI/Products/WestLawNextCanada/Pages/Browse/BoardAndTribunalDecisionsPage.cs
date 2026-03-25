namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Browse
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages.Browse;

    using OpenQA.Selenium;

    /// <summary>
    /// Board And Tribunal Decisions Category Page
    /// </summary>
    public class BoardAndTribunalDecisionsPage : CheckboxBrowsePage
    {
        private static readonly By MoreInfoTribunalLocator = By.Id("co_moreInfoTribunalLink");
        
        private static readonly By TribunalsWarningMessage =
            By.XPath("//div[@role='tooltip']//div[@class='co_infoBox_message']");

        /// <summary>
        /// Search Selected Tribunals Across Jurisdictions Link
        /// </summary>
        public ILink MoreInfoTribunalLink = new Link(MoreInfoTribunalLocator);

        /// <summary>
        /// Returns Maximum 20 Tribunals Reached Warning Message Text
        /// </summary>
        public ILabel TribunalsWarning => new Label(TribunalsWarningMessage);

        /// <summary>
        /// Clicks on 21 Tribunals and checks if warning message is displayed
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsMaxTribunalsWarningMessageDisplayed(List<string> list)
        {
            try
            {
                for (int i = 1; i <= 21; i++)
                {
                    CheckboxComponent.SelectCheckbox<CheckboxBrowsePage>(
                        list[i].Replace(" ", string.Empty).Replace("&", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty));
                }
            }
            catch (ElementClickInterceptedException)
            {
                return true;
            }

            return false;
        }
    }
}
