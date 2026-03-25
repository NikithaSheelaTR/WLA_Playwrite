namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNextMobile.Components;

    /// <summary>
    /// MobileBasePageWithHeader
    /// </summary>
    public class MobileBasePageWithHeader : MobileBasePage
    {
        /// <summary>
        /// The Search header
        /// </summary>
        public MobileSearchHeaderComponent Header { get; } = new MobileSearchHeaderComponent();

        /// <summary>
        /// Changes the jurisdiction to All for state and federal
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T SetAllStateAndFederalJuris<T>() where T : ICreatablePageObject
        {
            var jurisPage = this.Header.ClickChangeJurisdictionLink<ChangeJurisdictionPage>();
            jurisPage.SelectStateAndFederal(Jurisdiction.AllStates, Jurisdiction.AllFederal);
            return jurisPage.ClickSaveButton<T>();
        }
    }
}
