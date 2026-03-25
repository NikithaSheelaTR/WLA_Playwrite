//namespace WestlawPrecision.Tests
//{
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using ReportPortal.Client.Abstractions.Responses;
//    using ReportPortal.Client;
//    using System;
//    using System.Collections.Generic;
//    using System.Threading.Tasks;
//    using ReportPortal.Client.Abstractions.Models;
//    using ReportPortal.Client.Abstractions.Requests;
//    using System.Linq;
//    using System.IO;
//    using System.Text.RegularExpressions;
//    using ReportPortal.Client.Abstractions.Filtering;
//    using Framework.Core.Utils.Extensions;

//    /// <summary>
//    /// Precision Left rail filters tests
//    /// </summary>
//    [TestClass]
//    public class ReportPortalUtils
//    {
//        /// <summary>
//        /// Precision Left rail filters tests
//        /// </summary>
//        [TestMethod]
//        [TestCategory("Generator")]
//        public async Task LaunchIdGenerator()
//        {
//            Uri reportPortalInstanceUri = new Uri("https://cr-reportportal.1129.aws-int.thomsonreuters.com/api/v1");
//            string projectName = Environment.GetEnvironmentVariable("TEST_LOCATION");
//            string token = Environment.GetEnvironmentVariable("RP_TOKEN");
//            Service service = new Service(reportPortalInstanceUri, projectName, token);
//            LaunchCreatedResponse launchCreatedResponse = await service.Launch.StartAsync(new StartLaunchRequest()
//            {
//                Name = $"{Environment.GetEnvironmentVariable("TEST_SUITE")} {Environment.GetEnvironmentVariable("TEST_PRODUCTS")} {Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")}",
//                Description = Environment.GetEnvironmentVariable("LAUNCH_DESCRIPTION"),
//                Mode = LaunchMode.Default,
//                StartTime = DateTime.UtcNow,
//                Attributes = new List<ItemAttribute>()
//                {
//                    new ItemAttribute
//                    {
//                        Key = "run_id",
//                        Value = Environment.GetEnvironmentVariable("TEST_RUN_GUID")
//                    }
//                }
//            });

//            LaunchResponse launch = await service.Launch.GetAsync(launchCreatedResponse.Uuid);

//            string testListFilePath = $@"{Environment.GetEnvironmentVariable("TEMP_OUTPUT_FILE")}";

//            string fileContent = File.ReadAllText(testListFilePath);

//            var testNames = Regex.Matches(fileContent, @"^\s*(\w+)(?=\r?\n)", RegexOptions.Multiline);

//            using (var outputFile = File.CreateText(testListFilePath))
//            {
//                outputFile.WriteLine($"RP_LAUNCH_ID={launch.Id}");
//                outputFile.WriteLine($"TESTS_LIST={string.Join(",", testNames.Cast<Match>().Select(m => m.Groups[0].Value.Trim()))}");
//            }
//        }

//        /// <summary>
//        /// Precision Left rail filters tests
//        /// </summary>
//        [TestMethod]
//        [TestCategory("TestListCollector")]
//        public void TestListCollector()
//        {
//            string testListFilePath = $@"{Environment.GetEnvironmentVariable("TEMP_OUTPUT_FILE")}";

//            string fileContent = File.ReadAllText($@"{Environment.GetEnvironmentVariable("TEMP_OUTPUT_FILE")}");

//            var testNames = Regex.Matches(fileContent, @"^\s*(\w+)(?=\r?\n)", RegexOptions.Multiline);

//            using (var outputFile = File.CreateText(testListFilePath))
//            {
//                outputFile.WriteLine($"TESTS_LIST={string.Join(",", testNames.Cast<Match>().Select(m => m.Groups[0].Value.Trim()))}");
//            }
//        }

//        /// <summary>
//        /// Precision Left rail filters tests
//        /// </summary>
//        [TestMethod]
//        [TestCategory("LaunchesAggregator")]
//        public async Task LaunchesAggregator()
//        {
//            Uri reportPortalInstanceUri = new Uri("https://cr-reportportal.1129.aws-int.thomsonreuters.com/api/v1");
//            string projectName = Environment.GetEnvironmentVariable("TEST_LOCATION");
//            string token = Environment.GetEnvironmentVariable("RP_TOKEN");

//            Service service = new Service(reportPortalInstanceUri, projectName, token);

//            // Retrieve current launches
//            FilterOption currentLaunchesFilter = new FilterOption()
//            {
//                Filters = new List<Filter>()
//                {
//                    new Filter(FilterOperation.Contains, "name", $"{Environment.GetEnvironmentVariable("TEST_SUITE")} {Environment.GetEnvironmentVariable("TEST_PRODUCTS")} {Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")} {Environment.GetEnvironmentVariable("TEST_RUN_GUID")}"),
//                    new Filter(FilterOperation.Equals, "status", "IN_PROGRESS")
//                }
//            };

//            Content<LaunchResponse> unfinishedLaunches = await service.Launch.GetAsync(currentLaunchesFilter);

//            foreach (var launch in unfinishedLaunches.Items)
//            {
//                await service.Launch.FinishAsync(launch.Uuid.ToString(), new FinishLaunchRequest
//                {
//                    EndTime = DateTime.UtcNow
//                });
//            }

//            FilterOption filter = new FilterOption()
//            {
//                Filters = new List<Filter>()
//                {
//                    new Filter(FilterOperation.Contains, "name", $"{Environment.GetEnvironmentVariable("TEST_SUITE")} {Environment.GetEnvironmentVariable("TEST_PRODUCTS")} {Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")} {Environment.GetEnvironmentVariable("TEST_RUN_GUID")}")
//                },
//                Paging = new Paging(1, Environment.GetEnvironmentVariable("PARALLEL_TEST_COUNT").ConvertCountToInt()),
//                Sorting = new Sorting(new List<string> { "startTime" }, SortDirection.Descending)
//            };

//            Content<LaunchResponse> last10NamedLaunches = await service.Launch.GetAsync(filter);

//            DateTime startTime = last10NamedLaunches.Items.Min(launch => launch.StartTime);
//            DateTime? endTime = last10NamedLaunches.Items.Max(launch => launch.EndTime);

//            LaunchResponse firstLaunch = last10NamedLaunches.Items.First();

//            LaunchResponse finalLaunch = await service.Launch.MergeAsync(new MergeLaunchesRequest
//            {
//                Name = $"{Environment.GetEnvironmentVariable("TEST_SUITE")} {Environment.GetEnvironmentVariable("TEST_PRODUCTS")} {Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")}",
//                Description = firstLaunch.Description,
//                StartTime = startTime,
//                EndTime = endTime.Value,
//                MergeType = "DEEP",
//                Mode = firstLaunch.Mode,
//                Launches = last10NamedLaunches.Items.Select(l => l.Id).ToList()
//            });
//        }
//    }
//}
