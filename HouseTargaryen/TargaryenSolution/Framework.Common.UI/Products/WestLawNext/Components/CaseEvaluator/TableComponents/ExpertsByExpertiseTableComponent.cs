namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.TableComponents
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Reports;
    using Framework.Common.UI.Products.WestLawNext.Enums.Report;

    /// <summary>
    /// Represents the Experts By Expertise table on CE report page
    /// </summary>
    public class ExpertsByExpertiseTableComponent : AwardsAndExpertsBaseReportTableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpertsByExpertiseTableComponent"/> class.
        /// </summary>
        /// <param name="expandTableCompletely">The expand table completely.</param>
        public ExpertsByExpertiseTableComponent(bool expandTableCompletely = false)
            : base(ReportPageTables.ExpertsByExpertise)
        {
            if (expandTableCompletely)
            {
                this.ExpandTableCompletely();
            }
        }

        /// <summary>
        /// Gets the SortMenuExpertByExpertiseDropdown
        /// </summary>
        public SortMenuExpertByExpertiseDropdown Dropdown { get; } = new SortMenuExpertByExpertiseDropdown();

        /// <summary>
        /// Returns string list of column elements
        /// if headerName is Experts By expertise, sort by expertise (default) or by experts (expertise = false)
        /// </summary>
        /// <param name="headerName">The header Name.</param>
        /// <param name="expertise">The expertise.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public List<string> GetColumnAsStringList(ExpertsByExpertiseHeaders headerName, bool expertise = true)
            => this.GetColumnAsStringList((int)headerName, expertise);
    }
}