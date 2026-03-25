namespace Common.Utils.Serialization
{
    #region Using Namespaces
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    #endregion

    /// <summary>
    ///  Basic class providing functionality to deserialise objects from external XML-files.
    /// </summary>
    public class DefinitionBuilder
    {
        #region PUBLIC methods
        /// <summary>
        /// Loads definitions from the external XML-file with definitions by deserialising them.
        /// </summary>
        /// <param name="fileName">The XML-file with definitions.</param>
        /// <param name="rootForDefinitions">The container, where to load definitions from.</param>
        /// <param name="nativeDefinitionTypes">The types whose to definitions deserialise.</param>
        /// <param name="nameSpace">The namespace for each deserialised element.</param>
        /// <returns>The array of object of the types specified in nativeDefinitionTypes.</returns>
        public static object[] BuildDefinitionsFromFile(
            string fileName,
            string rootForDefinitions,
            Type[] nativeDefinitionTypes,
            string nameSpace)
        {
            var definitions = new List<object>();
            var xdoc = XDocument.Parse(File.ReadAllText(fileName));
            XElement rootElement = xdoc.Elements().ToArray()[0].Element(rootForDefinitions);

            if (rootElement != null)
            {
                XNamespace ns = nameSpace ?? string.Empty;

                foreach (var type in nativeDefinitionTypes)
                {
                    foreach (var localRoot in rootElement.Elements(ns + type.Name))
                    {
                        try
                        {
                            var xreader = localRoot.CreateReader();
                            var serialiser = new XmlSerializer(type);

                            definitions.Add(serialiser.Deserialize(xreader));
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(
                                "Could not deserialise a definition due to the exception\n{0}{1}\n  Skipping\n",
                                ex.Message,
                                ex.InnerException != null ? string.Format("\n (Inner Exception):{0}", ex.InnerException.Message) : string.Empty);
                        }
                    }
                }
            }

            return definitions.ToArray();
        }

        /// <summary>
        /// Writes definitions to the external XML-file by serialising them.
        /// </summary>
        /// <param name="definitions">The array of objects to serialise.</param>
        /// <param name="rootForDefinitions">The container, where to write definitions to.</param>
        /// <param name="targetFile">The XML-file with definitions.</param>
        public static void SerialiseDefinitionsToFile(object[] definitions, string rootForDefinitions, string targetFile)
        {
            var fs = new FileStream(targetFile, FileMode.Create);
            var writer = new XmlTextWriter(fs, Encoding.UTF8) { Formatting = Formatting.Indented };

            writer.WriteStartDocument();
            writer.WriteStartElement("Definitions");

            SerialiseDefinitions(definitions, rootForDefinitions, writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            fs.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="targetFile"></param>
        public static void SerialiseBareDefinitionToFile(object definition, string targetFile)
        {
            var fs = new FileStream(targetFile, FileMode.Create);
            var writer = new XmlTextWriter(fs, Encoding.UTF8)
            {
                Indentation = 2,
                IndentChar = ' ',
                Formatting = Formatting.Indented
            };

            SerialiseBareDefinition(definition, writer);

            writer.Close();
            fs.Close();
        }
        #endregion

        #region INTERNAL methods
        internal static void SerialiseDefinitions(object[] definitions, string rootForDefinitions, XmlTextWriter writer)
        {
            if (writer != null && writer.WriteState != WriteState.Closed && writer.WriteState != WriteState.Error)
            {
                if (!string.IsNullOrEmpty(rootForDefinitions))
                {
                    writer.WriteStartElement(rootForDefinitions);
                }

                foreach (var definition in definitions)
                {
                    SerialiseBareDefinition(definition, writer);
                }

                if (!string.IsNullOrEmpty(rootForDefinitions))
                {
                    writer.WriteEndElement();
                }
            }
        }

        internal static void SerialiseBareDefinition<T>(T definition, XmlTextWriter writer)
        {
            if (writer != null && writer.WriteState != WriteState.Closed && writer.WriteState != WriteState.Error)
            {
                Type baseType = typeof (T),
                    defType = baseType.IsValueType || Equals(definition, null) ? baseType : definition.GetType();

                try
                {
                    var serialiser = new XmlSerializer(defType);
                    
                    // ReSharper disable once AssignNullToNotNullAttribute
                    serialiser.Serialize(writer, definition);
                }
                catch (Exception ex)
                {
                    Logger.LogError(
                        "Could not serialise the definition of type '{0}' due to the exception\n{1}\n  Skipping\n",
                        defType,
                        ex.Message);
                }
            }
        }
        #endregion
    }
}
