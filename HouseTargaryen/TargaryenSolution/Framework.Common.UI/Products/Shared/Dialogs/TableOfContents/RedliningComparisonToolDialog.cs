namespace Framework.Common.UI.Products.Shared.Dialogs.TableOfContents
{
    using Framework.Common.UI.Products.WestLawNext.Components.TabPanel;

    /// <summary>
    /// Redlining comparison tool dialog.
    /// </summary>
    public class RedliningComparisonToolDialog : BaseModuleRegressionDialog
    {     
        /// <summary>
        /// Tab Panel
        /// </summary>
        public RedlineComparisonToolTabPanel TabPanel { get; set; } = new RedlineComparisonToolTabPanel();
    }
}
