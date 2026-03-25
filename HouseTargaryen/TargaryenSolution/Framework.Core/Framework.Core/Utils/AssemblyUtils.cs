namespace Framework.Core.Utils
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// The assembly utils.
    /// </summary>
    public class AssemblyUtils
    {
        /// <summary>
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="resourceName">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetResource(string assemblyName, string resourceName)
        {
            Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(assemblyName + ".dll"));
            TextReader textReader =
                new StreamReader(assembly.GetManifestResourceStream(assemblyName + "." + resourceName));
            string result = textReader.ReadToEnd();
            textReader.Close();

            return result;
        }
    }
}