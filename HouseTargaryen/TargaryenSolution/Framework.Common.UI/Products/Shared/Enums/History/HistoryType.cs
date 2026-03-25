namespace Framework.Common.UI.Products.Shared.Enums.History
{
    /// <summary>
    /// History (Research) Type 
    /// Tab on the History page
    /// </summary>
    public enum HistoryType
    {
        /// <summary>
        /// All History
        /// Table columns: Event, Description, DateTime, ClientId
        /// </summary>
        AllHistory,

        /// <summary>
        /// Documents
        /// Table columns: CheckBox, Title, Content, DateTime, ClientId
        /// </summary>
        Documents,

        /// <summary>
        /// Searches
        /// Table columns: Description, DateTime, ClientId
        /// </summary>
        Searches
    }
}