namespace Framework.Common.UI.Raw.WestlawEdge.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Various service-like methods utilized in page objects (or tests)
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Swaps the last and the first names of "Smith, John", trims spaces and returns the "normalized" Contact name of "John Smith"
        /// </summary>
        /// <param string="name">Contact name in format [LastName], [FirstName]</param>
        /// <returns>"normalized" contact name in format [FirstName] [LastName]</returns>
        /// TODO: consider creating a simple business object with respective fields and methods.
        public static string NormalizeContactName(string name) => $"{name.Split(',').Last().Trim()} {name.Split(',').First().Trim()}";

        /// <summary>
        /// Invokes NormalizeContactName method for each time in the list
        /// </summary>
        /// <param string="listOfNames"></param>
        /// <returns>list of normalized contact name</returns>
        public static IEnumerable<string> NormalizeListOfContactNames(IEnumerable<string> listOfNames) => listOfNames.Select(x => Utils.NormalizeContactName(x));
    }
}
