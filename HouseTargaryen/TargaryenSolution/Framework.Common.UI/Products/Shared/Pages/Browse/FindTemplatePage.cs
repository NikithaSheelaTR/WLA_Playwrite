namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    /// <summary>
    /// FindTemplatePage
    /// </summary>
    public class FindTemplatePage : CommonAuthenticatedWestlawNextPage, IBrowseCategoryPage
    {
        private static readonly string SafeFindTemplateEndPartIdLocator = "substring(@id, string-length(@id) - 1)";

        private static readonly string SafeFindTemplateGoButtonLocator = "//li[./label[text()={0}]]/a";

        private static readonly string SafeFindTemplateInputLocator =
            "//li[./label[text()={0}]]/input[@type='text' and " + SafeFindTemplateEndPartIdLocator + " = {1} or "
            + SafeFindTemplateEndPartIdLocator + " = {2}]";

        /// <summary>
        /// EnterAndApplyFormText
        /// </summary>
        /// <param name="labelName">labelName</param>
        /// <param name="valuesInput">valuesInput</param>
        /// <typeparam name="T">t</typeparam>
        /// <returns>T</returns>
        public T EnterAndApplyFormText<T>(string labelName, params string[] valuesInput) where T : ICreatablePageObject
        {
            int findableInputId = 0;
            const int ExtraFindableInputId = 1;
            const string PartOfFindableId = "_";
            for (int i = 0; i < valuesInput.Length; i++)
            {
                DriverExtensions.SetTextField(
                    valuesInput[i],
                    SafeXpath.BySafeXpath(
                        SafeFindTemplateInputLocator,
                        labelName,
                        PartOfFindableId + (i + findableInputId),
                        PartOfFindableId + (i + ExtraFindableInputId + findableInputId)));
                findableInputId++;
            }

            DriverExtensions.GetElement(SafeXpath.BySafeXpath(SafeFindTemplateGoButtonLocator, labelName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}