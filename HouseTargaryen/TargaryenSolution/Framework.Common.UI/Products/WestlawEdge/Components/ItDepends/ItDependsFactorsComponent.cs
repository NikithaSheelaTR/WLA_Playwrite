namespace Framework.Common.UI.Products.WestlawEdge.Components.ItDepends
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using Framework.Common.UI.Products.WestlawEdge.Enums.ItDepends;
    using Framework.Common.UI.Products.WestlawEdge.Items.ItDepends;

    /// <summary>
    /// Factors Component
    /// </summary>
    public class ItDependsFactorsComponent : BaseModuleRegressionComponent
    {
        private static readonly By FactorLocator = By.XPath("//table[contains(@class,'ItDepends-table')]//tbody/tr");
        private static readonly By ViewCasesButtonLocator = By.XPath("//*[@id='co_applyFactors']");
        private static readonly By SelectAllButtonLocator = By.XPath("//*[contains(@class,'co_select co_primaryBtn')]");
        private static readonly By PrecentageMessageLocator = By.XPath("//span[@id='co_itDepends_favorPercentage']");

        private static readonly By ContainerLocator = By.ClassName("ItDepends-factorsView");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private List<ItDependsFactorItem> FactorItemsList => this.GetFactorsItems();

        /// <summary>
        /// Get List of Factors
        /// </summary>
        public List<string> GetListOfFactors() => this
            .FactorItemsList.Select(item => item.FactorName).ToList<string>();

        /// <summary>
        /// Select Factor
        /// </summary>
        /// <param name="factor">Factor name</param>
        /// <param name="favor">Favor value</param>
        public void SetFactorFavor(string factor, ItDependsFactorFavorsEnum favor)
        {
            ItDependsFactorItem factorItem = this.FactorItemsList.Find(item => item.FactorName == factor);

            switch (favor)
            {
                case ItDependsFactorFavorsEnum.Positive:
                    factorItem.SetPositiveFavor();
                    break;
                case ItDependsFactorFavorsEnum.Negative:
                    factorItem.SetNegativeFavor();
                    break;
                default:
                    factorItem.SetNeutralFavor();
                    break;
            }
        }

        /// <summary>
        /// Select All Factors
        /// </summary>
        public void ClickSelectAllFactorsButton() => DriverExtensions.Click(SelectAllButtonLocator);

        /// <summary>
        /// Click View cases button
        /// </summary>
        public void ClickViewCasesButton()
        {
            DriverExtensions.Click(ViewCasesButtonLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verify if View Cases button is enabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsViewCasesButtonEnabled() => !DriverExtensions
                                                      .GetElement(ViewCasesButtonLocator).GetAttribute("class")
                                                      .Contains("co_disabled");

        /// <summary>
        /// Get Matching cases message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetPrecentageMessageText() => DriverExtensions.GetText(PrecentageMessageLocator).Trim();

        /// <summary>
        /// Get factors list
        /// </summary>
        private List<ItDependsFactorItem> GetFactorsItems() => DriverExtensions
            .GetElements(FactorLocator).Select(item => new ItDependsFactorItem(item)).ToList();
    }
}
