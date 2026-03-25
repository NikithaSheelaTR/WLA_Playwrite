
namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using Framework.Common.UI.Products.Shared.Components.GraphDialog;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using javax.print.attribute.standard;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe dialog for Graphs Facet
    /// </summary>
    public class GraphsDialog : BaseModuleRegressionDialog
    {
        private static readonly By GraphsDialogTitleLocator = By.XPath("//h3[contains(@id,'coid_lightboxAriaLabel_')]");

        private static readonly By CancelButtonLocator = By.Id("co_graphCancelButton");

        private static readonly By TextAreaLocator = By.Id("coid_title");

        private EnumPropertyMapper<Dialogs, WebElementInfo> dialogsMap;

        /// <summary>
        /// Title component
        /// </summary>
        public ChartDisplayOptionsComponent Title => new ChartDisplayOptionsComponent("Title");

        /// <summary>
        /// View component
        /// </summary>
        public ChartDisplayOptionsComponent View => new ChartDisplayOptionsComponent("View");

        /// <summary>
        /// Legend component
        /// </summary>
        public ChartDisplayOptionsComponent Legend => new ChartDisplayOptionsComponent("Legend");

        /// <summary>
        /// Show Other component
        /// </summary>
        public ChartDisplayOptionsComponent ShowOther => new ChartDisplayOptionsComponent("Show Other");

        /// <summary>
        /// Node Points component
        /// </summary>
        public ChartDisplayOptionsComponent NodePoints => new ChartDisplayOptionsComponent("Node Points");

        /// <summary>
        /// Graph data component
        /// </summary>
        public GraphDataComponent GraphData => new GraphDataComponent();
        
        /// <summary>
        /// Chart Type component
        /// </summary>
        public TypeComponent Type => new TypeComponent();

        /// <summary>
        /// Chart component
        /// </summary>
        public ChartComponent Chart => new ChartComponent();

        /// <summary>
        /// Dialogs  Map
        /// </summary>
        public EnumPropertyMapper<Dialogs, WebElementInfo> DialogsMap
            => this.dialogsMap = this.dialogsMap ?? EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>();

        /// <summary>
        /// Get Graph dialog title text
        /// </summary>
        /// <returns> Graph dialog title text </returns>
        public string GetGraphDialogTitle() => DriverExtensions.GetText(GraphsDialogTitleLocator);

        /// <summary>
        /// Close Graphs dialog
        /// </summary>
        /// <returns> Graph dialog title text </returns>
        public void CloseGraphsDialog() => this.ClickElement(CancelButtonLocator);

        /// <summary>
        /// Close Graphs dialog
        /// </summary>
        /// <returns> Graph dialog title text </returns>
        public bool IsTextAreaEnabled() => DriverExtensions.GetElement(TextAreaLocator).Enabled;

        /// <summary>
        /// Verify that dialog is displayed
        /// </summary>
        /// <returns> True if dialog is displayed, false otherwise </returns>
        public bool IsChartPopUpDisplayed() =>
            DriverExtensions.IsDisplayed(
                By.XPath(this.DialogsMap[Dialogs.Graphs].LocatorString),
                By.XPath(this.DialogsMap[Dialogs.ChartsPopUpDialog].LocatorString));
    }
}
