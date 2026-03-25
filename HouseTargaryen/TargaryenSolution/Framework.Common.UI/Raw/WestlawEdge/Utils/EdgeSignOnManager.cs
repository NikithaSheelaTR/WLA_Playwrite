namespace Framework.Common.UI.Raw.WestlawEdge.Utils
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Core.Utils.Execution;
   
    /// <summary>
    /// The sign-on manager for Westlaw Indigo.
    /// </summary>
    public class EdgeSignOnManager : WestlawSignOnManager
    {
         /// <summary>
        /// Signs off of Westlaw.
        /// </summary>
        /// <returns>The <see cref="ICreatablePageObject"/>.</returns>
        public override ICreatablePageObject SignOff()
        {           
                SafeMethodExecutor.Execute(
                     () =>
                         new EdgeHeaderComponent()
                        .ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickSignOff()).LogDetails();

            if (new CommonSignOffPage().ForcesignoffLink.Displayed)
            {
                new CommonSignOffPage().ForcesignoffLink.Click();
            }

            return new CommonSignOffPage();
        }
     }
}
