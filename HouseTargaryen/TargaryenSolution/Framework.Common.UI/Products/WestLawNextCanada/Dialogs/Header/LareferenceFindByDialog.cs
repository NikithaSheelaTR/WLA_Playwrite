namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.Header
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Lareference Find By dialog
    /// </summary>
    public class LareferenceFindByDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("co_findLightbox");
        private static readonly By FindByHeadingLabelLocators = By.ClassName("co_search_advancedSearchFormHeading");

        private EnumPropertyMapper<LarefFindByInputOptions, WebElementInfo> larefFindByOptionMapper;
        private EnumPropertyMapper<LarefFindBySearchButtons, WebElementInfo> larefFindByButtonMapper;

        /// <summary>
        /// Laref Find By Input Options Mapper
        /// </summary>
        protected EnumPropertyMapper<LarefFindByInputOptions, WebElementInfo> LarefFindByInputOptionMapper =>
           this.larefFindByOptionMapper = this.larefFindByOptionMapper
                                  ?? EnumPropertyModelCache
                                      .GetMap<LarefFindByInputOptions, WebElementInfo>(
                                          string.Empty,
                                          @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        ///Laref Find By buttons Mapper
        /// </summary>
        protected EnumPropertyMapper<LarefFindBySearchButtons, WebElementInfo> LarefFindByButtonMapper =>
           this.larefFindByButtonMapper = this.larefFindByButtonMapper
                                  ?? EnumPropertyModelCache
                                      .GetMap<LarefFindBySearchButtons, WebElementInfo>(
                                          string.Empty,
                                          @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Find By Heading Labels
        /// </summary>
        public IReadOnlyCollection<ILabel> FindByHeadingLabels => new ElementsCollection<Label>(ContainerLocator, FindByHeadingLabelLocators);

        /// <summary>
        /// Method to find out if the Laref Find By Input Options are present
        /// </summary>
        /// <param name="options">Options to check</param>
        /// <returns>True if all options are present</returns>
        public bool AreLaRefFindByInputOptionsPresent(params LarefFindByInputOptions[] options) =>
            options.All(option => DriverExtensions.IsDisplayed(
                By.Id(this.LarefFindByInputOptionMapper[option].Id)));

        /// <summary>
        /// Method to check if Laref Find By Buttons displayed
        /// </summary>
        /// <param name="buttons">Button options to verify</param>
        /// <returns></returns>
        public bool AreLarefFindByButtonsPresent(params LarefFindBySearchButtons[] buttons) =>
            buttons.All(button => DriverExtensions.IsDisplayed(
                By.Id(this.LarefFindByButtonMapper[button].Id)));

        /// <summary>
        /// Enter text in Laref Find By Input Option
        /// </summary>
        /// <param name="option">Laref input option to send the query</param>
        /// <param name="query">query to search</param>
        public void EnterTextInLarefFindByInputOption(LarefFindByInputOptions option, string query) =>
            DriverExtensions.GetElement(By.Id(this.LarefFindByInputOptionMapper[option].Id)).SendKeys(query);

        /// <summary>
        /// Click search button
        /// </summary>
        /// <typeparam name="T">Page instance to create</typeparam>
        /// <param name="button">Find by search button to click</param>
        /// <returns></returns>
        public T ClickSearchButton<T>(LarefFindBySearchButtons button) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.Id(this.LarefFindByButtonMapper[button].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}