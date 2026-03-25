namespace Framework.Common.UI.Products.WestLawNext.Pages.MigratedTax
{
    using Framework.Common.UI.Products.Shared.Components.RightPaneComponents;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;

    /// <summary>
    /// PWC Tax Page
    /// </summary>
    public class PwcTaxPage : CommonMigratedTaxPage
    {
        /// <summary>
        /// IRS Customized Libraries 
        /// </summary>
        public IrsCustomizedLibrariesComponent IrsCustomizedLibraries { get; set; } = new IrsCustomizedLibrariesComponent();

        /// <summary>
        /// PWC Customized Libraries
        /// </summary>
        public PwcCustomizedLibrariesComponent PwcCustomizedLibraries { get; set; } = new PwcCustomizedLibrariesComponent();
    }
}