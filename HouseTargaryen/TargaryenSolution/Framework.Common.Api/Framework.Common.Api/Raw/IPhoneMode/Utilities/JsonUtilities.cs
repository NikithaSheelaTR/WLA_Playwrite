namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    /// <summary>
    /// is used to de serialize JSON into a dictionary object
    /// </summary>
    public static class JsonUtilities
    {
        /// <summary>
        /// This method is good for converting JSON into a simple nested dictionary object.
        /// </summary>
        /// <param name="jsonString">JSON string to create a nested dictionary from.</param>
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
    }
}