namespace Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch
{
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Topic Selections dialog
    /// </summary>
    public class TopicSelectionsDialog : BaseModuleRegressionDialog
    {
        private const string ItemLctMask = "//div[@id='co_facet_topic_popup']//li/label[text()={0}]";

        private static readonly By SelectButtonLocator = By.Id("co_facet_topic_filterButton");

        /// <summary>
        /// Item button
        /// </summary>
        public IButton ItemButton(string name) => new Button(SafeXpath.BySafeXpath(ItemLctMask, name));

        /// <summary>
        /// Select button
        /// </summary>
        public IButton SelectButton => new Button(SelectButtonLocator);

        /// <summary>
        /// The select items and apply.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="names"> The names. </param>
        /// <returns> New instance of the page </returns>
        public T SelectItemsAndApply<T>(params string[] names) where T : ICreatablePageObject
        {
            names.ToList().ForEach(name => ItemButton(name).Click());
            return this.SelectButton.Click<T>();
        }
    }
}