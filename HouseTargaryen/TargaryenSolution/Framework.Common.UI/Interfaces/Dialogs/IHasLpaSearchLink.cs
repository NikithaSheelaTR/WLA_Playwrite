namespace Framework.Common.UI.Interfaces.Dialogs
{
    /// <summary>
    /// This interface is used for dialog that contains advanced search for items that are not offered in the list. 
    /// This specific search use FindLaw TR product 
    /// In facets it is represented as a links: Search.... and Start Over
    /// LPA means Legal Professional Authority for more information https://thehub.thomsonreuters.com/groups/findlaw-projects/projects/lpa
    /// </summary>
    public interface IHasLpaSearchLink
   {
        /// <summary>
        /// Enter text and click Legal Professional Authority link
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T EnterTextAndClickLpaSearchLink<T>(string itemName) where T : ICreatablePageObject;
   }
}
