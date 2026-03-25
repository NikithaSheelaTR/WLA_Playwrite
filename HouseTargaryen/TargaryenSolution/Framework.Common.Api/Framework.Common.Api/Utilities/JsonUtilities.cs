namespace Framework.Common.Api.Utilities
{
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    /// <summary>
    /// The json utilities.
    /// </summary>
    public static class JsonUtilities
    {
        /// <summary>
        /// This method is good for converting Json into a simple nested dictionary object.
        /// </summary>
        /// <param name="jsonString">Json string to create a nested dictionary from.</param>
        /// <returns>A Dictionary object with the JSON values inside</returns>
        public static Dictionary<string, object> DeserializeFromJsonStringToDictionaryObject(string jsonString)
        {
            var jss = new JavaScriptSerializer();
            object o = jss.DeserializeObject(jsonString);

            if (o.GetType().Name == "Object[]")
            {
                return (Dictionary<string, object>)((object[])o)[0];
            }

            return (Dictionary<string, object>)o;
        }

        /// <summary>
        /// The deserialize json array.
        /// </summary>
        /// <param name="jsonString">
        /// The json string.
        /// </param>
        /// <returns>
        /// The <see cref="List{Dictionary}"/>.
        /// </returns>
        public static List<Dictionary<string, object>> DeserializeJsonArray(string jsonString)
        {
            var returnVal = new List<Dictionary<string, object>>();
            var jss = new JavaScriptSerializer();
            object o = jss.DeserializeObject(jsonString);

            if (o.GetType().Name == "Object[]")
            {
                var arr = (object[])o;

                foreach (object a in arr)
                {
                    returnVal.Add((Dictionary<string, object>)a);
                }

                return returnVal;
            }

            return null;
        }
    }
}