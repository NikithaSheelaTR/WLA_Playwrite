namespace Framework.Common.Api.Endpoints.CaseNoteBook.Utilities
{
    using Framework.Common.Api.Endpoints.CaseNoteBook.DataModel;
    using Framework.Common.Api.Endpoints.CaseNoteBook.DataModel.RequestBody;

    /// <summary>
    /// Class that contains function that builds the Query parameter value
    /// </summary>
    public static class QueryBuilder
    {
        /// <summary>
        /// The build query parameter value.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="profileType">
        /// The profile type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string BuildQueryParameterValue(CaseNotebookUrlCreatorRequest request, ProfileType profileType)
        {
            string querParamValue = string.Empty;

            if (request == null)
            {
                return querParamValue;
            }

            switch (profileType)
            {
                case ProfileType.Arbitrators:
                    querParamValue =
                        $"advanced: IND({QueryBuilder.GetFormattedFullName(request)}) & ORG({QueryBuilder.GetFormattedOrganization(request.Organization)}) & CSZ({request.City}) & ST((({request.State})))";
                    break;
                case ProfileType.AttorneyAndJudges:
                    querParamValue =
                        $"advanced: IND({QueryBuilder.GetFormattedFullName(request)}) & ORG({QueryBuilder.GetFormattedOrganization(request.Organization)}) & CY({request.City}) & ST((({request.State})))";
                    break;
                case ProfileType.Characters:
                    querParamValue =
                        $"advanced: NA({QueryBuilder.GetFormattedOrganization(request.Organization)}) & ADDR({request.Street}) & CITY({request.City}) & ST(({request.State}))";
                    break;
                case ProfileType.ExpertLibrary:
                    querParamValue =
                        $"advanced: IND({QueryBuilder.GetFormattedFullName(request)}) & CSZ({request.City}) & ST((({request.State})))";
                    break;
                case ProfileType.PeopleMap:
                    querParamValue =
                        $"advanced: NAME({request.FirstName}) & NA({request.LastName}) & CITY({request.City}) & ST({request.State}) & STR({request.Street})";
                    break;
            }

            return querParamValue;
        }

        /// <summary>
        /// Replaces blanks with /3
        /// </summary>
        /// <param name="p">organization string with blanks</param>
        /// <returns>organization string with blanks replaced with /3</returns>
        public static string GetFormattedOrganization(string p)
        {
            return p.Replace(" ", " /3 ");
        }

        /// <summary>
        /// The get formatted full name.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetFormattedFullName(CaseNotebookUrlCreatorRequest request)
        {
            return request.FirstName + " /3 " + request.LastName;
        }
    }
}