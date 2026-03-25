namespace Framework.Common.UI.Raw.WestlawEdge.Utils.TrDiscover
{
    using System.Collections.Generic;

    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;

    /// <summary>
    /// The TrdSuggestionComparer class
    /// </summary>
    public class TrdSuggestionComparer : IComparer<TrdSuggestionModel>
    {
        private static Dictionary<string, int> EntityRank
            =>
            new Dictionary<string, int>()
            {
                { "Cases with the Key Number for", 1 },
                { "Cases heard by", 2 },
                { "Cases argued by", 3 },
                { "Cases represented by", 4 },
                { "Cases involving", 5 },
                { "Cases on", 6 }
            };

        private readonly string query;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        public TrdSuggestionComparer(string query)
        {
            this.query = query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(TrdSuggestionModel x, TrdSuggestionModel y)
        {
            int rankX = EntityRank[x.Predicate];
            int rankY = EntityRank[y.Predicate];

            if (x.Entity.Equals(this.query))
                rankX = 0;
            if (y.Entity.Equals(this.query))
                rankY = 0;
            return rankX == rankY ? 0 : rankX.CompareTo(rankY);
        }
    }
}
