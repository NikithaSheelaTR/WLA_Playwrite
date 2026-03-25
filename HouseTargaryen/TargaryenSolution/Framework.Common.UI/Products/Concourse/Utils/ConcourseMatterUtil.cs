namespace Framework.Common.UI.Products.Concourse.Utils
{
    using System;

    using Framework.Common.UI.Products.Concourse.Dialogs;
    using Framework.Common.UI.Products.Concourse.Pages;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;

    /// <summary>
    /// Class to create Data Setup needed for WLN-Concourse Regression
    /// </summary>
    public class ConcourseMatterUtil
    {
        /// <summary> Adding New Matter and adding permissions to specific matter </summary>
        /// <param name="matterName"> The matter Name. </param>
        /// <param name="collaboratorName"> The collaborator Name. </param>
        /// <param name="collaboratorRole"> The collaborator Role. </param>
        public void AddNewMatter(
            string matterName,
            string collaboratorName = "",
            SharingRoles collaboratorRole = SharingRoles.NotSpecified)
        {
            var concourseHomePage = DriverExtensions.CreatePageInstance<ConcourseHomePage>();
            var matterRoomPage = concourseHomePage.Header.SelectProduct<MatterRoomPage>("Matter Room");

            CreateMatterDialog createMatterDialog = matterRoomPage.ClickOnNewMatterButton();
            createMatterDialog.EnterMatterName(matterName);

            // Make this Matter to a Collaborative Matter
            createMatterDialog.SelectMatterAccess("Collaborative");

            // Adding permissions to created matter
            if (!string.IsNullOrWhiteSpace(collaboratorName) && collaboratorRole != SharingRoles.NotSpecified)
            {
                createMatterDialog.AddPermissions(collaboratorName, collaboratorRole);
            }

            MatterDetailsPage matterDetailsPage = createMatterDialog.ClickSaveButton();

            if (matterDetailsPage == null)
            {
                Logger.LogError($"Matter '{matterName}' already exists!");
                throw new Exception($"Matter '{matterName}' already exists exception");
            }
        }

        /// <summary>
        /// Close the given matters
        /// </summary>
        /// <param name="matterName"> The matter Name. </param>
        public void CloseMatter(string matterName)
        {
            EditMatterDialog editMatterDialog = new MatterDetailsPage().ClickOnEditMatterButton();
            editMatterDialog.CloseMatter();
        }

        /// <summary> This method is created for Lost Access WLN-Concourse Test </summary>
        /// <param name="matterName"> The matter Name. </param>
        /// <param name="participant"> The participant name. </param>
        public void RemovePermissionsOnMatter(string matterName, string participant)
        {
            // Go to Matter Room
            var concourseHomePage = DriverExtensions.CreatePageInstance<ConcourseHomePage>();
            var matterRoomPage = concourseHomePage.Header.SelectProduct<MatterRoomPage>("Matter Room");

            // Opening the matter. Assumption is this method is from Matter Room
            MattersPage mattersPage = matterRoomPage.ClickOnViewAllLink();
            MatterDetailsPage matterDetailsPage = mattersPage.ClickMatterByName(matterName);
            EditMatterDialog editMatterDialog = matterDetailsPage.ClickOnEditMatterButton();

            // Removing the permissions from matter
            editMatterDialog.RemoveParticipant(participant);

            // Saving the changes
            editMatterDialog.ClickOnSaveButton();
        }
    }
}