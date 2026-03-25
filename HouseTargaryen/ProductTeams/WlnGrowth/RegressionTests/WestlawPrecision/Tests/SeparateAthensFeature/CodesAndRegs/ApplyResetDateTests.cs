namespace WestlawPrecision.Tests.SeparateAthensFeature.CodesAndRegs
{
    using System;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ApplyResetDateTests : CodesAndRegsBaseTest
    {
        protected const string FeatureTestCategory = "ApplyResetDates";

        /// <summary>
        /// Test version advisory message and apply/reset functions (Stories 1757020, 1757117 and bug 1788357).
        /// 1. Sign in WL Edge and set jurisdiction to all states and federal (IAC-TOC-STATUTES-FUTURE-DATE)
        /// 2. Navigate to page: Content types->Regulations->Code of Federal Regulations (CFR)
        /// 3. Check: Versioning advisory message is not displayed on Regulations page
        /// 4. Check: Default effective date is empty on Regulations page
        /// 5. Enter today's date in format MM/dd/yyyy
        /// 6. Check: Entering current date enables Apply button
        /// 7. Go to home page and navigate to: Content types->Statutes->United States Code Annotated (USCA)
        /// 8. Check: Versioning advisory message is not displayed on Statutes page
        /// 9. Check: Default effective date is empty on Statutes page
        /// 10.Type in the next day date in format MM/dd/yyyy and click Apply button
        /// 11.Check: Effective date field has the correct value
        /// 12.Check: Reset button is enabled after applying future date
        /// 13.Click Reset button to clear out the effective date field
        /// 14.Check: Effective date field is empty after resetting
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void ApplyResetDateTest()
        {
            const string RegulationsPageUri = @"/Browse/Home/Regulations?transitionType=Default&contextData=(sc.Default)";
            const string StatutesPageUri = @"/Browse/Home/StatutesCourtRules?transitionType=Default&contextData=(sc.Default)";
            const string RegulationsPage = "Code of Federal Regulations (CFR)";
            const string StatutesPage = "United States Code Annotated (USCA)";
            string futureDate = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
            string currentDate = DateTime.Today.ToString("MM/dd/yyyy");

            this.GetHomePage<EdgeHomePage>().Header.OpenJurisdictionDialog()
                .SelectJurisdictions(true, Jurisdiction.AllStates, Jurisdiction.AllFederal).ClickSelectButton<CommonSearchHomePage>();

            var regulationsPage = NavigateToBrowsePage(RegulationsPageUri).ClickCategory<TableOfContentsBrowsePage>(RegulationsPage);

            this.TestCaseVerify.IsFalse(
                "Verify versioning advisory message is not displayed on Regulations page",
                regulationsPage.EffectiveDateComponent.VersioningAdvisoryLabel.Displayed,
                "Versioning advisory message should be removed on Regulations page");

            this.TestCaseVerify.IsTrue(
                "Verify default effective date is empty on Regulations page",
                regulationsPage.EffectiveDateComponent.EffectiveDateText.Length == 0,
                "Default effective date should be empty on Regulations page");

            regulationsPage.EffectiveDateComponent.EnterEffectiveDate(currentDate);

            this.TestCaseVerify.IsTrue(
                "Verify entering current date enables Apply button",
                regulationsPage.EffectiveDateComponent.ApplyButton.Enabled,
                "Entering current date should enable Apply button");

            var statutesPage = NavigateToBrowsePage(StatutesPageUri).ClickCategory<TableOfContentsBrowsePage>(StatutesPage);

            this.TestCaseVerify.IsFalse(
                "Verify versioning advisory message is not displayed on Statutes page",
                statutesPage.EffectiveDateComponent.VersioningAdvisoryLabel.Displayed,
                "Versioning advisory message should be removed on Statutes page");

            this.TestCaseVerify.IsTrue(
                "Verify default effective date is empty on Statutes page",
                regulationsPage.EffectiveDateComponent.EffectiveDateText.Length == 0,
                "Default effective date should be empty on Statutes page");

            statutesPage = statutesPage.EffectiveDateComponent.SetNewEffectiveDate<TableOfContentsBrowsePage>(futureDate);

            this.TestCaseVerify.AreEqual(
                "Verify effective date displays the expected applied date",
                regulationsPage.EffectiveDateComponent.EffectiveDateText,
                futureDate,
                "Expected date is not set correctly");

            this.TestCaseVerify.IsTrue(
                "Verify applying future date enables Reset button",
                statutesPage.EffectiveDateComponent.ResetButton.Enabled,
                "Applying future date should allow user to reset changes");
            
            statutesPage = statutesPage.EffectiveDateComponent.ResetButton.Click<TableOfContentsBrowsePage>();

            this.TestCaseVerify.IsTrue(
                "Verify clicking Reset sets effective date to empty",
                statutesPage.EffectiveDateComponent.EffectiveDateText.Length == 0,
                "Clicking Reset should set effective date to empty");
        }

        private EdgeCommonBrowsePage NavigateToBrowsePage(string pageUri)
        {
            string WlnSite = "https://1.next.";
            const string Domain = ".westlaw.com";
            string environment = this.TestExecutionContext.TestEnvironment.Id.ToString().ToLower();
            if (environment.EndsWith("aws"))
            {
                WlnSite = "https://region-use1.next.";
                environment = environment.Replace("aws", "");
            }
                
            string browsePageUrl = $"{WlnSite}{environment}{Domain}{pageUri}";
            return BrowserPool.CurrentBrowser.GoToUrl<EdgeCommonBrowsePage>(browsePageUrl);
        }
    }
}
