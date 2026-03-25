namespace Framework.Common.UI.Raw.WestlawEdge.Utils.TrDiscover
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.TRD;

    /// <summary>
    /// The TrdSuggesterUtilities class
    /// </summary>
    public static class TrdSuggesterUtilities
    {
        /// <summary>
        /// The suggestion 
        /// </summary>
        public static string Suggestion { get; private set; } = string.Empty;

        private static Dictionary<TrdAttributesEnum, string> EntityPredicates => new Dictionary<TrdAttributesEnum, string>()
        {
            {TrdAttributesEnum.Topic, "on the topic of"},
            {TrdAttributesEnum.Judge, "heard by"},
            {TrdAttributesEnum.Attorney, "argued by"},
            {TrdAttributesEnum.Party, "involving"},
            {TrdAttributesEnum.Court, "decided in"},
            {TrdAttributesEnum.LawFirm, "represented by"},
            {TrdAttributesEnum.PracticeArea, "on the practice area of"},
            {TrdAttributesEnum.Key, "with the Key Number for"},
            {TrdAttributesEnum.Date, "on"},
            {TrdAttributesEnum.Reported, "which are"}
        };

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="type">Trd attribute type</param>
        /// <param name="value">value</param>
        public static void AddEntity(TrdAttributesEnum type, string value)
        {
            if (Suggestion.Length == 0)
            {
                Suggestion = "Cases";
            }

            Suggestion += $" {EntityPredicates[type]} {value}";
        }
        
        /// <summary>
        /// Clears suggestion 
        /// </summary>
        public static void ClearSuggestion()
        {
            Suggestion = string.Empty;
        }

        /// <summary>
        /// Gets key number nodes 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<string> GetKeyNumberNodes(string entity) => entity.Split('/').Select(node => node.Trim()).ToList();

    }
}