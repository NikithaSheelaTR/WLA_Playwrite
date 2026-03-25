using System;
using System.Collections;

namespace Common.Utils.Serialization
{
    /// <summary>
    ///  The class to support loading friendly homogeneous objects from the external definition files.
    /// </summary>
    public class HomogeneousTypeBuilder
    {
        #region PUBLIC methods
        /// <summary>
        /// Loads homogeneous objects of type T from the external XML-file with definitions.
        /// </summary>
        /// <param name="fileName">The XML-file with definitions.</param>
        /// <param name="rootForDefinitions">The container, where to load definitions from.</param>
        /// <param name="nameSpace">The namespace for each deserialised element.</param>
        /// <typeparam name="T">The type of definitions to deserialise.</typeparam>
        /// <returns>The array of homogeneous objects of type T.</returns>
        public static T[] BuildDefinitionsFromFile<T>(string fileName, string rootForDefinitions, string nameSpace)
        {
            var result = new T[] { };

            try
            {
                var defs = DefinitionBuilder.BuildDefinitionsFromFile(
                    fileName, rootForDefinitions, new[] { typeof(T) }, nameSpace);

                result = new ArrayList(defs).ToArray(typeof(T)) as T[];
            }
            catch (Exception e)
            {
                Logger.LogError(
                    "The parsing of the file '{0}' with {2} definitions failed with an exception\n{1}\n",
                    fileName,
                    e,
                    typeof(T).Name);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static T[] BuildDefinitionsFromFile<T>(string fileName, string nameSpace = null)
        {
            return BuildDefinitionsFromFile<T>(fileName, typeof(T).Name + "Definitions", nameSpace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="definitions"></param>
        /// <param name="targetFile"></param>
        public static void SerialiseDefinitionsToFile<T>(T[] definitions, string targetFile)
        {
            DefinitionBuilder.SerialiseDefinitionsToFile(definitions as object[], typeof(T).Name + "Definitions", targetFile);
        }
        #endregion
    }
}