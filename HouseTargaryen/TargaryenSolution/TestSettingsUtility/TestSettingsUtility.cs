namespace TestSettingsUtility
{
    using System;
    using System.Xml;

    /// <summary>
    /// TestSettingsUtility updates .testsettings/.runsettings file with 1)email subject for qreport and 2)the number of parallel test runs
    /// </summary>
    public class TestSettingsUtility
    {
        public static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 5)
            {
                Console.WriteLine("ERROR: {0} expects 4 input parameters, but {1} arguments were specified.", args[0], args.Length - 1);
            }
            else
            {
                string originalTestSettingsFile = args[1], newTestSettingsFile = args[2], qReportEmailSubject = args[3], threadsCount = args[4];

                var xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.Load(originalTestSettingsFile);
                    if (xmlDoc.DocumentElement != null)
                    {
                        XmlNode testSettings = xmlDoc.GetElementsByTagName(xmlDoc.DocumentElement.Name)[0];

                        XmlAttributeCollection attributesList = testSettings.Attributes;

                        if (attributesList != null)
                        {
                            attributesList.GetNamedItem("name").Value = qReportEmailSubject;
                        }

                        if (xmlDoc.GetElementsByTagName("NamingScheme")[0] != null)
                        {
                            xmlDoc.GetElementsByTagName("NamingScheme")[0].RemoveAll();
                        }

                        XmlElement namingScheme = xmlDoc.CreateElement("NamingScheme", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

                        XmlAttribute baseName = xmlDoc.CreateAttribute("baseName");
                        baseName.Value = qReportEmailSubject;

                        XmlAttribute appendTimeStamp = xmlDoc.CreateAttribute("appendTimeStamp");
                        appendTimeStamp.Value = "false";

                        XmlAttribute useDefault = xmlDoc.CreateAttribute("useDefault");
                        useDefault.Value = "false";

                        namingScheme.Attributes.Append(baseName);
                        namingScheme.Attributes.Append(appendTimeStamp);
                        namingScheme.Attributes.Append(useDefault);

                        testSettings.AppendChild(namingScheme);

                        // parallelTestCount defines the number of the parallel test runs will be launched
                        // options are in the range -0 <= n <= number of cores
                        // where 0 = Auto configure: all free cores will be used
                        int parallelTestCount;
                        if (int.TryParse(threadsCount, out parallelTestCount) && parallelTestCount >= 0)
                        {
                            parallelTestCount = parallelTestCount <= Environment.ProcessorCount
                                                    ? parallelTestCount
                                                    : Environment.ProcessorCount;

                            attributesList = xmlDoc.GetElementsByTagName("Execution")[0].Attributes;

                            if (attributesList != null && attributesList.Count > 0)
                            {
                                attributesList.GetNamedItem("parallelTestCount").Value = parallelTestCount.ToString();
                            }
                        }
                    }

                    xmlDoc.Save(newTestSettingsFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}