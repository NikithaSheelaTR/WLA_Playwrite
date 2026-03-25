namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Know Your Judge Refine Analysis component
    /// </summary>
    public class KnowYourJudgeRefineAnalysisComponent : BaseModuleRegressionComponent
    {
        private static readonly By RefineAnalysisContainerLocator = By.XPath("//div[contains(@class,'refinerContainer')]");
        private static readonly By RefineAnalysisTitleLabelLocator = By.XPath("//h3[contains(@class, 'ReportRefiner')]");
        private static readonly By ContinueButtonLocator = By.XPath("//saf-button-v3[@data-testid='continue-button']");
        private static readonly By GoBackButtonLocator = By.XPath("//saf-button-v3[@data-testid='go-back-button']");
        private static readonly By ProgressBarLabelLocator = By.XPath("//saf-progress-ring-v3[@data-testid='continue-spinner']");
        private static readonly By ClearSelectionButtonLocator = By.XPath("//div[contains(@class, 'ReportRefiner-module')]//saf-button-v3[@saf='button']");
        private static readonly By CheckBoxSelectionTextLocator = By.XPath("//saf-alert-v3[@data-testid='report-error-alert']");
        private static readonly By SectionHeaderLabelLocator = By.XPath("//legend[contains(@class, 'ReportRefiner-module__optionGroupLabel')]");
        private static readonly By CheckBoxGroupLocator = By.XPath("//div[contains(@class, 'ReportRefiner-module__checkboxGroup')]");
        private static readonly By CheckBoxLocator = By.XPath(".//saf-checkbox-v3[@saf='checkbox']");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('input'));";
        private static readonly By ExpandAnalysisSwitchButtonLocator = By.XPath("//div[contains(@class, 'expandToggleContainer')]//saf-switch-v3[@saf='switch']");

        /// <summary>
        /// Continue button
        /// </summary>
        public IButton ContinueButton => new Button(ComponentLocator, ContinueButtonLocator);

        /// <summary>
        /// Progress bar label
        /// </summary>
        public ILabel ProgressBarLabel => new Label(ProgressBarLabelLocator);

        /// <summary>
        /// Go back button
        /// </summary>
        public IButton GoBackButton => new Button(ComponentLocator, GoBackButtonLocator);

        /// <summary>
        /// Clear selection button
        /// </summary>
        public IButton ClearSelectionButton => new Button(ComponentLocator, ClearSelectionButtonLocator);

        /// <summary>
        /// CheckBox selection text
        /// </summary>
        public ILabel CheckBoxSelectionText => new Label(ComponentLocator, CheckBoxSelectionTextLocator);

        /// <summary>
        /// Section Header labels
        /// </summary>
        public IReadOnlyCollection<ILabel> SectionHeaderLabels => new ElementsCollection<Label>(SectionHeaderLabelLocator);

        /// <summary>
        /// Expand Analysis switch button
        /// </summary>
        public IButton ExpandAnalysisSwitchButton => new Button(ComponentLocator, ExpandAnalysisSwitchButtonLocator);
        
        /// <summary>
        /// Refine Analysis Title
        /// </summary>
        public string RefineAnalysisTitle => DriverExtensions.GetElement(ComponentLocator, RefineAnalysisTitleLabelLocator).Text;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => RefineAnalysisContainerLocator;

        /// <summary>
        /// select checkbox
        /// </summary>
        /// <param name="options"> The checkbox options to select </param>
        public void selectCheckbox(List<string> options)
        {            
                IEnumerable<IWebElement> CheckBoxes = DriverExtensions.GetElements(ComponentLocator, CheckBoxGroupLocator)
                .SelectMany(group => group.FindElements(CheckBoxLocator));
                foreach (IWebElement checkbox in CheckBoxes) 
                { 
                    if (checkbox!= null & options.Any(opt => checkbox.Text.Contains(opt)))
                    {
                        IWebElement checkboxInput = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, checkbox);
                        checkboxInput.Click();
                    }
                }
            
        }


    }
}
