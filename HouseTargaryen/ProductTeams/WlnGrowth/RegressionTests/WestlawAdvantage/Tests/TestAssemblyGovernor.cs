using Framework.Core.QrtBaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// The class encapsulates the most common test assembly operations that precede and follow the test life cycle. 
/// </summary>
[TestClass]
public static class TestAssemblyGovernor
{
    /// <summary>
    /// Prepares a test run.
    /// </summary>
    /// <param name="testContext">The test Context.</param>
    [AssemblyInitialize]
    public static void InitializeAssembly(TestContext testContext)
    {
        BaseTest.InitializeTestRun();
    }

    /// <summary>
    /// Generates QualityTestRun.xml file for loading quality checks to QRT 2.0
    /// </summary>
    [AssemblyCleanup]
    public static void CleanUpAssembly()
    {
        BaseTest.CleanupTestRun();
    }
}