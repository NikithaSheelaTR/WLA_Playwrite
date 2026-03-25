namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// NotesOfDecisionsFacet
    /// </summary>
    public class ReferencesInNotesOfDecisionsFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_HasNOD");

        private EnumPropertyMapper<NotesOfDecisions, WebElementInfo> notesOfDecisionsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private EnumPropertyMapper<NotesOfDecisions, WebElementInfo> NotesOfDecisionsMap =>
            this.notesOfDecisionsMap = this.notesOfDecisionsMap ?? EnumPropertyModelCache.GetMap<NotesOfDecisions, WebElementInfo>();

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="notesOfDecisions">The notesOfDecisions option.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(NotesOfDecisions notesOfDecisions)
            => this.GetCheckboxCount(this.NotesOfDecisionsMap[notesOfDecisions].LocatorString);

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="notesOfDecisions">The notesOfDecisions option</param>
        /// <returns>True if the facet is a checkbox</returns>
        public bool IsCheckboxDisplayed(NotesOfDecisions notesOfDecisions)
            => this.IsCheckboxDisplayed(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.NotesOfDecisionsMap[notesOfDecisions].LocatorString)));

        /// <summary>
        /// Verify that the given facet is of the checkbox type
        /// </summary>
        /// <param name="notesOfDecisions">The notesOfDecisions option</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckbox(NotesOfDecisions notesOfDecisions)
            => this.IsCheckbox(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.NotesOfDecisionsMap[notesOfDecisions].LocatorString)));

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="notesOfDecisions">The notesOfDecisions option</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(NotesOfDecisions notesOfDecisions)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.NotesOfDecisionsMap[notesOfDecisions].LocatorString)));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="notesOfDecisions">The notesOfDecisions option</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(NotesOfDecisions notesOfDecisions, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.NotesOfDecisionsMap[notesOfDecisions].LocatorString)), setTo);
    }
}