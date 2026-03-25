namespace Framework.Common.UI.Products.WestLawNext.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    /// <summary>
    /// The base tab component.
    /// </summary>
    public abstract class BaseTabComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private const string TitleLocatorByNameLctMask = "//a[contains(@class,'co_tabLink') and contains(text(),{0})]";

        /// <summary>
        /// Tab title
        /// </summary>
        public virtual string Title => DriverExtensions.GetText(SafeXpath.BySafeXpath(TitleLocatorByNameLctMask, this.TabName));

        /// <summary>
        /// Gets the proper tab name.
        /// </summary>
        protected abstract string TabName { get; }
    }
}